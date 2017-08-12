using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SusiAPICommon.Models;
using System.Collections.ObjectModel;

namespace AffirmationBar.WPF.ViewModels
{
	public class StudentInfoViewModel: BaseViewModel
	{
        private StudentInfo studentInfo;
        public StudentInfoViewModel(StudentInfo studentInfo)
		{
			this.studentInfo = studentInfo;    
		}


        public string FacultyName => studentInfo.FacultyName;
        public string Names => studentInfo.Names;

        public string EGN => studentInfo.EGN;

        public string City => studentInfo.City;

        public string Region => studentInfo.Region;

        public string Gender => studentInfo.Gender;

        public Semester Semester => studentInfo.Semester;

        public StudyYear Year => studentInfo.Year;

        public string FormOfEducation => studentInfo.FormOfEducation;

        public string Degree => studentInfo.Degree;

        public string Program => studentInfo.Program;

        public int FacultyNumber => studentInfo.FacultyNumber;

        public string Reason => studentInfo.Reason;

    }
}
