using AffirmationBar.ViewModels;
using AffirmationBar.WPF.Views;
using SusiAPI.Responses;
using SusiAPICommon.Models;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AffirmationBar.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();

		public MainWindow()
		{
			InitializeComponent();

			DataContext = LoginViewModel;
		}

		private void OnUsernameFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;
			tb.Text = string.Empty;
			tb.GotFocus -= OnUsernameFocus;
		}

        private void OnPasswordFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox tb = (PasswordBox)sender;
            tb.Password = string.Empty;
            tb.GotFocus -= OnPasswordFocus;
        }


        private async Task LoginAsync()
		{
			LoginViewModel.Password = txtBoxPassword.Password;
            LoginResponse loginResponse = await LoginViewModel.LoginAsync();
            if (!loginResponse.LoggedIn)
                MessageBox.Show("Грешка", "Възникна грешка. Проверете името и паролата си.");
            else
            {
                string number = null;
                if (loginResponse.HasMultipleRoles)
                {
                    ChooseRoleWindow chooseRoleWindow = new ChooseRoleWindow(loginResponse.Roles);
                    chooseRoleWindow.ShowInTaskbar = false;
                    chooseRoleWindow.Owner = this;
                    chooseRoleWindow.ShowDialog();

                    number = chooseRoleWindow.ChooseRoleViewModel.SelectedRole;
                }

                StudentInfo studentInfo = await LoginViewModel.GetStudentInfoAsync(number);
                var newForm = new StudentInfoWindow(studentInfo); //create your new form.
                newForm.ShowInTaskbar = false;
                newForm.Owner = Application.Current.MainWindow;
                newForm.ShowDialog(); //show the new form.
            }
		}

		private async void getDocBttn_Click(object sender, RoutedEventArgs e)
		{
			await LoginAsync();
		}


		private async void OnPasswordKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
				await LoginAsync();
		}
	}
}
