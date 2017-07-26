using SusiAPI.Models;
using System;

namespace SusiAPI
{
    public class SusiFetcher
    {
        private ISusiParser parser;
        public SusiFetcher(ISusiParser parser)
        {
            this.parser = new ClassicSusiParser();
        }

        public bool Login(string username, string password)
        {
            return parser.Login(username, password);
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
