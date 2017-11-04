using AffirmationBar.ViewModels;
using AffirmationBar.WPF.Views;
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
			StudentInfo studentInfo = await LoginViewModel.GetStudentInfoAsync();
			if (studentInfo.FacultyNumber != 0)
			{
				
				var newForm = new StudentInfoWindow(studentInfo); //create your new form.
				newForm.ShowInTaskbar = false;
				newForm.Owner = Application.Current.MainWindow;
				newForm.ShowDialog(); //show the new form.
			}
			else
			{
				MessageBox.Show("Грешка", "Възникна грешка. Проверете името и паролата си.");
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
