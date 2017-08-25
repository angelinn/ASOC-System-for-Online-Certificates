using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AffirmationBar.ViewModels.Events
{
    public delegate void LoginEventHandler(object sender, LoginEventArgs args);

    public class LoginEventArgs : EventArgs
    {
        public bool IsSuccessful { get; private set; }
        public StudentInfo StudentInfo { get; private set; }

        public LoginEventArgs(bool success, StudentInfo info = null)
        {
            IsSuccessful = success;
            StudentInfo = info;
        }
    }
}
