using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.ComponentModel;
using Software_Technology.Classes;
using System.Diagnostics;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Software_Technology.Navigation_UI_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Delete_Users_Admins : Page, INotifyPropertyChanged
    {
        MainWindow x;

        public event PropertyChangedEventHandler PropertyChanged;

        List<string> delete_usersID = new List<string>();
        List<string> delete_usersUsername = new List<string>();



        List<string> _data_bind_delete_users_admins = new List<string>();

        public List<string> Data_bind_Delete_Users_Admins
        {
            get { return _data_bind_delete_users_admins; }
            set
            {
                if (_data_bind_delete_users_admins != value)
                {
                    _data_bind_delete_users_admins = value;
                    OnPropertyChanged(nameof(Data_bind_Delete_Users_Admins));
                }

            }

        }

        public Delete_Users_Admins()
        {
            this.InitializeComponent();

           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }

            delete_usersUsername = new List<String>(DatabaseController.GetMembersUsername());

            delete_usersID = new List<String>(DatabaseController.GetMembersID());

            for (int i=0; i < delete_usersID.Count;i++)
            {
                Data_bind_Delete_Users_Admins.Add(delete_usersID[i]+","+ delete_usersUsername[i]);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Delete_Property_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            string memberToBeDeleted0 = (String)((Button)sender).Tag;

            string[] memberToBeDeleted = memberToBeDeleted0.Split(',');

            x.admin_variable.DeleteMember(memberToBeDeleted[0], memberToBeDeleted[1]);

            x.TeachingTip.Title = "Επιτυχής Διαγραφή Χρήστη Μember";
            x.TeachingTip.Subtitle = "Η διαδικασία ολοκληρώθηκε επιτυχώς !";
            x.TeachingTip.IsOpen = true;
        }
    }
}
