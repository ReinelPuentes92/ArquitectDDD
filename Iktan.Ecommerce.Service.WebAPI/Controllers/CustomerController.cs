using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

using Iktan.Ecommerce.App.DTO;
using Iktan.Ecommerce.App.Interface;

namespace Iktan.Ecommerce.Service.WebAPI.Controllers
{
    [System.Web.Http.Route("[controller]/[action]")]
    [ApiController]    
    public class CustomerController : Controller
    {
        private readonly ICustomerApplication _customerApplication;

        public CustomerController(ICustomerApplication customerApplication)
        {
            _customerApplication = customerApplication;
        }

        #region Metodos Sincronos

        [System.Web.Http.HttpPost]
        public IActionResult ResgistryCustomer([System.Web.Http.FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
                return BadRequest();
            var response = _customerApplication.Insert(customerDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }

        [System.Web.Http.HttpPut]
        public IActionResult RefreshCustomer([System.Web.Http.FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
                return BadRequest();
            var response = _customerApplication.Update(customerDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [System.Web.Http.HttpDelete]
        
        public IActionResult DiseableCustomer(string CustomerId)
        {
            if(String.IsNullOrEmpty(CustomerId))
                return BadRequest();
            var response = _customerApplication.Delete(CustomerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [System.Web.Http.HttpGet]
        public IActionResult CustomerSearch(string CustomerId)
        {
            if(String.IsNullOrEmpty(CustomerId))
                return BadRequest();
            var response = _customerApplication.Get(CustomerId);
            if(response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [System.Web.Http.HttpGet]
        public IActionResult CustomerReport()
        {
            var response = _customerApplication.GetAll();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        #endregion

    }
}
