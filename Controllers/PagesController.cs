// File: Controllers/PagesController.cs
using Microsoft.AspNetCore.Mvc;

namespace WebSiteExample.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }
    }
}
