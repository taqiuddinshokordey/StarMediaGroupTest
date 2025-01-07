using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using StarMediaGroupTest;
using Microsoft.Extensions.Configuration;
using StarMediaGroupTest.Models;
using Microsoft.EntityFrameworkCore;
using StarMediaGroupTest.Data;
using System.Diagnostics;
using System.Linq;

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

        [HttpPost]
        public IActionResult DeclineConsent()
        {
            try
            {
                var guid = Guid.NewGuid();
                var consent = new PrivacyConsent
                {
                    Id = guid,
                    Accepted = false,
                    Timestamp = DateTime.UtcNow,
                    Version = 1
                };

                _context.Add(consent);
                _context.SaveChanges();

                Response.Cookies.Append("PrivacyConsent", guid.ToString(),
                    new CookieOptions { Expires = DateTime.UtcNow.AddDays(1), HttpOnly = true });

                // Return success message (optional: return the ID if needed by the client)
                return Json(new { message = "Consent Declined", consentId = guid });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Json(new { message = "Error accepting consent", error = ex.Message });
            }
        }

        public class ThemePreferenceRequest
        {
            public bool IsDarkMode { get; set; }
        }

        [HttpPost]
        public IActionResult UpdateThemePreference([FromBody] ThemePreferenceRequest request)
        {
            try
            {
                bool isDarkMode = request.IsDarkMode;
                // Get consent ID from cookie
                var consentIdString = Request.Cookies["PrivacyConsent"];
                if (string.IsNullOrEmpty(consentIdString) || !Guid.TryParse(consentIdString, out Guid consentId))
                {
                    return Json(new { success = false, message = "Consent not found" });
                }

                // Update theme preference
                var consent = _context.PrivacyConsentModel!.Find(consentId);
                if (consent == null)
                {
                    return Json(new { success = false, message = "Consent not found" });
                }

                consent.IsDarkMode = isDarkMode;
                _context.SaveChanges();

                return Json(new { success = true, message = "Theme preference updated" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetThemePreference()
        {
            try
            {
                var consentIdString = Request.Cookies["PrivacyConsent"];
                if (string.IsNullOrEmpty(consentIdString) || !Guid.TryParse(consentIdString, out Guid consentId))
                {
                    return Json(new { success = false, isDarkMode = false });
                }

                var consent = _context.PrivacyConsentModel!.Find(consentId);
                if (consent == null)
                {
                    return Json(new { success = false, isDarkMode = false });
                }

                return Json(new { success = true, isDarkMode = consent.IsDarkMode });
            }
            catch (Exception)
            {
                return Json(new { success = false, isDarkMode = false });
            }
        }

        [HttpGet]
        public IActionResult GetCookie(Guid id)
        {
            try
            {
                // Assuming you have _context as your DbContext
                var cookieValue = _context.PrivacyConsentModel!
                    .Where(c => c.Id == id)
                    .Select(c => c.Id)
                    .FirstOrDefault();

                var consentIdString = Request.Cookies["PrivacyConsent"];
                if (string.IsNullOrEmpty(consentIdString) || !Guid.TryParse(consentIdString, out cookieValue))
                {
                    return Json(new { success = false, message = "Cookie not found" });
                }


                return Json(new { success = true, value = cookieValue });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

