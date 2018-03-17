using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SusiAPI.Web
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
    }
}
