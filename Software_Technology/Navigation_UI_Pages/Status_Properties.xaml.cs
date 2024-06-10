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
using Software_Technology.Classes;

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

        //Temporary List
        List<string> sale = new List<string>();

        List<RealEstate> _data_bind_status = new List<RealEstate>();
        

        internal List<RealEstate> Data_bind_Status
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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }

            Combo_Selection.SelectedIndex = 0;

        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Combo_Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedValue.ToString() == "Πώληση")
            {
                
                Debug.WriteLine("Hello 1");
                
                Data_bind_Status = x.member_variable.ShowMyPurchased_Rented_Sold_LeasedRealEstatesMember("sold");

            }
            else
            {
                
                Debug.WriteLine("Hello 2");
                
                Data_bind_Status = x.member_variable.ShowMyPurchased_Rented_Sold_LeasedRealEstatesMember("leased");

            }
        }
    }
}
