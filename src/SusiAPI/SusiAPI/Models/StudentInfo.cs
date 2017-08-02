using System;
using System.Collections.Generic;
using System.Text;

namespace SusiAPI.Models
{
    public class StudentInfo
    {
        public string Faculty { get; set; }
        public string Names { get; set; }
        public string EGN { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Gender { get; set; }
        public Semester Semester { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public StudyYear Year { get; set; }
        public string FormOfEducation { get; set; }
        public string Degree { get; set; }
        public string Program { get; set; }
        public int FacultyNumber { get; set; }
        public string Reason { get; set; }
    }
}
