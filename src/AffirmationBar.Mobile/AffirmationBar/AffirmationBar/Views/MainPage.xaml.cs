using AffirmationBar.ViewModels;
using SusiAPI.Responses;
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
            LoginResponse loginResponse = await LoginViewModel.LoginAsync();
            if (!loginResponse.LoggedIn)
                await DisplayAlert("Грешка", "Възникна грешка. Проверете името и паролата си.", "ОК");
            else
            {
                string number = null;
                if (loginResponse.HasMultipleRoles)
                {
                    ChooseRolePage chooseRolePage = new ChooseRolePage(loginResponse.Roles);
                    await grid.FadeTo(0, 500);
                    await Navigation.PushAsync(chooseRolePage);
                    grid.Opacity = 1;

                    chooseRolePage.Disappearing += async (s, e) =>
                    {
                        number = chooseRolePage.ChooseRoleViewModel.SelectedRole;
                        StudentInfo studentInfo = await LoginViewModel.GetStudentInfoAsync(number);
                        if (studentInfo != null)
                        {
                            LoginViewModel.Password = String.Empty;

                            await grid.FadeTo(0, 500);
                            await Navigation.PushAsync(new CertificateOptionsPage(studentInfo), false);
                            grid.Opacity = 1;
                        }
                    };
                }
            }
        }
    }
}
