using AffirmationBar.ViewModels;
using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AffirmationBar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentInfoPage : ContentPage
	{
        public StudentInfoViewModel StudentInfoViewModel { get; set; }
        public StudentInfoPage(StudentInfo studentInfo)
		{
			InitializeComponent();

            StudentInfoViewModel = new StudentInfoViewModel(studentInfo);
            BindingContext = StudentInfoViewModel;
		}
	}
}