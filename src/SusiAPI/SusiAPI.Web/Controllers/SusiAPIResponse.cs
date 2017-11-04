using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SusiAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SusiAPI.Web.Controllers
{
    public class SusiAPIResponse : IActionResult
    {
        private int? statusCode;
        private object responseObject;

        public SusiAPIResponse(int? statusCode, object responseObject)
        {
            this.statusCode = statusCode;
            this.responseObject = responseObject;
        }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            await (new ObjectResult(responseObject)
            {
                StatusCode = statusCode
            }).ExecuteResultAsync(context);
        }
    }
}
