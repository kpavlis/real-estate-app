using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Software_Technology.Navigation_UI_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Admins_Data : Page
    {
        MainWindow x;

        string Username { get { return username_obj.Text; } set { username_obj.Text = value; } }
        string Name { get { return name_obj.Text; } set {  name_obj.Text = value; } }
        string Surname { get { return surname_obj.Text; } set { surname_obj.Text = value; } }
        string Password { get { return password_obj.Password.ToString(); } }

        public Admins_Data()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(Password)) && !(String.IsNullOrWhiteSpace(Password)))
            {
                x.admin_variable.ChangePassword(x.admin_variable.GetUsersID(), Password.ToString());

            }

            x.admin_variable.UpdateNameSurnameUsers(x.admin_variable.GetUsersID(), Name.ToString(), Surname.ToString());

            x.TeachingTip.Title = "Επιτυχής Ενημέρωση Στοιχείων Χρήστη Admin";
            x.TeachingTip.Subtitle = "Η διαδικασία ολοκληρώθηκε επιτυχώς !";
            x.TeachingTip.IsOpen = true;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
            {
                password_obj.IsEnabled = true;
            }
            else
            {
                password_obj.Password = "";
                password_obj.IsEnabled = false;
            }
        }
    }
}
