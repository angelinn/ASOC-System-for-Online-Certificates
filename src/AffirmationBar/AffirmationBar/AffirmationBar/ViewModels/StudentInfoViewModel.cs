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
    }
}
