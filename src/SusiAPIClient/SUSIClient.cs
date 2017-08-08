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
        private const string API_URL = "http://susiapi.azurewebsites.net";
        private static readonly string LOGIN_URL = $"{API_URL}/api/login";
        private static readonly string STUDENT_INFO_URL = $"{API_URL}/api/affirmation";

        private const string JSON_MEDIA_TYPE = "application/json";

        private HttpClient client = new HttpClient();
        private bool isAuthenticated;

        public async Task<bool> LoginAsync(string username, string password)
        {
            string json = JsonConvert.SerializeObject(new { Username = username, Password = password });
            HttpResponseMessage response = await client.PostAsync(LOGIN_URL, new StringContent(json, Encoding.UTF8, JSON_MEDIA_TYPE));

            return true;
        }
        
        public async Task<StudentInfo> GetStudentInfoAsync(string username, string password)
        {

            try
            {
                string json = JsonConvert.SerializeObject(new { Username = username, Password = password });

                HttpResponseMessage response = await client.PostAsync(STUDENT_INFO_URL, new StringContent(json, Encoding.UTF8, JSON_MEDIA_TYPE));
                string resJson = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<StudentInfo>(resJson);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
