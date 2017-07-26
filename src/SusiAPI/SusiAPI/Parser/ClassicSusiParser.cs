using System;
using System.Collections.Generic;
using System.Text;
using SusiAPI.Models;
using System.Net.Http;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace SusiAPI.Parser
{
    public class ClassicSusiParser : ISusiParser
    {
        private static string SUSI_URL = "https://susi.uni-sofia.bg/";
        private static string LOGIN_URL = $"{SUSI_URL}ISSU/forms/Login.aspx";

        private const string USERNAME_KEY = "txtUserName";
        private const string PASSWORD_KEY = "txtPassword";
        public bool Authenticated => throw new NotImplementedException();

        public StudentInfo GetStudentInfo()
        {
            throw new NotImplementedException();
        }

        public bool IsCurrentlyAStudent()
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            CookieContainer cookies = new CookieContainer();

            using (HttpClientHandler handler = new HttpClientHandler())
            using (HttpClient client = new HttpClient(handler))
            {
                handler.CookieContainer = cookies;

                HtmlDocument rootDocument = new HtmlWeb().Load(SUSI_URL);
                HtmlNodeCollection nodes = rootDocument.DocumentNode.SelectNodes("//input");

                Dictionary<string, string> formData = new Dictionary<string, string>();
                foreach (HtmlNode node in nodes)
                    formData.Add(node.Id.TrimStart('\r', '\n'), (node.Attributes["value"] == null) ? String.Empty : node.Attributes["value"].Value);

                formData[USERNAME_KEY] = username;
                formData[PASSWORD_KEY] = password;

                string result = client.PostAsync(LOGIN_URL, new FormUrlEncodedContent(formData)).Result.Content.ReadAsStringAsync().Result;
                File.WriteAllText("file.txt", result);
            }
            return true;
        }
    }
}
