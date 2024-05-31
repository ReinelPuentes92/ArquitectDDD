using Iktan.Ecommerce.App.DTO;
using Iktan.Ecommerce.Transversal.Common;

namespace Iktan.Ecommerce.App.Interface
{
    public interface IUserApplication
    {
        Task<Response<UserDTO>> Authenticate(string userName, string password);
    }
}
