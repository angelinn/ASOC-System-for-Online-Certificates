using SusiAPI.Models;
using SusiAPI.Parser;
using System;
using System.Threading.Tasks;

namespace SusiAPI
{
    public class SusiFetcher
    {
        private ISusiParser parser;
        public SusiFetcher(ISusiParser parser)
        {
            this.parser = new ClassicSusiParser();
        }
        
        public async Task<bool> Login(string username, string password)
        {
            return await parser.LoginAsync(username, password);
        }
        
        public bool IsCurrentlyAStudent()
        {
            if (!parser.Authenticated)
                throw new Exception("Use login method first.");

            return parser.IsCurrentlyAStudent();
        }

        public StudentInfo GetStudentInfo()
        {
            if (!parser.Authenticated)
                throw new Exception("Use login method first.");

            return parser.GetStudentInfo();
        }
    }
}
