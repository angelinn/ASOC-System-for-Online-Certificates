using SusiAPI.Models;
using SusiAPI.Parser;
using System;
using System.Threading.Tasks;

namespace SusiAPI
{
    public class SusiService
    {
        private ISusiParser parser;
        public SusiService(ISusiParser parser = null)
        {
            this.parser = parser ?? new ClassicSusiParser();
        }
        
        public async Task<bool> LoginAsync(string username, string password)
        {
            return await parser.LoginAsync(username, password);
        }
        
        public bool IsCurrentlyAStudent()
        {
            if (!parser.Authenticated)
                throw new Exception("Use login method first.");

            return parser.IsCurrentlyAStudent();
        }

        public async Task<StudentInfo> GetStudentInfoAsync()
        {
            if (!parser.Authenticated)
                throw new Exception("Use login method first.");

            return await parser.GetStudentInfoAsync();
        }
    }
}
