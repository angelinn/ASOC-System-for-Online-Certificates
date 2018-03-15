using Newtonsoft.Json;
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

            SusiService susi = new SusiService();
            susi.LoginAsync(username, password).Wait();

            StudentInfo studentInfo = susi.GetStudentInfoAsync().Result;
            File.WriteAllText("D:\\cert.html", CertificateService.GetCertificate(studentInfo).Content);
        }
    }
}