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
        private SusiClient susiClient = new SusiClient();

        public RolesViewModel(StudentInfo studentInfo)
        {
            this.Student = studentInfo;
            this.Student.Reason = Roles.First();
        }

        public StudentInfo Student { get; set; }

        public IList<string> Roles { get; set; } = new List<string>()
        {
            //we have to fill the list with all roles from the html
            "25605",
            "71462"
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
