using AffirmationBar.ViewModels;
using SusiAPICommon.Models;
using System.Windows;

namespace AffirmationBar.WPF.Views
{
	/// <summary>
	/// Interaction logic for StudentInfoWindow.xaml
	/// </summary>
	public partial class StudentInfoWindow : Window
	{
		public StudentInfoViewModel StudentInfoViewModel { get; set; }
		public StudentInfoWindow(StudentInfo studentInfo)
		{
			InitializeComponent();
			StudentInfoViewModel = new StudentInfoViewModel(studentInfo);
            this.DataContext = StudentInfoViewModel;
            //this.Show();
        }
    }
}
