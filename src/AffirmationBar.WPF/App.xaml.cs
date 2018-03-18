using Microsoft.HockeyApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AffirmationBar.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            HockeyClient.Current.Configure("212665a3789d4a0198f3490fb9b7eef6");
            await HockeyClient.Current.SendCrashesAsync();
        }
    }
}
