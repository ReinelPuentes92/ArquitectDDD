using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Iktan.Ecommerce.App.DTO;
using Iktan.Ecommerce.App.Interface;
using Iktan.Ecommerce.Domain.Entity;
using Iktan.Ecommerce.Domain.Interface;
using Iktan.Ecommerce.Transversal.Common;
using Iktan.Ecommerce.App.Validation;

namespace Iktan.Ecommerce.App.Main
{
    public class UserApplication : IUserApplication
    {

        private readonly IUserDomain _userDomain;
        private readonly IMapper _mapper;
        private readonly UserDtoValidator _userDtoValidator;

        public UserApplication(IUserDomain userDomain, IMapper mapper, UserDtoValidator userDtoValidator)
        {
            _userDomain = userDomain;
            _mapper = mapper;
            _userDtoValidator = userDtoValidator;
        }

        public async Task<Response<UserDTO>> Authenticate(string userName, string password)
        {
            var response = new Response<UserDTO>();
            var validation = _userDtoValidator.Validate(new UserDTO() { UserName = userName, Password = password });

            if(!validation.IsValid)
            {
                response.Message = "Errores de Validación.";
                response.Errors = validation.Errors;
                return response;
            }
            try
            {
                var user = await _userDomain.Authenticate(userName, password);
                response.Data = _mapper.Map<UserDTO>(user);

                if(response.Data != null)
                { 
                    response.IsSuccess = true;
                    response.Message = "Autenticacion Exitosa !!!";
                }

            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe !!!";
            }
            catch (Exception ex)
            {
                response.IsSuccess= false;
                response.Message = ex.Message;
            }

            return response;
        }

       
    }
}
