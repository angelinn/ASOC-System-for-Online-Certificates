using System;
using System.Collections.Generic;
using System.Text;
using SusiAPI.Models;
using System.Net.Http;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace SusiAPI.Parser
{
    public class ClassicSusiParser : ISusiParser
    {
        private static readonly string SUSI_URL = "https://susi.uni-sofia.bg/";
        private static readonly string LOGIN_URL = $"{SUSI_URL}ISSU/forms/Login.aspx";

        private const string USERNAME_KEY = "txtUserName";
        private const string PASSWORD_KEY = "txtPassword";

        private HttpClient client;
        private HttpClientHandler handler;

        private bool isAuthenticated;
        public bool IsAuthenticated
        {
            get
            {
                return isAuthenticated;
            }
        }

        public ClassicSusiParser()
        {
            handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
            client = new HttpClient(handler);
        }

        public bool Authenticated => throw new NotImplementedException();

        public Task<StudentInfo> GetStudentInfoAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsCurrentlyAStudent()
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> LoginAsync(string username, string password)
        {
            HtmlDocument rootDocument = new HtmlWeb().Load(SUSI_URL);
            HtmlNodeCollection nodes = rootDocument.DocumentNode.SelectNodes("//input");

            Dictionary<string, string> formData = new Dictionary<string, string>();
            foreach (HtmlNode node in nodes)
                formData.Add(node.Id.TrimStart('\r', '\n'), (node.Attributes["value"] == null) ? String.Empty : node.Attributes["value"].Value);
            
            formData[USERNAME_KEY] = username;
            formData[PASSWORD_KEY] = password;

            HttpResponseMessage response = await client.PostAsync(LOGIN_URL, new FormUrlEncodedContent(formData));
            string stringResult = await response.Content.ReadAsStringAsync();

            // Insert logic to confirm login 
            isAuthenticated = true;
            return isAuthenticated;
        }
    }
}
