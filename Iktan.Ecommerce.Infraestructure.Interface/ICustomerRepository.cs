﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Iktan.Ecommerce.Domain.Entity;

namespace Iktan.Ecommerce.Infraestructure.Interface
{
    public interface ICustomerRepository
    {
        
        #region Metodos Asincronos

        Task<bool> InsertAsync(Customers customers);
        Task<bool> UpdateAsync(Customers customers);
        Task<bool> DeleteAsync(string customerId);

        Task<Customers> GetAsync(string customerId);
        Task<IEnumerable<Customers>> GetAllAsync();

        #endregion
    }
}
