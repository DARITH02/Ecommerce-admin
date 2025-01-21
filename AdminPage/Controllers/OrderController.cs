using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
