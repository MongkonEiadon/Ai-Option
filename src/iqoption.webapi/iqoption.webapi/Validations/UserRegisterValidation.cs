using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using iqoption.webapi.ViewModels;

namespace iqoption.webapi.Validations
{
    public class UserRegisterValidation  : AbstractValidator<LoginViewModel> {

        public UserRegisterValidation()
        {
            RuleFor(m => m.Email).EmailAddress();
            RuleFor(m => m.Email).NotNull().NotEmpty();
            RuleFor(m => m.Password).NotNull();
            RuleFor(m => m.Password).Length(5, 15);
        }
    }
}
