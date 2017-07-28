using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SusiAPI.Web.ViewModels;
using System.Net;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SusiAPI.Web.Controllers
{
    [Route("api/[controller]")]
    public class AffirmationController : Controller
    {
        private readonly SusiService susiService;

        public AffirmationController(SusiService susiService)
        {
            this.susiService = susiService;
        }
        
        [HttpPost]
        public async Task<IActionResult> GetAffirmation([FromBody]LoginViewModel login)
        {
            if (await susiService.LoginAsync(login.Username, login.Password))
                return new SusiAPIResponse(StatusCodes.Status200OK, new { Result = "Login successful" });
            
            return new SusiAPIResponse(StatusCodes.Status422UnprocessableEntity, new { Result = "Username or password is wrong" });
        }
    }
}
