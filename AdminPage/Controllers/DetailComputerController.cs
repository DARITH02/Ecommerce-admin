using AdminPage.Data;
using AdminPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AdminPage.Controllers
{
    public class DetailComputerController : Controller
    {
        private readonly PcDbContext _context;
    
        public DetailComputerController(PcDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Products> pc = _context.getProName();


            ViewBag.ProName = pc;

            return View();

        }
        public JsonResult getPc()
        {
            List<Computer> pcs = new List<Computer>();
            pcs = _context.getPc();

            return Json(pcs);
        }


        [HttpPost]
        public JsonResult Create(Computer pc)
        {
            bool computer = _context.addNew(pc);


            return Json(new { pc ,computer });
        }


        
        public JsonResult Edite(int id)
        {

            Computer computer = _context.getById(id);

            return Json(new { id , computer });
        }

        public JsonResult Update(Computer computer)
        {
            bool isUpdate = _context.Update(computer);


            return Json(new { computer ,isUpdate});
        }

        public JsonResult Delete(int id)
        {

            bool isDelete = _context.Delete(id);


            return Json(new { id, isDelete });              
        }


    }
}
