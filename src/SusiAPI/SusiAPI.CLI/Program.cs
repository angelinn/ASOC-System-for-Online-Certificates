using Newtonsoft.Json;
using SusiAPI.Responses;
using SusiAPICommon.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace SusiAPI.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string username = Console.ReadLine();
            string password = Console.ReadLine();

            SusiSession susi = new SusiSession();
            LoginResponse loginResponse = susi.LoginAsync(username, password).Result;

            string key = null;
            if (loginResponse.HasMultipleRoles)
            {
                foreach (string role in loginResponse.Roles)
                    Console.WriteLine($"Found role {role}");

                key = Console.ReadLine();
            }
            
            
            StudentInfo studentInfo = susi.GetStudentInfoAsync(key).Result;
            File.WriteAllText("D:\\cert.html", CertificateService.GetCertificate(studentInfo).Content);
        }
    }
}