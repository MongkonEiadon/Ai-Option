using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using iqoption.web.Models;

namespace iqoption.web.Validations
{
    public class LoginViewModelValidation : AbstractValidator<LoginViewModel>
    {

        public LoginViewModelValidation()
        {
            RuleFor(m => m.Email).EmailAddress().WithMessage("รูปแบบ email ไม่ถูกต้อง");
            RuleFor(m => m.Email).NotEmpty().WithMessage("กรุณาระบุ email");
            RuleFor(m => m.Email).NotNull().WithMessage("กรุณาระบุ email");
            RuleFor(m => m.Password).NotNull().WithMessage("กรุณาระบุรหัสผ่าน");
            RuleFor(m => m.Password).Length(5, 15).WithMessage("กรุณาระบุรหัสผ่านตั้งแต่ 5 ถึง 15 ตัวอักษร");
        }
    }

    public class RegisterViewModelValidation : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidation()
        {
            RuleFor(m => m.Email).EmailAddress().WithMessage("รูปแบบ email ไม่ถูกต้อง");
            RuleFor(m => m.Email).NotEmpty().WithMessage("กรุณาระบุ email");
            RuleFor(m => m.Email).NotNull().WithMessage("กรุณาระบุ email");
            RuleFor(m => m.Password).NotNull().WithMessage("กรุณาระบุรหัสผ่าน");
            RuleFor(m => m.Password).Length(5, 15).WithMessage("กรุณาระบุรหัสผ่านตั้งแต่ 5 ถึง 15 ตัวอักษร");
        }
    }
}
