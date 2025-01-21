using AdminPage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPage.Controllers
{
    public class HomeController : Controller
    {
       public IActionResult Index()
        {
            return View();
        }
    }
}
