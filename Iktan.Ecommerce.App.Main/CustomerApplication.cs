using AutoMapper;

using Iktan.Ecommerce.App.DTO;
using Iktan.Ecommerce.App.Interface;
using Iktan.Ecommerce.Domain.Entity;
using Iktan.Ecommerce.Domain.Interface;
using Iktan.Ecommerce.Transversal.Common;

namespace Iktan.Ecommerce.App.Main
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomerDomain _domain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CustomerApplication> _logger;

        public CustomerApplication(ICustomerDomain domain, IMapper mapper, IAppLogger<CustomerApplication> logger)
        {
            _domain = domain;
            _mapper = mapper;
            _logger = logger;
        }

        #region Metodos Asincronos

        public async Task<Response<bool>> InsertAsync(CustomerDTO customerDTO)
        {

            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customerDTO);
                response.Data = await _domain.InsertAsync(customer);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Registrado !!!";
                }

            } catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();

            try
            {
                var customer = _mapper.Map<Customers>(customerDTO);
                response.Data = await _domain.UpdateAsync(customer);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualizacion Exitosa !!!";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string CustomerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _domain.DeleteAsync(CustomerId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminacion Exitosa !!!";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<CustomerDTO>> GetAsync(string CustomerId)
        {
            var response = new Response<CustomerDTO>();
            try
            {
                var customer = await _domain.GetAsync(CustomerId);
                response.Data = _mapper.Map<CustomerDTO>(customer);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Satisfactoria !!!";
                    _logger.LogInformation("Consulta Satisfactoria !!!");
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();
            try
            {
                var customer = await _domain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customer);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Datos Cargados !!!";
                    _logger.LogInformation("Datos Cargados !!!");
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }

            return response;
        }


        #endregion

    }
}
