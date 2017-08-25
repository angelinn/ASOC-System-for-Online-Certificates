using AffirmationBar.Services;
using AffirmationBar.Views;
using SusiAPICommon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AffirmationBar
{
    public partial class App : Application
    {
        public Page Page { get; set; }
        public INavigationService NavigationService { get; } = new NavigationService();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            Page = MainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
