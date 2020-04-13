﻿using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _uow;
        #endregion

        #region Constructor
        public CompanyController(IUnitOfWork uow)
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
            var allObj = _uow.Company.GetAll();
            return Json(new { data = allObj });

        }
        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteData = _uow.Company.Get(id);
            if (deleteData == null)
            {
                return Json(new { success = false, message = "Data not found !" });
            }
            _uow.Company.Remove(deleteData);
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
            Company cat = new Company();
            if (id == null)
            {
                // this for create operation.
                return View(cat);
            }

            cat = _uow.Company.Get((int)id);
            if (cat != null)
            {
                return View(cat);
            }

            return NotFound();



        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company Company)
        {
            if (ModelState.IsValid)
            {
                if(Company.Id == 0)
                {
                    // create
                    _uow.Company.Add(Company);
                }
                else
                {
                    // update
                    _uow.Company.Update(Company);
                }
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(Company);


        }


    }
}