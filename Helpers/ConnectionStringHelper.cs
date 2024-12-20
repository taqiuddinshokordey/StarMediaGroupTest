using Microsoft.Extensions.Configuration;

namespace StarMediaGroupTest.Helpers
{
    public class ConnectionStringHelper
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? GetRDSConnectionString()
        {
            // Retrieve the connection string from appsettings.json
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString)) return null;

            return connectionString; // Return the connection string directly
        }
    }
} 