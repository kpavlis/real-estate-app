using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Diagnostics;
using Software_Technology.Classes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Software_Technology.Navigation_UI_Pages
{

    public sealed partial class Members_Data : Page
    {
        MainWindow x;

        string Username { get { return username_obj.Text; } set { username_obj.Text = value; } }

        string NameOfUser { get { return name_obj.Text; } set { name_obj.Text = value; } }
        string Surname { get { return surname_obj.Text; } set { surname_obj.Text = value; } }
        string Password { get { return password_obj.Password.ToString(); } }
        string Email { get {  return email_obj.Text; } set { email_obj.Text = value; } }
        string Phone { get { return phone_obj.Text; } set { phone_obj.Text = value; } }

        public Members_Data()
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
                x.member_variable.ChangePassword(x.member_variable.GetUsersID(), Password.ToString());
                
            }

            
            x.member_variable.UpdateNameSurnameUsers(x.member_variable.GetUsersID(), NameOfUser.ToString(), Surname.ToString());
            x.member_variable.ChangeContactDetailsMember(Email.ToString(), Phone.ToString());

            x.TeachingTip.Title = "Επιτυχής Ενημέρωση Στοιχείων Χρήστη";
            x.TeachingTip.Subtitle = "Η διαδικασία ολοκληρώθηκε επιτυχώς !";
            x.TeachingTip.IsOpen = true;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true) {
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
