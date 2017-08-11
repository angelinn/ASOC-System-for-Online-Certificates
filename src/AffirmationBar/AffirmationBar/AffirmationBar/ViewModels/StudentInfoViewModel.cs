using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffirmationBar.ViewModels
{
    public class StudentInfoViewModel : BaseViewModel
    {
        private StudentInfo studentInfo;
        public StudentInfoViewModel(StudentInfo studentInfo)
        {
            this.studentInfo = studentInfo;
        }

        public string FacultyName => studentInfo.FacultyName;
        public string Names => studentInfo.Names;
        public string Egn => studentInfo.EGN;
        public string City => studentInfo.City;
        public string Region => studentInfo.Region;
        public string FormOfEducation => studentInfo.FormOfEducation;
        public Semester Semester => studentInfo.Semester;
        public StudyYear Year => studentInfo.Year;
        public string Degree => studentInfo.Degree;
        public string Program => studentInfo.Program;
        public string Reason => studentInfo.Reason;
        public int FacultyNumber => studentInfo.FacultyNumber;
        public string Home => $"{studentInfo.City}, {studentInfo.Region}";
        public string Gender => studentInfo.Gender;
    }
}
