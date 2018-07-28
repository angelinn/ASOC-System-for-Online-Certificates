using AffirmationBar.ViewModels;
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
	public partial class ChooseRolePage : ContentPage
	{
        public ChooseRoleViewModel ChooseRoleViewModel { get; private set; }
        public ChooseRolePage (List<string> roles)
		{
			InitializeComponent ();

            ChooseRoleViewModel = new ChooseRoleViewModel(roles);
            BindingContext = ChooseRoleViewModel;
		}

        private void OnChooseClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}