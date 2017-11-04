using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SusiAPI.Web.ViewModels;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SusiAPICommon.Models;
using SusiAPI.Common.Models;

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
        [Route("")]
        public async Task<IActionResult> GetStudentInfo([FromBody]LoginViewModel login)
        {
            if (await susiService.LoginAsync(login.Username, login.Password))
            {
                StudentInfo info = await susiService.GetStudentInfoAsync();
                return new SusiAPIResponse(StatusCodes.Status200OK, new SusiAPIResponseObject
                {
                    ResponseCode = SusiAPIResponseCode.Success,
                    Message = "OK",
                    Data = info
                });
            }

            return new SusiAPIResponse(StatusCodes.Status422UnprocessableEntity, new SusiAPIResponseObject
            {
                ResponseCode = SusiAPIResponseCode.InvalidCredentials,
                Message = "Username or password is wrong,"
            });
        }

        [HttpPost]
        [Route("Generate")]
        public SusiAPIResponse GetAffirmation([FromBody]StudentInfo studentInfo)
        {
            return new SusiAPIResponse(StatusCodes.Status200OK, new SusiAPIResponseObject
            {
                ResponseCode = SusiAPIResponseCode.Success,
                Message = "Certificate generated.",
                Data = CertificateService.GetCertificate(studentInfo)
            });
        }
    }
}
