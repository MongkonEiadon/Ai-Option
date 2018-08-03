using System.ComponentModel.DataAnnotations;
using iqoption.core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace ai.option.web.ViewModels {
    public class IqOptionRequestViewModel {
        [JsonProperty("EmailAddress")]
        [Required(ErrorMessage = "IqOption username required!")]
        [EmailAddress(ErrorMessage = "Please enter valid email address!")]
        [Display(Prompt = "Username")]
        public string EmailAddress { get; set; }

        [JsonProperty("Password")]
        [Required(ErrorMessage = "IqOption Password required!")]
        [Display(Prompt = "Password")]
        public string Password { get; set; }

        public IqOptionProfileResponseViewModel ProfileResponseViewModel { get; set; }

        public string Temp { get; set; }
        public bool IsPassed { get; set; }


        public string[] ErrorEmail { get; set; }

        public string[] ErrorPassword { get; set; }

    }

    public class LoginViewModel {
        [Display(Description = "Username", Prompt = "example@email.com")]
        [Required(ErrorMessage = "Your unique email to login app")]
        [EmailAddress(ErrorMessage = "Username must be in email format")]
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