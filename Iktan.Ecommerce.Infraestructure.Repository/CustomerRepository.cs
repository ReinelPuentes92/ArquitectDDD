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

        #region metodos sincronos

        public bool Insert(Customers customer) 
        {
            using (var connection = _connectionFactory.GetDbConnection)
            {
                var storeSp = "CustomersInsert";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
	            parameters.Add("Fax", customer.Fax);

                var responseSp = connection.Execute(storeSp, param: parameters, commandType: CommandType.StoredProcedure);
                return responseSp > 0;
            }
        }

        public bool Update(Customers customer) 
        {
            using (var connection = _connectionFactory.GetDbConnection)
            {
                var storeSp = "CustomersUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var responseSp = connection.Execute(storeSp, param: parameters, commandType: CommandType.StoredProcedure);
                return responseSp > 0;
            }
        }

        public bool Delete(string customerId) 
        {
            using (var connection = _connectionFactory.GetDbConnection) 
            {
                var storeSp = "CustomersDelete";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customerId);

                var responseSp = connection.Execute(storeSp, param: parameters, commandType: CommandType.StoredProcedure);
                return responseSp > 0;
            }
        }

        public Customers Get(string customerId) 
        {
            using (var connection = _connectionFactory.GetDbConnection) 
            {
                var storeSp = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customerId);

                var responseSp = connection.QuerySingle<Customers>(storeSp, param: parameters, commandType: CommandType.StoredProcedure);
                return responseSp;
            }
        }

        public IEnumerable<Customers> GetAll() 
        {
            using (var connection = _connectionFactory.GetDbConnection)
            {
                var storeSp = "CustomersList";

                var responseSp = connection.Query<Customers>(storeSp, commandType: CommandType.StoredProcedure);
                return responseSp;
            }
        }

        #endregion

        #region Metodos Asincronos

        public async Task<bool> InsertAsync(Customers customer) 
        {
            using (var connection = _connectionFactory.GetDbConnection) 
            {
                var storeSp = "CustomersInsert";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

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
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

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
                parameters.Add("CustomerID", customerId);

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
                parameters.Add("CustomerID", customerId);

                var responseSp = await connection.QuerySingleAsync<Customers>(storeSp, param: customerId, commandType: CommandType.StoredProcedure);
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
