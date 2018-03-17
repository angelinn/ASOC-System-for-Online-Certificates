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
using SusiAPI.Web.Services;
using Microsoft.AspNetCore.Authorization;
using SusiAPI.Web.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SusiAPI.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AffirmationController : Controller
    {
        private readonly SessionManager sessionManager;

        public AffirmationController(SessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> GetStudentInfo([FromBody]LoginViewModel login)
        {
            if (!sessionManager.TryGetSession(login.Username, out SusiSession susiService))
                return Unauthorized();

            StudentInfo info = await susiService.GetStudentInfoAsync();
            return new SusiAPIResponse(StatusCodes.Status200OK, new SusiAPIResponseObject
            {
                ResponseCode = SusiAPIResponseCode.Success,
                Message = "OK",
                Data = info
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
