using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Software_Technology.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    
    public sealed partial class Delete_Properties : Page, INotifyPropertyChanged
    {
        MainWindow x;

        public event PropertyChangedEventHandler PropertyChanged;

        List<string> properties = new List<string>();

        List<int> _data_bind_delete = new List<int>();

        public List<int> Data_bind_Delete
        {
            get { return _data_bind_delete; }
            set
            {
                if (_data_bind_delete != value)
                {
                    _data_bind_delete = value;
                    OnPropertyChanged(nameof(Data_bind_Delete));
                }

            }

        }

        public Delete_Properties()
        {
            this.InitializeComponent();
            properties.Add("Property 1");
            properties.Add("Property 2");
            properties.Add("Property 3");
            properties.Add("Property 4");
            properties.Add("Property 5");
            properties.Add("Property 6");
            properties.Add("Property 7");
            properties.Add("Property 8");
            properties.Add("Property 9");
            properties.Add("Property 10");
            properties.Add("Property 11");

            //Combo_Selection.SelectedIndex = 0;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }
            Combo_Selection.SelectedIndex = 0;
        }

        private void Delete_Property_Click(object sender, RoutedEventArgs e)
        {
            //Write the delete code here.
            ((Button)sender).IsEnabled = false;
            Debug.WriteLine((((Button)sender).Tag).ToString());
            //properties.RemoveAt(num);
            //((Button)sender).IsEnabled = false;

            //foreach (string item in properties)
            //{
            //Debug.WriteLine(item);
            //}
            int myR = (int)((Button)sender).Tag;
            x.member_variable.DeleteMyRealEstateMember(myR);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Combo_Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
            Debug.WriteLine(((ComboBox)sender).SelectedValue.ToString());
            if (((ComboBox)sender).SelectedValue.ToString().Equals("Πώληση"))
            {
                //!!!!!!!!!!
                Data_bind_Delete = new List<int>(DatabaseController.GetMyRealEstatesForDelete(x.member_variable.GetUsersID(), false));
                Debug.WriteLine("Hello 1");
            }
            else if(((ComboBox)sender).SelectedValue.ToString().Equals("Εκμίσθωση"))
            {
                Data_bind_Delete = new List<int>(DatabaseController.GetMyRealEstatesForDelete(x.member_variable.GetUsersID(), true));
                Debug.WriteLine("Hello 2");
            }
        }
    }
}
