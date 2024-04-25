using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

using Iktan.Ecommerce.Transversal.Common;


namespace Iktan.Ecommerce.Infraestructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null)
                {
                    return null;
                }
                else
                {
                    sqlConnection.ConnectionString = _configuration.GetConnectionString("Northwind");
                    sqlConnection.Open();
                    return sqlConnection;
                }
            }
        }
    }
}
