using System;
using System.Collections.Generic;

namespace iqoption.web.Models {
    public class DashboardViewModel {
        
        public UserViewModel LogInUser { get; set; }
      
    }

    public class IqOptionUserViewModel {

        public string IqOptionEmail { get; set; }
        public string IqOptionPassword { get; set; }
        public bool IsEnable { get; set; }
    }

    public class UserViewModel {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}