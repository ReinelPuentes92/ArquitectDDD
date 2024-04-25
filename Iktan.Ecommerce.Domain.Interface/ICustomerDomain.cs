using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

using Iktan.Ecommerce.Domain.Entity;

namespace Iktan.Ecommerce.Domain.Interface
{
    public interface ICustomerDomain
    {
        #region Metodos Sincronos

        bool Insert(Customers customers);
        bool Update(Customers customers);
        bool Delete(string customerId);

        Customers Get(string customerId);
        IEnumerable<Customers> GetAll();

        #endregion

        #region Metodos Asincronos

        Task<bool> InsertAsync(Customers customers);
        Task<bool> UpdateAsync(Customers customers);
        Task<bool> DeleteAsync(string customerId);

        Task<Customers> GetAsync(string customerId);
        Task<IEnumerable<Customers>> GetAllAsync();

        #endregion
    }
}
