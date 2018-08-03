using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ai.option.web.ViewModels.Account
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string RoleName { get; set; }
        public int NumberOfIqAccounts { get; set; }
    }
}
