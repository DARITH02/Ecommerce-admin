using AdminPage.Data;
using AdminPage.Models;
using DocuSign.eSign.Model;
using Microsoft.AspNetCore.Mvc;
using Brand = AdminPage.Models.Brand;


namespace AdminPage.Controllers
{

    public class BrandController : Controller
    {
        private readonly DbContext _DBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BrandController(DbContext dbContext,IWebHostEnvironment webHostEnvironment)
        {
            _DBContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
           //List<Brand> brands = new List<Brand>();
           //brands= _DBContext.selectBrand();
           return View();
        }

        public JsonResult GetAllBrand()
        {
            List<Brand> brands = new List<Brand>();
            brands = _DBContext.selectBrand();
            return Json(new { brands });
        }


        public JsonResult GetImage(Brand obj)
        {
            string newFile = "";
            if (obj.Img != null && obj.Img.Length > 0)
            {
                newFile = DateTime.Now.ToString("yyyyMMdd-HHmmss") + Path.GetExtension(obj.Img.FileName).ToLower();

                string folder = Path.Combine(_webHostEnvironment.WebRootPath, "TempImages");
                string fullPath=Path.Combine(folder, newFile);

                using(var stream=new FileStream(fullPath, FileMode.Create))
                {
                   obj.Img.CopyTo(stream);
                }
                obj.Img_name = newFile;

                return Json(new { obj.Img_name });
            }
            return Json(new{ obj, newFile });
        }



        public JsonResult Create(Brand obj)
        {
            if (obj.Brand_name != null && obj.Img_name != null)
            {

                string tmpPath = Path.Combine(_webHostEnvironment.WebRootPath, "TempImages");
                string imgPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/Brands");

                string imgTmp = Path.Combine(tmpPath, obj.Img_name);

                string imgFinal = Path.Combine(imgPath, obj.Img_name);


                bool isInsert = _DBContext.insertBrand(obj);
                if (isInsert) {
                System.IO.File.Move(imgTmp, imgFinal);                
                }
                else
                {
                   System.IO.File.Delete(imgTmp);
                }
                return Json(new { a = 100 , isInsert ,obj});
            }
            return Json(new { obj });
        }

        // [HttpGet]
        // public IActionResult Create()
        // {
        //   return View();
        // }
        // [HttpPost]
        // public IActionResult Create(Brand obj)
        //{

         /*   if (obj.Img != null && obj.Img.Length > 0 && obj.Brand_name != null)
        //     {*/
        //         string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(obj.Img.FileName).ToLower();
        //         string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        //         string fullPath = Path.Combine(folder, fileName);

        //         using (var stream = new FileStream(fullPath, FileMode.Create))
        //         {
        //             obj.Img.CopyTo(stream);
        //         }

        //         var brand = new Brand
        //         {
        //             Brand_name = obj.Brand_name,
        //             Brand_description = obj.Brand_description,
        //             Create_at = obj.Create_at, 
        //             Update_at = obj.Update_at, 
        //             Img_name = fileName

        //         };

        //     _DBContext.insertBrand(brand);

        //              return Json(new {a=true});

        /* }


         return View(obj);*/
        //}


        public JsonResult Edite(int id)
        {

            var items = _DBContext.Find_items(id);

            return Json(items);
        }

        [HttpPost]
        public JsonResult Edite(Brand brand)
        {       
            string imgNew = "";
            if (brand.Brand_name != null&&brand.Brand_description!=null) {  
                string tmpDir = Path.Combine(_webHostEnvironment.WebRootPath, "TempImages");
                string imgDir= Path.Combine(_webHostEnvironment.WebRootPath, "images/Brands");
                if (brand.Img_name!=null)
                    {
                        imgNew = brand.Img_name;
                        string tmpImg = Path.Combine(tmpDir, brand.Img_name);
                        string imgFinal = Path.Combine(imgDir,brand.Img_name);

                        string imgOldDir = Path.Combine(imgDir, brand.ImgOld);

                        System.IO.File.Move(tmpImg, imgFinal);

                        System.IO.File.Delete(imgOldDir);

                    }
                else
                    {
                        imgNew=brand.ImgOld;
                    }
                        Brand brand1=new Brand()
                        {
                            Id=brand.Id,
                            Brand_name=brand.Brand_name,
                            Brand_description=brand.Brand_description,
                            Img_name=imgNew,
                            Create_at=brand.Create_at
                        };
                bool isUpdate = _DBContext.updateBrand(brand1);

                    if (isUpdate) { 
                
                            return Json(new { brand1 ,isUpdate});
                        }
                    else
                        {
                            if (brand.Img_name != null) {
                            string imgFinal = Path.Combine(imgDir, brand.Img_name);

                                System.IO.File.Delete(imgFinal);
                            }
                            return Json(new { brand1, isUpdate });

                        }
            }
            return Json(new { status = 300 });
        } 
       
        //public IActionResult Delete(int id)
        //{

        //    bool isDelete = _DBContext.deleteBrand(id);
        //    if (isDelete)
        //    {
        //        return Json(new { status = true });
        //    }
        //    else
        //    {
        //        return Json(new { statu = false });
        //    }
        //    return Json(isDelete);

        //}

    }
    }

