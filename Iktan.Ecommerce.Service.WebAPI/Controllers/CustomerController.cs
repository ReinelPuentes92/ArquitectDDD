using Iktan.Ecommerce.App.DTO;
using Iktan.Ecommerce.App.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iktan.Ecommerce.Service.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]    
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;

        public CustomerController(ICustomerApplication customerApplication)
        {
            _customerApplication = customerApplication;
        }

        #region Metodos Sincronos

        //[HttpPost]
        //public IActionResult ResgistryCustomer([System.Web.Http.FromBody] CustomerDTO customerDTO)
        //{
        //    if (customerDTO == null)
        //        return BadRequest();
        //    var response = _customerApplication.Insert(customerDTO);
        //    if (response.IsSuccess)
        //        return Ok(response);

        //    return BadRequest(response.Message);

        //}

        //[HttpPut]
        //public IActionResult RefreshCustomer([System.Web.Http.FromBody] CustomerDTO customerDTO)
        //{
        //    if (customerDTO == null)
        //        return BadRequest();
        //    var response = _customerApplication.Update(customerDTO);
        //    if (response.IsSuccess)
        //        return Ok(response);

        //    return BadRequest(response.Message);
        //}

        //[HttpDelete("{CustomerId}")]

        //public IActionResult DiseableCustomer(string CustomerId)
        //{
        //    if (String.IsNullOrEmpty(CustomerId))
        //        return BadRequest();
        //    var response = _customerApplication.Delete(CustomerId);
        //    if (response.IsSuccess)
        //        return Ok(response);

        //    return BadRequest(response.Message);
        //}

        //[HttpGet("{CustomerId}")]
        //public IActionResult CustomerSearch(string CustomerId)
        //{
        //    if (String.IsNullOrEmpty(CustomerId))
        //        return BadRequest();
        //    var response = _customerApplication.Get(CustomerId);
        //    if (response.IsSuccess)
        //        return Ok(response);

        //    return BadRequest(response.Message);
        //}

        //[HttpGet]
        //public IActionResult CustomerReport()
        //{
        //    var response = _customerApplication.GetAll();
        //    if (response.IsSuccess)
        //        return Ok(response);

        //    return BadRequest(response.Message);
        //}

        #endregion

        #region "Metodos Asincronos"

        [HttpPost]
        public async Task<IActionResult> RegistryCustomerAsync([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
                return BadRequest();
            var response = await _customerApplication.InsertAsync(customerDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> RefreshCustomerAsync([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
                return BadRequest();
            var response = await _customerApplication.UpdateAsync(customerDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpDelete("{CustomerId}")]
        public async Task<IActionResult> DiseableCustomerAsync(string CustomerId)
        {
            if (String.IsNullOrEmpty(CustomerId))
                return BadRequest();
            var response = await _customerApplication.DeleteAsync(CustomerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> CustomerSearchAsync(string CustomerId)
        {
            if (String.IsNullOrEmpty(CustomerId))
                return BadRequest();
            var response = await _customerApplication.GetAsync(CustomerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerReportAsync()
        {
            var response = await _customerApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
        #endregion
    }
}
