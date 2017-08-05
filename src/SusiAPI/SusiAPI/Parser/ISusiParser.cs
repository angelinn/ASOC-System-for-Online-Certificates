﻿using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SusiAPI.Parser
{
    public interface ISusiParser
    {
        Task<bool> LoginAsync(string username, string password);
        bool IsAuthenticated { get; }
        bool IsCurrentlyAStudent { get; }
        Task<StudentInfo> GetStudentInfoAsync();
    }
}
