using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AffirmationBar.ViewModels
{
    public class ChooseRoleViewModel : BaseViewModel
    {
        public List<string> Roles { get; set; }

        public ChooseRoleViewModel(List<string> roles)
        {
            Roles = roles;
            SelectedRole = Roles[1];
        }

        private string selectedRole;
        public string SelectedRole
        {
            get
            {
                return selectedRole;
            }
            set
            {
                selectedRole = value;
                OnPropertyChanged();
            }
        }
    }
}
