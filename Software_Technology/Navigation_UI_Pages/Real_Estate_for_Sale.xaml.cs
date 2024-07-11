using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Software_Technology.Navigation_UI_Pages;
using System.Diagnostics;
using Software_Technology.Special_UI_Pages;
using Software_Technology.Classes;
using System.ComponentModel;
using Windows.Foundation.Metadata;


namespace Software_Technology.Navigation_UI_Pages
{
    
    public sealed partial class Real_Estate_for_Sale : Page, INotifyPropertyChanged
    {
        MainWindow x;

        int Location_Selection { get { return Location_Radios.SelectedIndex; } set { Location_Radios.SelectedIndex = value; } }
        int Min_Square_Meters_Selection { get { return (int)Min_Square_Meters_Slider.Value; } set { Min_Square_Meters_Slider.Value = value; } }
        int Bedrooms_Selection { get { return Bedrooms_Combobox.SelectedIndex; } set { Bedrooms_Combobox.SelectedIndex = value; } }
        int Max_Price_Selection { get { return (int)Max_Price_Slider.Value; } set { Max_Price_Slider.Value = value; } }


        List<RealEstate> _data_bind_for_sale = new List<RealEstate>();

        RealEstate reToBeBought = new RealEstate(0, null, null, 0, 0, 0, 0, 0, true, true, null, null, null, new List<string>());


        internal List<RealEstate> Data_bind_For_Sale
        {
            get { return _data_bind_for_sale; }
            set
            {
                if (_data_bind_for_sale != value)
                {
                    _data_bind_for_sale = value;
                    OnPropertyChanged(nameof(Data_bind_For_Sale));
                }

            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Real_Estate_for_Sale()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }
            
            if (x.member_variable != null)
            {
                
                String userIDExists = x.member_variable.GetUsersID();
                Data_bind_For_Sale = new List<RealEstate>(Members.ShowRealEstateToBuy_RentMember(userIDExists, false, 0, 0, 0, 0));
            }
            else
            {
                
                Data_bind_For_Sale = new List<RealEstate>(Members.ShowRealEstateToBuy_RentMember("", false, 0, 0, 0, 0));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Πληροφορίες Ακινήτου";
            dialog.CloseButtonText = "Κλείσιμο";
            dialog.Content = new Real_Estate_Info((RealEstate)(((Button)sender).Tag));

            var result = await dialog.ShowAsync();

        }

        private async void Buy_Click(object sender, RoutedEventArgs e)
        {
            reToBeBought = (RealEstate)(((Button)sender).Tag);
            if (x.member_variable == null && x.admin_variable != null)
            {
                x.TeachingTip.Title = "Αποτυχία Αγοράς";
                x.TeachingTip.Subtitle = "Πρέπει να συνδεθείς ως πελάτης για να αγοράσεις το ακίνητο !";
                x.TeachingTip.IsOpen = true;
            }
            else if(x.member_variable == null && x.admin_variable == null)
            {
                x.dialog.Title = "Προχώρησε σε Σύνδεση/Εγγραφή";
                x.dialog.PrimaryButtonText = "Σύνδεση";
                x.dialog.SecondaryButtonText = "";
                x.dialog.CloseButtonText = "Κλείσιμο";
                x.dialog.DefaultButton = ContentDialogButton.Primary;
                x.dialog.Content = new Sign_In_Page(x);

                if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
                {
                    x.dialog.XamlRoot = this.XamlRoot;
                }

                await x.dialog.ShowAsync();
            }
            else
            {
                ContentDialog dialog = new ContentDialog();

                dialog.XamlRoot = this.XamlRoot;
                dialog.Title = "Διαδικασία Πληρωμή";
                dialog.CloseButtonText = "Κλείσιμο";
                dialog.PrimaryButtonText = "Πληρωμή";
                dialog.PrimaryButtonClick += Pay_Click;
                dialog.Tag = sender;
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = new Payment_Form();

                var result = await dialog.ShowAsync();
            }
        }

        private void Pay_Click(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ((Button)sender.Tag).IsEnabled = false;
            
            x.member_variable.Buy_Sell_Rent_LeaseRealEstateMember(reToBeBought, x.member_variable.GetUsersID());

            x.TeachingTip.Title = "Επιτυχής Αγορά Ακινήτου";
            x.TeachingTip.Subtitle = "Η διαδικασία ολοκληρώθηκε επιτυχώς !";
            x.TeachingTip.IsOpen = true;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Data_bind_For_Sale.Clear();
            
            if (x.member_variable != null)
            {
                String userIDExists = x.member_variable.GetUsersID();
                Data_bind_For_Sale = new List<RealEstate>(Members.ShowRealEstateToBuy_RentMember(userIDExists, false, Location_Selection, Min_Square_Meters_Selection, Bedrooms_Selection, Max_Price_Selection));
            }
            else
            {
                Data_bind_For_Sale = new List<RealEstate>(Members.ShowRealEstateToBuy_RentMember("", false, Location_Selection, Min_Square_Meters_Selection, Bedrooms_Selection, Max_Price_Selection));
            }
        }

        private void Clear_Filters_Click(object sender, RoutedEventArgs e)
        {
            if (x.member_variable != null)
            {
                
                String userIDExists = x.member_variable.GetUsersID();
                Data_bind_For_Sale = new List<RealEstate>(Members.ShowRealEstateToBuy_RentMember(userIDExists, false, 0, 0, 0, 0));
            }
            else
            {
                
                Data_bind_For_Sale = new List<RealEstate>(Members.ShowRealEstateToBuy_RentMember("", false, 0, 0, 0, 0));
            }
        }
    }

}
