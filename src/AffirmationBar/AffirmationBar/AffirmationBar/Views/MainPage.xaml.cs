using AffirmationBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AffirmationBar.Views
{
    public partial class MainPage : ContentPage
    {
        public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();
        public MainPage()
        {
            InitializeComponent();

            BindingContext = LoginViewModel;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (await LoginViewModel.LoginAsync())
            {
                await Navigation.PushAsync(new StudentInfoPage());
            }
        }
    }
}
