using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using StarMediaGroupTest;
using Microsoft.Extensions.Configuration;
using StarMediaGroupTest.Models;
using Microsoft.EntityFrameworkCore;
using StarMediaGroupTest.Data;
using System.Diagnostics;

namespace StarMediaGroupTest.Controllers
{
    public class ConsentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AcceptConsent()
        {
            try
            {
                var guid = Guid.NewGuid();
                var consent = new PrivacyConsent
                {
                    Id = guid,
                    Accepted = true,
                    Timestamp = DateTime.UtcNow,
                    Version = 1
                };

                _context.Add(consent);
                _context.SaveChanges();

                Response.Cookies.Append("PrivacyConsent", guid.ToString(),
                    new CookieOptions { Expires = DateTime.UtcNow.AddYears(1), HttpOnly = true });

                // Return success message (optional: return the ID if needed by the client)
                return Json(new { message = "Consent accepted", consentId = guid });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Json(new { message = "Error accepting consent", error = ex.Message });
            }
        }
    }
}