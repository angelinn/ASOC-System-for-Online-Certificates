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
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SusiAPI.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AffirmationController : Controller
    {
        private readonly SessionManager sessionManager;
        private readonly ILogger<AffirmationController> logger;

        public AffirmationController(ILogger<AffirmationController> logger, SessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
            this.logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetStudentInfo([FromQuery]string number = null)
        {
            Claim usernameClaim = User.FindFirst("username");
            if (!sessionManager.TryGetSession(usernameClaim.Value, out SessionEntry sessionEntry))
                return Unauthorized();

            StudentInfo info = await sessionEntry.Session.GetStudentInfoAsync(number);
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
            {
                logger.LogInformation($"{usernameClaim.Value} has requested certificate, but session has expired.");
                return Unauthorized();
            }

            StudentInfo studentInfo = sessionEntry.StudentInfo;
            studentInfo.Reason = reason;

            logger.LogInformation($"Generating {reason} certificate for {usernameClaim.Value}, fn {studentInfo.FacultyNumber}");

            return new SusiAPIResponse(StatusCodes.Status200OK, new SusiAPIResponseObject
            {
                ResponseCode = SusiAPIResponseCode.Success,
                Message = "Certificate generated.",
                Data = CertificateService.GetCertificate(studentInfo)
            });
        }
    }
}
