using SusiAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SusiAPI
{
    public interface ISusiParser
    {
        bool Login(string username, string password);
        bool Authenticated { get; }
        bool IsCurrentlyAStudent();
        StudentInfo GetStudentInfo();
    }
}
