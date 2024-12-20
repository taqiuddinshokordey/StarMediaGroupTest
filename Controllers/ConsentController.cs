using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using StarMediaGroupTest;
using Microsoft.Extensions.Configuration;
using StarMediaGroupTest.Models;
using Microsoft.EntityFrameworkCore;
using StarMediaGroupTest.Data;

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
            var guid = Guid.NewGuid();
            var consent = new PrivacyConsent
            {
                Id = guid,
                Accepted = true,
                Timestamp = DateTime.UtcNow,
                Version = 1
            };

            var consentData = $"{consent.Id},{consent.Timestamp},{consent.Version}";

            _context.Add(consent);
            _context.SaveChanges();

            Response.Cookies.Append("PrivacyConsent",consentData,
                new CookieOptions { Expires = DateTime.UtcNow.AddYears(1) });

            // Return the consent data in the response
            return Json(new { message = "Consent accepted", privacyConsentCookie = consentData });
        }
    }
}