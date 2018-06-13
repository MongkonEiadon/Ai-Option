using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;

namespace ai.option.web.ViewModels
{
    public class IqOptionRequest
    {
        [Required(ErrorMessage = "IqOption username required!")]
        [EmailAddress(ErrorMessage = "Please enter valid email address!")]
        [Display(Prompt = "Username")]
        public string EmailAddress { get; set; }
        
        [Required(ErrorMessage = "IqOption Password required!")]
        [Display(Prompt = "Password")]
        public string Password { get; set; }
    }

    public class LoginViewModel {

        [Display(Description = "Username", Prompt = "example@email.com")]
        [Required (ErrorMessage = "Your unique email to login app")]
        [EmailAddress (ErrorMessage = "Username must be in email format")]
        public string EmailAddress { get; set; }
        
        [Display(Description = "Password", Prompt = "********")]
        [Required(ErrorMessage = "Password required!")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [Display(Description = "Confirm Password", Prompt = "********")]
        public string ConfirmedPassword { get; set; }


        [Display(Description = "Invitation Code")]
        public string InvitationCode { get; set; }
    }
    





}
    