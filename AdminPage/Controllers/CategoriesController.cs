using Microsoft.AspNetCore.Mvc;
using AdminPage.Data;
using AdminPage.Models;
namespace AdminPage.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DbContext _dbContext;
        public CategoriesController(DbContext dbContext)
        {
            _dbContext = dbContext;

        }



        public IActionResult Index()
        {
            List<Categories> categories = _dbContext.getCateAll();
            return View(categories);
        }

        [HttpPost]
        public IActionResult Create(string CategoriesName) {
            /*   _dbContext.insertCate(cate);*/
            var cate_name = new Categories();
            cate_name.CategoriesName = CategoriesName;
            bool isInsert = _dbContext.insertCate(cate_name);

            if (isInsert) {
                return Json(new { status = isInsert });
            }
            return Json(new { status = isInsert });
        }

        public JsonResult Edite(int id)
        {
            Categories cate_find = _dbContext.Find_cate(id);

            return Json(cate_find);
        }


        [HttpPost]
        public JsonResult Edite(Categories cate)
        {
            bool isUpdate = _dbContext.updateCate(cate);
            if (isUpdate) {

                return Json(new { status = isUpdate });

            }
            return Json(new { status = isUpdate });
        }

        [HttpPost]
        public JsonResult Delete(int id) {

            bool isDelete= _dbContext.Delete_cate(id);
            return Json(new { status = isDelete });
        }



    }
}
