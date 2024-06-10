using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Status_Properties : Page, INotifyPropertyChanged
    {
        MainWindow x;

        public event PropertyChangedEventHandler PropertyChanged;

        List<string> sale = new List<string>();

        List<string> _data_bind_status = new List<string>();

        public List<string> Data_bind_Status
        {
            get { return _data_bind_status; }
            set
            {
                if (_data_bind_status != value)
                {
                    _data_bind_status = value;
                    OnPropertyChanged(nameof(Data_bind_Status));
                }

            }

        }

        public Status_Properties()
        {
            this.InitializeComponent();

            sale.Add("Property_1");
            sale.Add("Property_2");
            sale.Add("Property_3");
            sale.Add("Property_4");
            sale.Add("Property_5");
            sale.Add("Property_6");
            sale.Add("Property_7");
            sale.Add("Property_8");

            //Combo_Selection.SelectedIndex = 0;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }

            Combo_Selection.SelectedIndex = 0;

            Debug.WriteLine(x.member_variable.email);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Combo_Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedValue.ToString() == "Πώληση")
            {
                Data_bind_Status = sale;
                Debug.WriteLine("Hello 1");
                Debug.WriteLine(x.member_variable.surname);
            }
            else
            {
                Data_bind_Status = new List<string>();
                Debug.WriteLine("Hello 2");
                Debug.WriteLine(x.member_variable.phoneNumber);
            }
        }
    }
}
