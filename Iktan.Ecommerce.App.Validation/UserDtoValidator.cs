using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Iktan.Ecommerce.App.DTO;

namespace Iktan.Ecommerce.App.Validation
{
    public class UserDtoValidator :AbstractValidator<UserDTO>
    {

        public UserDtoValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().NotNull();
            RuleFor(u => u.Password).NotEmpty().NotNull();
        }

    }
}
