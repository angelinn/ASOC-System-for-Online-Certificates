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


        public string FacultyName => this.studentInfo.FacultyName;
        public string Names => this.studentInfo.Names;

        public string EGN => this.studentInfo.EGN;

        public string City => this.studentInfo.City;

        public string Region => this.studentInfo.Region;

        public string Gender => this.studentInfo.Gender;

        public Semester Semester => this.studentInfo.Semester;

        //public string Year => this.studentInfo.Year;

        public string FormOfEducation => this.studentInfo.FormOfEducation;

        public string Degree => this.studentInfo.Degree;

        public string Program => this.studentInfo.Program;

        public int FacultyNumber => this.studentInfo.FacultyNumber;

        public string Reason => this.studentInfo.Reason;

    }
}
