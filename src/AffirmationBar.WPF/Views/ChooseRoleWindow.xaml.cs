using AffirmationBar.ViewModels;
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
    /// Interaction logic for ChooseRoleWindow.xaml
    /// </summary>
    public partial class ChooseRoleWindow : Window
    {
        public ChooseRoleViewModel ChooseRoleViewModel { get; private set; }
        public ChooseRoleWindow(List<string> roles)
        {
            InitializeComponent();

            ChooseRoleViewModel = new ChooseRoleViewModel(roles);
            DataContext = ChooseRoleViewModel;
        }

        private void OnChooseClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
