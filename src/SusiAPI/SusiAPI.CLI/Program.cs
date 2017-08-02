using Newtonsoft.Json;
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
            HttpClient client = new HttpClient();
            string result = client.PostAsync("http://localhost:49953/api/affirmation", new StringContent(
                                       JsonConvert.SerializeObject(new { Username = "", Password = "" }), 
                                                                   Encoding.UTF8, "application/json"))
                                   .Result.Content.ReadAsStringAsync().Result;

            File.WriteAllText("D:\\temp.txt", result);
        }
    }
}