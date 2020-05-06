using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using MainMusicStore.Models.ViewModels;
using MainMusicStore.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MainMusicStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(IUnitOfWork uow, IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _uow = uow;
            _emailSender = emailSender;
            _userManager = userManager;

        }
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new OrderHeader(),
                ListCart = _uow.ShoppingCart.GetAll(u => u.ApplicationUserId == claims.Value, includeProperties: "Product")

            };
            ShoppingCartVM.OrderHeader.OrderTotal = 0;
            ShoppingCartVM.OrderHeader.ApplicationUser = _uow.ApplicationUser.GetFirstOrDefault(u => u.Id == claims.Value, includeProperties: "Company");

            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = ProjectConstant.GetPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
                cart.Product.Description = ProjectConstant.ConvertToRawHtml(cart.Product.Description);

                if (cart.Product.Description.Length >= 50)
                {
                    cart.Product.Description = cart.Product.Description.Substring(0, 49) + "...";
                }
            }


            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = _uow.ApplicationUser.GetFirstOrDefault(u => u.Id == claims.Value);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification email is empty!");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            ModelState.AddModelError(string.Empty, "verification emil sent.Please check your email!");
            return RedirectToAction("Index");



        }

        public IActionResult Plus(int cartId)
        {
            var cart = _uow.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId, includeProperties: "Product");

            if (cart == null)
            {
                return RedirectToAction("Index");
            }
            cart.Count += 1;
            cart.Price = ProjectConstant.GetPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
            _uow.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _uow.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId, includeProperties: "Product");
            if (cart.Count == 1)
            {
                var cnt = _uow.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
                _uow.ShoppingCart.Remove(cart);
                _uow.Save();
                HttpContext.Session.SetInt32(ProjectConstant.shoppingCart, cnt - 1);
            }
            else
            {
                cart.Count -= 1;
                cart.Price = ProjectConstant.GetPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                _uow.Save();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _uow.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId, includeProperties: "Product");

            var cnt = _uow.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            _uow.ShoppingCart.Remove(cart);
            _uow.Save();

            HttpContext.Session.SetInt32(ProjectConstant.shoppingCart, cnt - 1);
            return RedirectToAction("Index");
        }

    }
}