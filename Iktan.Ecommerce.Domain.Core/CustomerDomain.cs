using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Iktan.Ecommerce.Domain.Entity;
using Iktan.Ecommerce.Domain.Interface;
using Iktan.Ecommerce.Infraestructure.Interface;


namespace Iktan.Ecommerce.Domain.Core
{
    public class CustomerDomain : ICustomerDomain
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerDomain(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        #region Metodos Sincronos

        public bool Insert(Customers customers)
        {
            return (_customerRepository.Insert(customers));
        }

        public bool Update(Customers customers) 
        {
            return (_customerRepository.Update(customers));
        }

        public bool Delete(string customerId) 
        {
            return (_customerRepository.Delete(customerId));
        }

        public Customers Get(string customerId)
        {
            return (_customerRepository.Get(customerId));
        }

        public IEnumerable<Customers> GetAll() 
        {
            return (_customerRepository.GetAll());
        }

        #endregion

        #region Metodo Asincrono

        public async Task<bool> InsertAsync(Customers customers) 
        {
            return (await _customerRepository.InsertAsync(customers));
        }

        public async Task<bool> UpdateAsync(Customers customers) 
        {
            return (await _customerRepository.UpdateAsync(customers));
        }

        public async Task<bool> DeleteAsync(string customerId) 
        {
            return (await _customerRepository.DeleteAsync(customerId));
        }

        public async Task<Customers> GetAsync(string customerId) 
        {
            return (await _customerRepository.GetAsync(customerId));
        }

        public async Task<IEnumerable<Customers>> GetAllAsync() 
        {
            return (await _customerRepository.GetAllAsync());
        }

        #endregion


    }
}
