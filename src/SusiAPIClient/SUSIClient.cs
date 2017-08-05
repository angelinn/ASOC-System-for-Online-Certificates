using Newtonsoft.Json;
using SusiAPICommon.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SusiAPIClient
{
    public class SusiClient
    {
        private const string API_URL = "";
        private static readonly string LOGIN_URL = "";
        private static readonly string STUDENT_INFO_URL = "";

        private const string JSON_MEDIA_TYPE = "application/json";
        private HttpClient client = new HttpClient();

        private bool isAuthenticated;

        public async Task<bool> LoginAsync(string username, string password)
        {
            string json = JsonConvert.SerializeObject(new { Username = username, Password = password });
            HttpResponseMessage response = await client.PostAsync(LOGIN_URL, new StringContent(json, Encoding.UTF8, JSON_MEDIA_TYPE));

            return true;
        }
        
        public async Task<StudentInfo> GetStudentInfoAsync()
        {
            if (!isAuthenticated)
                return null;

            HttpResponseMessage response = await client.GetAsync(STUDENT_INFO_URL);
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<StudentInfo>(json);
        }
    }
}
