using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient; // Required for SQL Server connections
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace StarMediaGroupTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("test-sql-db")]
        public async Task<IActionResult> TestSqlDb()
        {
            try
            {
                // Get connection string from configuration
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                // Create and open SQL Server connection
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    return Ok("Database connection successful!");
                }
            }
            catch (Exception ex)
            {
                // Log and return error message
                return StatusCode(500, $"Error connecting to database: {ex.Message}");
            }
        }

        // Other endpoints remain unchanged...
    }
}
