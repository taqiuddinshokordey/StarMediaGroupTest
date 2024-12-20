using Microsoft.EntityFrameworkCore;
using StarMediaGroupTest.Models;

namespace StarMediaGroupTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PrivacyConsent>? PrivacyConsentModel { get; set; } // Replace with your actual model
    }
}
