using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainMusicStore.DataAccess.IMainRepository;
using Microsoft.AspNetCore.Mvc;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _uow; 
        #endregion
        
        #region Constructor
        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
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
            var allObj = _uow.category.GetAll();
            return Json(new { data = allObj });

        } 
        #endregion

    }
}