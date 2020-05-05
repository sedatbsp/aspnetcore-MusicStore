using Dapper;
using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using MainMusicStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ProjectConstant.Role_Admin)]
    public class CoverTypeController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _uow;
        #endregion

        #region Constructor
        public CoverTypeController(IUnitOfWork uow)
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
            //var allObj = _uow.coverType.GetAll();
            var allCoverTypes = _uow.Sp_call.List<CoverType>(ProjectConstant.Proc_CoverType_GetAll,null);
            return Json(new { data = allCoverTypes });

        }
        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //var deleteData = _uow.coverType.Get(id);
            //if (deleteData == null)
            //{
            //    return Json(new { success = false, message = "Data not found !" });
            //}
            //_uow.coverType.Remove(deleteData);
            //_uow.Save();
            //return Json(new { success = true,message="Delete operation successfully" });

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);

            var deleteData = _uow.Sp_call.OneRecord<CoverType>(ProjectConstant.Proc_CoverType_Get,parameter);
            if (deleteData == null)
            {
                return Json(new { success = false, message = "Data not found !" });
            }

            _uow.Sp_call.Execute(ProjectConstant.Proc_CoverType_Delete,parameter);
            _uow.Save();
            return Json(new { success = true, message = "Delete operation successfully" });

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
            CoverType coverType = new CoverType();
            if (id == null)
            {
                // this for create operation.
                return View(coverType);
            }

            var parameter = new DynamicParameters();
            parameter.Add("@Id",id);
            coverType = _uow.Sp_call.OneRecord<CoverType>(ProjectConstant.Proc_CoverType_Get,parameter);

            //ct = _uow.coverType.Get((int)id);
            if (coverType != null)
            {
                return View(coverType);
            }

            return NotFound();



        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType CoverType)
        {
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", CoverType.Name);

                if (CoverType.Id == 0)
                {
                    // create
                    //_uow.coverType.Add(CoverType);
                    _uow.Sp_call.Execute(ProjectConstant.Proc_CoverType_Create, parameter);

                }
                else
                {
                    // update
                    parameter.Add("@Id", CoverType.Id);
                    //_uow.coverType.Update(CoverType);
                    _uow.Sp_call.Execute(ProjectConstant.Proc_CoverType_Update, parameter);
                }
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(CoverType);


        }


    }
}