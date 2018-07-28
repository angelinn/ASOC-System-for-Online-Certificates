using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AffirmationBar.ViewModels
{
    public class ChooseRoleViewModel : BaseViewModel
    {
        public List<string> Roles { get; set; } = new List<string>()
        {
            "71497",
            "25771"
        };

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

        public async Task LoadRolesAsync()
        {

        }
    }
}
