using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SusiAPI.Common.Models;
using SusiAPI.Web.Models;
using SusiAPI.Web.Services;
using SusiAPI.Web.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SusiAPI.Web.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly JwtOptions jwtOptions;
        private readonly SusiSession susiService;
        private readonly SessionManager sessionManager;
        private readonly ILogger<TokenController> logger;

        public TokenController(IOptions<JwtOptions> jwtOptions, ILogger<TokenController> logger, SusiSession susiService, SessionManager sessionManager)
        {
            this.jwtOptions = jwtOptions.Value;
            this.susiService = susiService;
            this.sessionManager = sessionManager;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody]LoginViewModel login)
        {
            logger.LogInformation($"Request token from user {login.Username}");
            if (await susiService.LoginAsync(login.Username, login.Password))
            {
                logger.LogInformation("Login successful");
                var tokenString = BuildToken(login.Username);
                sessionManager.AddSession(login.Username, susiService);

                return new SusiAPIResponse(StatusCodes.Status200OK, new SusiAPIResponseObject
                {
                    ResponseCode = SusiAPIResponseCode.Success,
                    Data = tokenString
                });
            }

            logger.LogInformation($"Unsuccessful login for user {login.Username}");
            return new SusiAPIResponse(StatusCodes.Status422UnprocessableEntity, new SusiAPIResponseObject
            {
                ResponseCode = SusiAPIResponseCode.InvalidCredentials,
                Message = "Username or password is wrong,"
            });
        }

        private string BuildToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jwtOptions.Issuer,
              jwtOptions.Issuer,
              expires: DateTime.Now.AddMinutes(5),
              signingCredentials: creds,
              claims: new List<Claim>() { new Claim("username", username) });

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
