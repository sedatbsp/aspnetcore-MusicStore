using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using MainMusicStore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment; // root erisimi saglar.
        #endregion

        #region Constructor
        public ProductController(IUnitOfWork uow, IWebHostEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region API CALLS
        public IActionResult GetAll()
        {
            var allObj = _uow.product.GetAll();
            return Json(new { data = allObj });

        }
        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteData = _uow.product.Get(id);
            if (deleteData == null)
            {
                return Json(new { success = false, message = "Data not found !" });
            }
            _uow.product.Remove(deleteData);
            _uow.Save();
            return Json(new { success = true,message="Delete operation successfully" });

        }

        #endregion

        /// <summary>
        /// Create or Update Get Method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _uow.category.GetAll().Select(i=>new SelectListItem
                {
                   Text = i.CategoryName,
                   Value = i.Id.ToString()
                }),
                CoverTypeList = _uow.coverType.GetAll().Select(i=>new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
                return View(productVM);
            productVM.Product = _uow.product.Get(id.GetValueOrDefault());
            if (productVM.Product == null)
                return NotFound();
            return View(productVM);



        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                if(product.Id == 0)
                {
                    // create
                    _uow.product.Add(product);
                }
                else
                {
                    // update
                    _uow.product.Update(product);
                }
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(product);


        }


    }
}

