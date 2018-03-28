using SusiAPIClient;
using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AffirmationBar.ViewModels
{
    public class RolesViewModel : BaseViewModel
    {
        List<string> roles;
        public RolesViewModel(List<string> studentRoles)
        {
            roles = studentRoles;
        }

        public StudentInfo Student { get; set; }

        public IList<string> Roles { get; set; } = new List<string>()
        {
            
        };
        

        private string selected;
        public string Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }
    }
}
