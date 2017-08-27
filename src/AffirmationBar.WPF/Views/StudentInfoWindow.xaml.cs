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
        public CertificateOptionsViewModel certificateOptions { get; set; } 
		public StudentInfoWindow(StudentInfo studentInfo)
		{
            InitializeComponent();

            certificateOptions = new CertificateOptionsViewModel(studentInfo);

            this.DataContext = certificateOptions;
        }

        private void getDoc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(certificateOptions.Student.Reason);
        }
    }
}
