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
            lines = Regex.Replace(lines, "{STUDY_YEAR}", studentInfo.Year.ToString());
            lines = Regex.Replace(lines, "{SEM_TYPE}", studentInfo.Semester.Type.ToString());
            lines = Regex.Replace(lines, "{YEAR}", studentInfo.Year.ToString());
            lines = Regex.Replace(lines, "{OKS}", studentInfo.Degree);
            lines = Regex.Replace(lines, "{TYPE}", studentInfo.FormOfEducation.ToString());
            lines = Regex.Replace(lines, "{PROGRAM_LINE_1}", studentInfo.Program);
            lines = Regex.Replace(lines, "{FACULTY_NUM}", studentInfo.FacultyNumber.ToString());
            lines = Regex.Replace(lines, "{FACULTY_NAME}", studentInfo.FacultyName);


            return lines;
        }
    }
}
   