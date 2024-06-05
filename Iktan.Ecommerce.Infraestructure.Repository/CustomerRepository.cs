using System;
using System.Data;
using Dapper;
using System.Threading.Tasks;

using Iktan.Ecommerce.Domain.Entity;
using Iktan.Ecommerce.Infraestructure.Interface;
using Iktan.Ecommerce.Transversal.Common;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.ComponentModel;



namespace Iktan.Ecommerce.Infraestructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private IConnectionFactory _connectionFactory;

        public CustomerRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }        

        #region Metodos Asincronos

        public async Task<bool> InsertAsync(Customers customer) 
        {
            using (var connection = _connectionFactory.GetDbConnection) 
            {
                var storeSp = "CustomersInsert";

                var parameters = new DynamicParameters();
                parameters.Add("@CustomerID", customer.CustomerId,
                        dbType: DbType.String,
                        direction: ParameterDirection.Input);
                parameters.Add("@CompanyName", customer.CompanyName,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@ContactName", customer.ContactName,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@ContactTitle", customer.CustomerId,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Address", customer.Address,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@City", customer.City,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Region", customer.Region,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@PostalCode", customer.PostalCode,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Country", customer.Country,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Phone", customer.Phone,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Fax", customer.Fax,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);

                var responseSp = await connection.ExecuteAsync(storeSp, param: parameters, commandType: CommandType.StoredProcedure);
                return responseSp > 0;
            }
        }

        public async Task<bool> UpdateAsync(Customers customer) 
        {
            using (var connection = _connectionFactory.GetDbConnection) 
            {
                var storeSp = "CustomersUpdate";

                var parameters = new DynamicParameters();
                parameters.Add("@CustomerID", customer.CustomerId,
                        dbType: DbType.String,
                        direction: ParameterDirection.Input);
                parameters.Add("@CompanyName", customer.CompanyName,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@ContactName", customer.ContactName,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@ContactTitle", customer.CustomerId,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Address", customer.Address,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@City", customer.City,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Region", customer.Region,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@PostalCode", customer.PostalCode,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Country", customer.Country,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Phone", customer.Phone,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);
                parameters.Add("@Fax", customer.Fax,
                       dbType: DbType.String,
                       direction: ParameterDirection.Input);


                var responseSp = await connection.ExecuteAsync(storeSp, param: parameters, commandType: CommandType.StoredProcedure);
                return responseSp > 0;

            }
        }

        public async Task<bool> DeleteAsync(string customerId) 
        {
            using (var connection = _connectionFactory.GetDbConnection) 
            {
                var storeSp = "CustomersDelete";
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerID", customerId,
                        dbType: DbType.String,
                        direction: ParameterDirection.Input);

                var responseSp = await connection.ExecuteAsync(storeSp, param: parameters, commandType: CommandType.StoredProcedure);
                return responseSp > 0;
            }
        }

        public async Task<Customers> GetAsync(string customerId) 
        {
            using (var connection = _connectionFactory.GetDbConnection) 
            {                
                var storeSp = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerID", customerId,
                        dbType: DbType.String,
                        direction: ParameterDirection.Input);
                var responseSp = await connection.QuerySingleAsync<Customers>(storeSp, param: parameters, commandType: CommandType.StoredProcedure);
                return responseSp;
            }
        }

        public async Task<IEnumerable<Customers>> GetAllAsync() 
        {
            using (var connection = _connectionFactory.GetDbConnection) 
            {
                var storeSp = "CustomersList";

                var responseSp = await connection.QueryAsync<Customers>(storeSp, commandType: CommandType.StoredProcedure);
                return responseSp;
            }
        } 

        #endregion
    }
}
