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
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
    