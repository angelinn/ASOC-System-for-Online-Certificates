using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SUSI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void clearText(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;
			tb.Text = string.Empty;
			tb.GotFocus -= clearText;
		}

        static async Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }


        private async void getDocBttn_Click(object sender, RoutedEventArgs e)
		{
            string username = txtUsername.Text;
            string password = txtBoxPassword.Password;

            HttpClient client = new HttpClient();
            var t= client.BaseAddress;
            string result = client.PostAsync("http://localhost:63654/api/affirmation", new StringContent(
                                       JsonConvert.SerializeObject(new { Username = username, Password = password }),
                                                                   Encoding.UTF8, "application/json"))
                                   .Result.Content.ReadAsStringAsync().Result;


            await WriteTextAsync("C:\\temp.txt", result);
            MessageBox.Show("Your file is created: \"C:\\temp.txt\"");

        }
	}
}
