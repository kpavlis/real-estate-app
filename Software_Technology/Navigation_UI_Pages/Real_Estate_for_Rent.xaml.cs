using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Software_Technology.Special_UI_Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Software_Technology.Classes;


namespace Software_Technology.Navigation_UI_Pages
{
    
    public sealed partial class Real_Estate_for_Rent : Page
    {
        MainWindow x;

        int Location_Selection { get { return Location_Radios.SelectedIndex; } set { Location_Radios.SelectedIndex = value; } }
        int Min_Square_Meters_Selection { get { return (int)Min_Square_Meters_Slider.Value; } set { Min_Square_Meters_Slider.Value = value; } }
        int Bedrooms_Selection { get { return Bedrooms_Combobox.SelectedIndex; } set { Bedrooms_Combobox.SelectedIndex = value; } }
        int Max_Price_Selection { get { return (int)Max_Price_Slider.Value; } set { Max_Price_Slider.Value = value; } }

        ObservableCollection<Test_Real> collect = new ObservableCollection<Test_Real>();

        public Real_Estate_for_Rent()
        {
            this.InitializeComponent();
            // This is a temporary code. It will be removed in a future update.
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.","550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Μονοκατοικία", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Μονοκατοικία", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Μονοκατοικία", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Μονοκατοικία", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }
            //x.member_variable.ShowRealEstateToBuy_Rent()
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Πληροφορίες Ακινήτου";
            dialog.CloseButtonText = "Κλείσιμο";
            dialog.Content = new Real_Estate_Info((Test_Real)(((Button)sender).Tag));

            var result = await dialog.ShowAsync();
        }

        private async void Rent_Click(object sender, RoutedEventArgs e)
        {
            if (x.member_variable == null)
            {
                x.TeachingTip.Title = "Αποτυχία Ενοικίασης";
                x.TeachingTip.Subtitle = "Πρέπει να συνδεθείς ως πελάτης για να ενοικιάσεις το ακίνητο !";
                x.TeachingTip.IsOpen = true;
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
            //x.member_variable.Buy_Sell_Rent_LeaseRealEstate()


        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //x.member_variable.ShowRealEstateToBuy_Rent()
        }

        private void Clear_Filters_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
