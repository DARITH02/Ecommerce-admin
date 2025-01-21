using AdminPage.Data;
using AdminPage.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace AdminPage.Controllers
{
    public class PhoneDetailController : Controller
    {
        private readonly ProductsDbContext _contextDbPro;
        private readonly DetailPhoneDbContext _contextDbDetailPhone;
        private readonly DbContext _contextDbBrand;



        public PhoneDetailController(ProductsDbContext contextDbPro,DetailPhoneDbContext detailPhone,DbContext brand)
        {
            _contextDbPro = contextDbPro;
            _contextDbDetailPhone = detailPhone;
            _contextDbBrand = brand;
        }

        public IActionResult Index()
        {

            List<Products> products = _contextDbPro.getCondition();
            ViewBag.ProductsId = products;

            List<Brand> brands=_contextDbBrand.selectBrand();
            ViewBag.Brand=brands;

            return View();
        }
        public JsonResult GetAll()
        {

            List<DetailPhone> listPhone = _contextDbDetailPhone.getDetailPhones();
            return Json(listPhone);
        }
        [HttpPost]
        public JsonResult AddInfo(DetailPhone phone) {
            bool insert = false;
            if (phone.Camera!=0&& phone.Discount != 0 && phone.Display!= 0 && phone.Ram!= 0 && phone.Rom!= 0 && phone.Color != null && phone.Bettery!= 0 )
            {
           insert=  _contextDbDetailPhone.infoPhone(phone);

            }
            return Json(new { insert, phone });
        }
        public JsonResult Edite(int id)
        {
             var findId=_contextDbDetailPhone.findId(id);

            return Json(new { id, findId });
        }
        [HttpPost]
        public JsonResult Update(DetailPhone phone) {

            bool isUpdate = _contextDbDetailPhone.Update(phone);
        return Json(new { phone, isUpdate });
        }

        public JsonResult Delete(int id)
        {
         bool isDelete=_contextDbDetailPhone.Delete(id);

            return Json(new { isDelete });   
         }

        public JsonResult Search(string proName) {
            bool searchNotFound = false;
            List<DetailPhone> search =new List<DetailPhone>();
            if (proName == null)
            {
                searchNotFound = false;
            }
            else
            {
                searchNotFound = true;
                search = _contextDbDetailPhone.search(proName);
            }
        
         return Json (new { searchNotFound, search, proName });
        }


        public JsonResult SearchBrand(string brand) {
            bool searchNotFound = false;
            List<DetailPhone> phones=new List<DetailPhone>();
            if (brand == null) { 
            searchNotFound=false;
            }
            {
                searchNotFound = true;
                phones = _contextDbDetailPhone.searchByBrand(brand);
            }
        
        return Json(new { searchNotFound, brand, phones });
        }

    }
}
