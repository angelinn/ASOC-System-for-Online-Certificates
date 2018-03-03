using AffirmationBar.ViewModels;
using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AffirmationBar.WPF.Views
{
    /// <summary>
    /// Interaction logic for ChoosingRoleWindow.xaml
    /// </summary>
    public partial class ChoosingRoleWindow : Window
    {
        public RolesViewModel roles { get; set; }
        public ChoosingRoleWindow(StudentInfo studentInfo)
        {
            InitializeComponent();

            roles = new RolesViewModel(studentInfo);

            this.DataContext = roles;
        }

        
    }
}
