using Newtonsoft.Json;
using SusiAPI.Common.Models;
using SusiAPI.Responses;
using SusiAPICommon.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SusiAPIClient
{
    public class SusiClient
    {
        private const string API_URL = "https://susiapi.azurewebsites.net"; //"http://localhost:61737"; // "https://susiapi.azurewebsites.net";
        private static readonly string TOKEN_URL = $"{API_URL}/api/token/create";
        private static readonly string STUDENT_INFO_URL = $"{API_URL}/api/affirmation";
        private static readonly string CERTIFICATE_URL = $"{API_URL}/api/affirmation/generate";

        private const string JSON_MEDIA_TYPE = "application/json";

        public string LastError { get; set; }

        private HttpClient client = new HttpClient();
        private bool isAuthenticated;

        public async Task<StudentInfo> GetStudentInfoAsync(string number = null)
        {
            try
            {
                string url = STUDENT_INFO_URL;
                if (!String.IsNullOrEmpty(number))
                    url += $"?number={number}";

                HttpResponseMessage response = await client.GetAsync(url);
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

        public async Task<Certificate> GetCertificate(string reason)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(CERTIFICATE_URL + $"?reason={reason}");
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

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            string json = JsonConvert.SerializeObject(new { Username = username, Password = password });
            HttpResponseMessage response = await client.PostAsync(TOKEN_URL, new StringContent(json, Encoding.UTF8, JSON_MEDIA_TYPE));

            string resJson = await response.Content.ReadAsStringAsync();
            SusiAPIResponseObject responseObject = JsonConvert.DeserializeObject<SusiAPIResponseObject>(resJson);
            if (responseObject.ResponseCode == SusiAPIResponseCode.Success)
            {
                string res = responseObject.Data.ToString();
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(res);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                isAuthenticated = true;
                return loginResponse;
            }

            return null;
        }
    }
}
