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
        }

        private void getDoc_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
