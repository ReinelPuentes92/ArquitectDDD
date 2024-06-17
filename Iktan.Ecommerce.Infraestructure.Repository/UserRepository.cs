using Dapper;
using Iktan.Ecommerce.Domain.Entity;
using Iktan.Ecommerce.Infraestructure.Interface;
using Iktan.Ecommerce.Transversal.Common;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iktan.Ecommerce.Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Users> Authenticate(string userName, string password)
        {
            using (var connection = _connectionFactory.GetDbConnection)
            {
                var storeSp = "[dbo].[UsersGetByUserAndPassword]";

                var parameters = new DynamicParameters();
                parameters.Add("@UserName", userName,
                    dbType: DbType.String,
                    direction: ParameterDirection.Input);
                parameters.Add("@Password", password,
                    dbType: DbType.String,
                    direction: ParameterDirection.Input);

                var responseSp = await connection.QuerySingleAsync<Users>(storeSp, param: parameters, commandType: CommandType.StoredProcedure);
                return responseSp;
            }
        }

    }
}
