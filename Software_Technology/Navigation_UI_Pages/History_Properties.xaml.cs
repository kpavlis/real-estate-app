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
    public sealed partial class History_Properties : Page, INotifyPropertyChanged
    {
        MainWindow x;

        public event PropertyChangedEventHandler PropertyChanged;

        //Temporary List
        List<string> buy = new List<string>();

        List<RealEstate> _data_bind_history = new List<RealEstate>();

        internal List<RealEstate> Data_bind_History
        {
            get { return _data_bind_history; }
            set
            {
                if (_data_bind_history != value)
                {
                    _data_bind_history = value;
                    OnPropertyChanged(nameof(Data_bind_History));
                }

            }

        }

        public History_Properties()
        {
            this.InitializeComponent();
            //This code will be removed
            buy.Add("Property_1");
            buy.Add("Property_2");
            buy.Add("Property_3");
            buy.Add("Property_4");
            buy.Add("Property_5");
            buy.Add("Property_6");
            buy.Add("Property_7");
            buy.Add("Property_8");

            
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
            if (((ComboBox)sender).SelectedValue.ToString() == "Αγορασμένα")
            {
                List<int> mylist = new List<int>(DatabaseController.GetRealEstates1(x.member_variable.GetUsersID()));
                Debug.WriteLine("reeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"+mylist.Count.ToString());
                //Data_bind_History = buy;
                Debug.WriteLine("Hello fuiobuiyererurb 1");
                Data_bind_History = x.member_variable.ShowMyPurchased_Rented_Sold_LeasedRealEstatesMember("bought");
            }
            else
            {
                //Data_bind_History = new List<string>();
                Debug.WriteLine("Hello 2");
                Data_bind_History = x.member_variable.ShowMyPurchased_Rented_Sold_LeasedRealEstatesMember("rented");
            }
        }
    }
}
