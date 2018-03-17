using System;
using System.Collections.Generic;
using System.Text;

namespace SusiAPI.Web
{
    public class ServiceManager
    {
        public static IServiceProvider Provider { get; private set; }

        public static void SetProvider(IServiceProvider provider)
        {
            Provider = provider;
        }
    }
}
