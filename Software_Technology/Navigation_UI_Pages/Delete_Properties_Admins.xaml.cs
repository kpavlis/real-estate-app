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

namespace Software_Technology.Navigation_UI_Pages
{
    
    public sealed partial class Delete_Properties_Admins : Page, INotifyPropertyChanged
    {
        MainWindow x;

        public event PropertyChangedEventHandler PropertyChanged;

        List<int> delete_properties = new List<int>();

        List<int> _data_bind_delete_properties_admins = new List<int>();

        public List<int> Data_bind_Delete_Properties_Admins
        {
            get { return _data_bind_delete_properties_admins; }
            set
            {
                if (_data_bind_delete_properties_admins != value)
                {
                    _data_bind_delete_properties_admins = value;
                    OnPropertyChanged(nameof(Data_bind_Delete_Properties_Admins));
                }

            }

        }

        public Delete_Properties_Admins()
        {
            this.InitializeComponent();

            Data_bind_Delete_Properties_Admins = delete_properties;
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
