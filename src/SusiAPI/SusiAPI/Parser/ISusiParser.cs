using SusiAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SusiAPI.Parser
{
    public interface ISusiParser
    {
        Task<bool> LoginAsync(string username, string password);
        bool Authenticated { get; }
        bool IsCurrentlyAStudent();
        Task<StudentInfo> GetStudentInfoAsync();
    }
}
