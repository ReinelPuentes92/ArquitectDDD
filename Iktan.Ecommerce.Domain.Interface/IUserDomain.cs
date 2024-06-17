using Iktan.Ecommerce.Domain.Entity;

namespace Iktan.Ecommerce.Domain.Interface
{
    public interface IUserDomain
    {
        Task<Users> Authenticate(string userName, string password);
    }
}
