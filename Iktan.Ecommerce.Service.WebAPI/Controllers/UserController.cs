﻿using Iktan.Ecommerce.App.DTO;
using Iktan.Ecommerce.App.Interface;
using Iktan.Ecommerce.Service.WebAPI.Helpers;
using Iktan.Ecommerce.Transversal.Common;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System.Text.Encodings.Web;


namespace Iktan.Ecommerce.Service.WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly AppSettings _appSettings;

        public UserController(IUserApplication userApplication, IOptions<AppSettings> appSettings)
        {
            _userApplication = userApplication;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UserAccess([FromBody] UserDTO userDTO)
        {
            var response = await _userApplication.Authenticate(userDTO.UserName, userDTO.Password);
            if (response.IsSuccess)
            {
                if(response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);
                } else
                {
                    return NotFound(response.Message);
                }
            }

            return BadRequest(response.Message);
        }

        private string BuildToken(Response<UserDTO> userDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDto.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
