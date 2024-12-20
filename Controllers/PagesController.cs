// File: Controllers/PagesController.cs
using Microsoft.AspNetCore.Mvc;

namespace StarMediaGroupTest.Controllers
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
        public IActionResult _TermsContent()
        {
            return PartialView();
        }

        public IActionResult _PrivacyContent()
        {
            return PartialView();
        }
    }
}
