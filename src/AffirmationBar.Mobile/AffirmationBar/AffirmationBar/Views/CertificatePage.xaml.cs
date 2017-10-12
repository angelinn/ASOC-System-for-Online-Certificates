using AffirmationBar.ViewModels;
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
	public partial class CertificatePage : ContentPage
	{
        public CertificatePage(string html)
        {
            InitializeComponent();

        }
	}
}