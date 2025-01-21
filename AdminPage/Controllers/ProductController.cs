using Microsoft.AspNetCore.Mvc;
using AdminPage.Data;
using AdminPage.Models;
using Microsoft.AspNetCore.Hosting;

namespace AdminPage.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductsDbContext _DbService;
        private readonly DbContext _DbContext;
        private readonly InventoryDbContext _dbInventory;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ProductController(ProductsDbContext DbContext,IWebHostEnvironment webHostEnvironment, DbContext dbContext , InventoryDbContext dbInventory)
        {
            _DbService = DbContext;
            _WebHostEnvironment = webHostEnvironment;
            _DbContext = dbContext;
            _dbInventory = dbInventory;
        }

        public IActionResult Index()
        {
         
            List<Brand> brands_list=new List<Brand>();
            brands_list = _DbContext.selectBrand();

            List<Categories> categories_list = _DbContext.getCateAll();

            List<Inventory> inventory_list = _dbInventory.getInventories();

            ViewBag.Inventory = inventory_list;
            ViewBag.cate = categories_list;
            ViewBag.Brands = brands_list;


            return View();
        }

        [HttpGet]
        public JsonResult getProducts()
        {
            List<Brand> brands_list = new List<Brand>();
            brands_list = _DbContext.selectBrand();

            var products = _DbService.getAllProducts();
            return Json(new { data = products });
        }


        [HttpPost]
        public JsonResult PreviewImg(Products obj) {
            if (obj.ImgFile != null && obj.ImgFile.Length > 0)
            {
                string FileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + Path.GetExtension(obj.ImgFile.FileName).ToLower();
                string folder = Path.Combine(_WebHostEnvironment.WebRootPath, "TempImages");
                string fullPath = Path.Combine(folder, FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    obj.ImgFile.CopyTo(stream);
                }
                obj.ImgName = FileName;
                return Json(obj.ImgName);
            }

            return Json(new { data = "noFile" });
        }

        [HttpPost]
        public JsonResult Create(Products products) {
            //string FileName = DateTime.Now.ToString("yyyyMMdd-HHmmss")+Path.GetExtension(products.ImgFile.FileName).ToLower();
            //string folder = Path.Combine(_WebHostEnvironment.WebRootPath, "images/Products");
            //string fullPath = Path.Combine(folder, FileName);
            //using(var stream=new FileStream(fullPath, FileMode.Create))
            //{
            //    products.ImgFile.CopyTo(stream);
            //}
                                                                      
            bool isInsert = _DbService.addProducts(products);
            if (products.ImgFile != null && products.ImgFile.Length > 0)
            {
            string tempFolder = Path.Combine(_WebHostEnvironment.WebRootPath, "TempImages");
            string imageFolder = Path.Combine(_WebHostEnvironment.WebRootPath, "images/Products");

            string tempPath = Path.Combine(tempFolder, products.ImgName);
            string finalPath = Path.Combine(imageFolder, products.ImgName);
           
            if (isInsert)
            {
            System.IO.File.Move(tempPath, finalPath);

            }
            else
            {
                System.IO.File.Delete(tempPath);
            }
            }
            return Json(new { data = products,status= isInsert });
        }

        public JsonResult FindProduct(int id)
        {
           Products products = _DbService.findProduct(id);

            return Json(new { id = id,data=products });

        }
        public IActionResult Update(Products product) {
            string imgUp = "";
            if (product.ImgNameUp != null)
            {
                string tempFolder = Path.Combine(_WebHostEnvironment.WebRootPath, "TempImages");
                string imageFolder = Path.Combine(_WebHostEnvironment.WebRootPath, "images/Products");
                string tempPath = Path.Combine(tempFolder, product.ImgNameUp);
                string finalPath = Path.Combine(imageFolder, product.ImgNameUp);
                string imgOld = Path.Combine(imageFolder, product.ImgName);
                System.IO.File.Move(tempPath, finalPath);
                System.IO.File.Delete(imgOld);
                imgUp =product.ImgNameUp;
            }
            else
            {
                imgUp = product.ImgName;
            }
          var pro =new Products{
                   Id=product.Id,
                   Cate_id=product.Cate_id,
                   Brand_id=product.Brand_id,
                   ImgName=imgUp,
                   Prices=product.Prices,
                   Pro_name=product.Pro_name,
                   Invetory=product.Invetory,
                   ProductsCode=product.ProductsCode,
            };
            bool inUpdate = _DbService.update(pro);
            return Json(new { product = product,status= inUpdate });  
        }


        public JsonResult Delete(int id,Products img)
        {
            Products isFind =_DbService.findProduct(id);
            bool isDelete = _DbService.Delete(id);
            string imageFolder = Path.Combine(_WebHostEnvironment.WebRootPath, "images/Products");

            if (isDelete)
            {
                string imgOld = Path.Combine(imageFolder, isFind.ImgName);
                System.IO.File.Delete(imgOld);
            }
            return Json(new { id,isFind.ImgName, isDelete });
        }


    }
}
