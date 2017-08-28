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
    public partial class CertificateOptionsPage : ContentPage
    {
        public CertificateOptionsViewModel CertificateOptionsViewModel { get; set; }
        public CertificateOptionsPage(StudentInfo info)
        {
            CertificateOptionsViewModel = new CertificateOptionsViewModel(info);
            CertificateOptionsViewModel.Selected = CertificateOptionsViewModel.Reasons[0];

            BindingContext = CertificateOptionsViewModel;

            InitializeComponent();
        }
    }
}
