using Newtonsoft.Json;
using SusiAPI.Common.Models;
using SusiAPICommon.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SusiAPIClient
{
    public class SusiClient
    {
        private const string API_URL = "https://susiapi.azurewebsites.net";
        private static readonly string LOGIN_URL = $"{API_URL}/api/login";
        private static readonly string STUDENT_INFO_URL = $"{API_URL}/api/affirmation";
        private static readonly string CERTIFICATE_URL = $"{API_URL}/api/affirmation/generate";

        private const string JSON_MEDIA_TYPE = "application/json";

        public string LastError { get; set; }

        private HttpClient client = new HttpClient();
        private bool isAuthenticated;

        public async Task<StudentInfo> GetStudentInfoAsync(string username, string password)
        {
            try
            {
                string json = JsonConvert.SerializeObject(new { Username = username, Password = password });

                HttpResponseMessage response = await client.PostAsync(STUDENT_INFO_URL, new StringContent(json, Encoding.UTF8, JSON_MEDIA_TYPE));
                string resJson = await response.Content.ReadAsStringAsync();

                SusiAPIResponseObject responseObject = JsonConvert.DeserializeObject<SusiAPIResponseObject>(resJson);
                if (responseObject.ResponseCode == SusiAPIResponseCode.Success)
                    return responseObject.Parse<StudentInfo>();

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        } 

        public async Task<Certificate> GetCertificate(StudentInfo studentInfo)
        {
            try
            {
                string json = JsonConvert.SerializeObject(studentInfo);

                HttpResponseMessage response = await client.PostAsync(CERTIFICATE_URL, new StringContent(json, Encoding.UTF8, JSON_MEDIA_TYPE));
                string resJson = await response.Content.ReadAsStringAsync();

                SusiAPIResponseObject responseObject = JsonConvert.DeserializeObject<SusiAPIResponseObject>(resJson);
                if (responseObject.ResponseCode == SusiAPIResponseCode.Success)
                    return responseObject.Parse<Certificate>();

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
