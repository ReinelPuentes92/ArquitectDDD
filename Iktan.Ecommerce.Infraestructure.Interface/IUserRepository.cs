using Iktan.Ecommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iktan.Ecommerce.Infraestructure.Interface
{
    public interface IUserRepository
    {
        Task<Users> Authenticate(string userName, string password);
    }
}
