using AffirmationBar.Services;
using AffirmationBar.ViewModels;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
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
    public partial class CertificateOptionsPage : ContentPage
    {
        public CertificateOptionsViewModel CertificateOptionsViewModel { get; private set; }
        public CertificateOptionsPage(StudentInfo info)
        {
            CertificateOptionsViewModel = new CertificateOptionsViewModel(info);
            CertificateOptionsViewModel.Student.Reason = CertificateOptionsViewModel.Reasons[0];

            BindingContext = CertificateOptionsViewModel;

            InitializeComponent();
        }

        private async void OnGenerateClicked(object sender, EventArgs e)
        {
            byte[] certificate = await CertificateOptionsViewModel.GetCertificateAsync();

            IStorageService storageService = DependencyService.Get<IStorageService>();

            if (await storageService.SaveFile($"certificate_{CertificateOptionsViewModel.Student.FacultyNumber}.html", certificate))
                await DisplayAlert("Успех", $"Файлът беше запазен.", "OK");
        }
    }
}
