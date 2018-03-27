using SusiAPI.Parser;
using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SusiAPI
{
    public class SusiSession
    {
        private ISusiParser parser;
        public SusiSession(ISusiParser parser = null)
        {
            this.parser = parser ?? new ClassicSusiParser();
        }
        
        public async Task<bool> LoginAsync(string username, string password)
        {
            return await parser.LoginAsync(username, password);
        }
        
        public bool IsCurrentlyAStudent()
        {
            if (!parser.IsAuthenticated)
                throw new Exception("Use login method first.");

            return parser.IsCurrentlyAStudent;
        }

        public async Task<StudentInfo> GetStudentInfoAsync()
        {
            if (!parser.IsAuthenticated)
                throw new Exception("Use login method first.");

            return await parser.GetStudentInfoAsync();
        }

        public async Task<List<string>> GetStudentRolesAsync()
        {
            if (!parser.IsAuthenticated)
                throw new Exception("Use login method first.");

            return await parser.GetStudentRolesAsync();
        }
    }
}
