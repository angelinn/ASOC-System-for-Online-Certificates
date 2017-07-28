using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SusiAPI.Web.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SusiAPI.Web.Controllers
{
    [Route("api/[controller]")]
    public class AffirmationController : Controller
    {
        private SusiService susiService;

        public AffirmationController(SusiService susiService)
        {
            this.susiService = susiService;
        }
        
        [HttpPost]
        public async Task<IActionResult> GetAffirmation([FromBody]LoginViewModel login)
        {
            if (await susiService.LoginAsync(login.Username, login.Password))
                return Ok();

            return Ok();
        }
    }
}
