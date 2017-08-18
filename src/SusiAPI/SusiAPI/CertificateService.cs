using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SusiAPI
{
    public static class CertificateService
    {
        private const string TEMPLATE_PATH = "../SusiAPI/Template/certificate_template.html";
        public static string GetCertificate(StudentInfo studentInfo)
        {
            string lines = File.ReadAllText(TEMPLATE_PATH);

            lines = Regex.Replace(lines, "{NAMES}", studentInfo.Names);
            lines = Regex.Replace(lines, "{EGN}", studentInfo.EGN);
            lines = Regex.Replace(lines, "{CITY}", studentInfo.City);
            lines = Regex.Replace(lines, "{REGION}", studentInfo.Region);

            string semesterType = studentInfo.Semester.Type == SemesterType.Summer ? "летен" : "зимен";
            lines = Regex.Replace(lines, "{SEM_TYPE}", semesterType);

            string stringYear = String.Empty;
            switch (studentInfo.Year)
            {
                case StudyYear.First:
                    stringYear = "първи";
                    break;
                case StudyYear.Second:
                    stringYear = "втори";
                    break;
                case StudyYear.Third:
                    stringYear = "трети";
                    break;
                case StudyYear.Fourth:
                    stringYear = "четвърти";
                    break;
            }

            lines = Regex.Replace(lines, "{STUDY_YEAR}", $"20{studentInfo.Semester.Begins}/20{studentInfo.Semester.Ends}");
            lines = Regex.Replace(lines, "{YEAR}", stringYear);
            lines = Regex.Replace(lines, "{OKS}", studentInfo.Degree);

            string formString = studentInfo.FormOfEducation == FormOfEducation.Regular ? "редовно" : "задочно";
            lines = Regex.Replace(lines, "{TYPE}", formString);
            lines = Regex.Replace(lines, "{PROGRAM_LINE_1}", studentInfo.Program);
            lines = Regex.Replace(lines, "{FACULTY_NUM}", studentInfo.FacultyNumber.ToString());
            lines = Regex.Replace(lines, "{FACULTY_NAME}", studentInfo.FacultyName);


            return lines;
        }
    }
}
   