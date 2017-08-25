using AffirmationBar.ViewModels.Events;
using SusiAPIClient;
using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AffirmationBar.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private SusiClient susiClient = new SusiClient();
        public event LoginEventHandler OnLoginCompleted;

        public ICommand LoginCommand;

        public LoginViewModel()
        {
        }

        public async Task<StudentInfo> GetStudentInfoAsync()
        {
            StudentInfo studentInfo = null;

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                IsLoading = true;
                studentInfo = await susiClient.GetStudentInfoAsync(username, password);
                IsLoading = false;
            }

            return studentInfo;
        }
        
        public async Task<string> GetCertificate()
        {
            string studentInfo = null;

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                IsLoading = true;
                studentInfo = await susiClient.GetCertificate(username, password);
                IsLoading = false;
            }

            return studentInfo;
        }

        public async Task LoginAsync()
        {
            StudentInfo studentInfo = await GetStudentInfoAsync();
            LoginEventArgs args = new LoginEventArgs(studentInfo != null, studentInfo);
            Password = String.Empty;

            OnLoginCompleted.Invoke(this, args);
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
