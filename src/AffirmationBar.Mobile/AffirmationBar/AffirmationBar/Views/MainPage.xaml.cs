using AffirmationBar.ViewModels;
using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace AffirmationBar.Views
{
    public partial class MainPage : ContentPage
    {
        public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = LoginViewModel;
            
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await LoginAsync();
        }

        private async void OnPasswordCompleted(object sender, EventArgs e)
        {
            await LoginAsync();
        }
        
        private async Task LoginAsync()
        {
            StudentInfo studentInfo = await LoginViewModel.GetStudentInfoAsync();
            if (studentInfo != null)
            { 
                LoginViewModel.Password = String.Empty;
                
                await grid.FadeTo(0, 500);
                await Navigation.PushAsync(new CertificateOptionsPage(studentInfo, LoginViewModel.SusiClient), false);
                grid.Opacity = 1;
            }
            else
            {
                await DisplayAlert("Грешка", "Възникна грешка. Проверете името и паролата си.", "ОК");
            }
        }
    }
}
