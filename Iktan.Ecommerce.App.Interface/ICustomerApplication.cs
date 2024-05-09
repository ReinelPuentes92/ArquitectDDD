﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Iktan.Ecommerce.App.DTO;
using Iktan.Ecommerce.Transversal.Common;

namespace Iktan.Ecommerce.App.Interface
{
    public interface ICustomerApplication
    {
        #region Metodos Sincornos

        Response<bool> Insert(CustomerDTO customerDTO);
        Response<bool> Update(CustomerDTO customerDTO);
        Response<bool> Delete(string CustomerId);
        Response<CustomerDTO> Get(string CustomerId);
        Response<IEnumerable<CustomerDTO>> GetAll();

        #endregion

        #region Metodos Asincronos

        Task<Response<bool>> InsertAsync(CustomerDTO customerDTO);
        Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO);
        Task<Response<bool>> DeleteAsync(string CustomerId);
        Task<Response<CustomerDTO>> GetAsync(string CustomerId);
        Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();

        #endregion
    }
}
