using SusiAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AffirmationBar.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private SusiClient susiClient = new SusiClient();

        public async Task<bool> LoginAsync()
        {
            IsLoading = true;

            await Task.Delay(3000);
            //await susiClient.LoginAsync(username, password);

            IsLoading = false;
            return true;
        }

        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
    }
}
