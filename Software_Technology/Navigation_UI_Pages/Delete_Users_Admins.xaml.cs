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

        List<string> delete_users = new List<string>();

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

            delete_users.Add("User_1");
            delete_users.Add("User_2");
            delete_users.Add("User_3");
            delete_users.Add("User_4");
            delete_users.Add("User_5");
            delete_users.Add("User_6");
            delete_users.Add("User_7");
            delete_users.Add("User_8");

            Data_bind_Delete_Users_Admins = delete_users;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Delete_Property_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
        }
    }
}
