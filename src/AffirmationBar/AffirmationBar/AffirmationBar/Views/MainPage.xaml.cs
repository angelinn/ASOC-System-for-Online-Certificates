using AffirmationBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AffirmationBar
{
    public partial class MainPage : ContentPage
    {
        public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();
        public MainPage()
        {
            InitializeComponent();

            BindingContext = LoginViewModel;
        }
    }
}
