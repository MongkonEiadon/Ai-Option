using System.ComponentModel.DataAnnotations;

namespace iqoption.web.Models {
    public class RegisterViewModel {
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        
       
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}