using AffirmationBar.ViewModels;
using AffirmationBar.WPF.Views;
using MahApps.Metro.Controls;
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
    public partial class MainWindow : MetroWindow
	{
		LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();

		public MainWindow()
		{
			InitializeComponent();

			DataContext = LoginViewModel;
		}

		private void clearText(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;
			tb.Text = string.Empty;
			tb.GotFocus -= clearText;
		}

		private async Task LoginAsync()
		{
			LoginViewModel.Password = txtBoxPassword.Password;
			StudentInfo studentInfo = await LoginViewModel.GetStudentInfoAsync();
			if (studentInfo != null)
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
