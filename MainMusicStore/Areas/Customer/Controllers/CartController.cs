using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainMusicStore.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(IUnitOfWork uow,IEmailSender emailSender,UserManager<IdentityUser> userManager)
        {
            _uow = uow;
            _emailSender = emailSender;
            _userManager = userManager;

        }
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public IActionResult Index()
        {
            ShoppingCartVM  = new ShoppingCartVM()
            {

            };
            return View();
        }
    }
}