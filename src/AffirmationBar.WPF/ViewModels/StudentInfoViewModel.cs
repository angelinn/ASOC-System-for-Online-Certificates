using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SusiAPICommon.Models;


namespace AffirmationBar.WPF.ViewModels
{
	class StudentInfoViewModel: BaseViewModel
	{
		private StudentInfo studentInfo;
		public StudentInfoViewModel(StudentInfo studentInfo)
		{
			this.studentInfo = studentInfo;
		}
	}
}
