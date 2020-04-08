﻿using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
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
        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteData = _uow.category.Get(id);
            if (deleteData == null)
            {
                return Json(new { success = false, message = "Data not found !" });
            }
            _uow.category.Remove(deleteData);
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
            Category cat = new Category();
            if (id == null)
            {
                // this for create operation.
                return View(cat);
            }

            cat = _uow.category.Get((int)id);
            if (cat != null)
            {
                return View(cat);
            }

            return NotFound();



        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.Id == 0)
                {
                    // create
                    _uow.category.Add(category);
                }
                else
                {
                    // update
                    _uow.category.Update(category);
                }
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(category);


        }


    }
}