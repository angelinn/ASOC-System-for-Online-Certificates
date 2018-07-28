using System;
using System.Collections.Generic;
using System.Text;

namespace SusiAPI.Responses
{
    public class LoginResponse
    {
        public bool LoggedIn { get; set; }
        public List<string> Roles { get; set; }
        public bool HasMultipleRoles { get; set; }
        public string Token { get; set; }
            

        public LoginResponse(bool loggedIn, bool hasMultipleRoles = false, List<string> roles = null)
        {
            LoggedIn = loggedIn;
            HasMultipleRoles = hasMultipleRoles;
            Roles = roles;
        }
    }
}
