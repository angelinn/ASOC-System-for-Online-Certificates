using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SusiAPI.Web.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SusiAPI.Web.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly JwtOptions jwtOptions;
        private readonly SusiService susiService;

        public TokenController(IOptions<JwtOptions> jwtOptions, SusiService susiService)
        {
            this.jwtOptions = jwtOptions.Value;
            this.susiService = susiService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody]LoginViewModel login)
        {
            IActionResult response = Unauthorized();
            if (await susiService.LoginAsync(login.Username, login.Password))
            {
                var tokenString = BuildToken(login.Username, login.Password);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string BuildToken(string username, string password)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jwtOptions.Issuer,
              jwtOptions.Issuer,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
