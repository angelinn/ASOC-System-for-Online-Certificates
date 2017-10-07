using AffirmationBar.ViewModels;
using Microsoft.Win32;
using SusiAPICommon.Models;
using System.IO;
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

        public CertificateOptionsViewModel certificateOptions { get; set; }

        public StudentInfoWindow(StudentInfo studentInfo)
        {
            InitializeComponent();

            certificateOptions = new CertificateOptionsViewModel(studentInfo);

            this.DataContext = certificateOptions;
        }



        private async void getDoc_Click(object sender, RoutedEventArgs e)
        {
            byte[] certificate = await certificateOptions.GetCertificateAsync();

            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "HTML Files (*.html)|*.html";
            openFileDialog.AddExtension = true;

            if (openFileDialog.ShowDialog() == true)
            {
                File.WriteAllBytes(openFileDialog.FileName, certificate);
            }
        }


    }
}
