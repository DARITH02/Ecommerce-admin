using AdminPage.Data;
using AdminPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class CountProductsController : Controller
    {
        private readonly CountDbContext _countDbContext;
        public CountProductsController(CountDbContext countDbContext) {
            _countDbContext = countDbContext;
        
        }


        public JsonResult Count()
        {
            List<Brand> brand = new List<Brand>();

            brand = _countDbContext.getBrand();
          int count=  _countDbContext.Count();
           List<int> coutBrand = _countDbContext.coutBrand();
            return Json(new { count, brand , coutBrand });
        }
    }
}
