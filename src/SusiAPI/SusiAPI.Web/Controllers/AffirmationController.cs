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
using System.Security.Claims;

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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetStudentInfo()
        {
            Claim usernameClaim = User.FindFirst("username");
            if (!sessionManager.TryGetSession(usernameClaim.Value, out SessionEntry sessionEntry))
                return Unauthorized();

            StudentInfo info = await sessionEntry.Session.GetStudentInfoAsync();
            sessionEntry.StudentInfo = info;

            return new SusiAPIResponse(StatusCodes.Status200OK, new SusiAPIResponseObject
            {
                ResponseCode = SusiAPIResponseCode.Success,
                Message = "OK",
                Data = info
            });
        }

        [HttpGet]
        [Route("Generate")]
        public IActionResult GenerateCertificate(string reason)
        {
            Claim usernameClaim = User.FindFirst("username");
            if (!sessionManager.TryGetSession(usernameClaim.Value, out SessionEntry sessionEntry))
                return Unauthorized();

            StudentInfo studentInfo = sessionEntry.StudentInfo;
            studentInfo.Reason = reason;
            return new SusiAPIResponse(StatusCodes.Status200OK, new SusiAPIResponseObject
            {
                ResponseCode = SusiAPIResponseCode.Success,
                Message = "Certificate generated.",
                Data = CertificateService.GetCertificate(studentInfo)
            });
        }
    }
}
