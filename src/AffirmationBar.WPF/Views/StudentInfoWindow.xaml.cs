using AffirmationBar.ViewModels;
using SusiAPICommon.Models;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace AffirmationBar.WPF.Views
{
	/// <summary>
	/// Interaction logic for StudentInfoWindow.xaml
	/// </summary>
	public partial class StudentInfoWindow : Window
	{
        LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();

        public CertificateOptionsViewModel certificateOptions { get; set; } 
		public StudentInfoWindow(StudentInfo studentInfo)
		{
            InitializeComponent();

            certificateOptions = new CertificateOptionsViewModel(studentInfo);

            this.DataContext = certificateOptions;
        }

        private async Task GetCertificate()
        {
            WebClient wb = new WebClient();
            wb.DownloadFile("https://cdn-ssl.img.disneystore.com/content/ds/skyway/2014/category/full/fwb_frozen_20140110.jpg", @"D:\\test.jpeg");
            string studentInfo = await LoginViewModel.GetCertificate();
        }

        private async void getDoc_Click(object sender, RoutedEventArgs e)
        {
            await GetCertificate();
        }

        
    }
}
