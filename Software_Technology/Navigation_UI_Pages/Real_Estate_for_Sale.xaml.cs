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


namespace Software_Technology.Navigation_UI_Pages
{
    
    public sealed partial class Real_Estate_for_Sale : Page
    {
        MainWindow x;

        List<Test_Real> collect = new List<Test_Real>();

        public Real_Estate_for_Sale()
        {
            this.InitializeComponent();
            // This is a temporary code. It will be removed in a future update.
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό.", "550", "6os", "Μαρούσι, Αθήνα", "Ένα όμορφο αγροτικό σπίτι."));
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
            //Query for Properties
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

        private async void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (x.admin_variable == null && x.member_variable == null)
            {
                x.TeachingTip.Title = "Αποτυχία Αγοράς";
                x.TeachingTip.Subtitle = "Πρέπει να συνδεθείς για να αγοράσεις το ακίνητο !";
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
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    // This is a temporary class. It will be removed in a future update.
    class Test_Real
    {
        internal List<string> image = new List<string>();
        internal string name;
        internal string comments;
        internal string square_meters;
        internal string floor;
        internal string area;
        internal string details;

        public Test_Real(string image, string image2, string name, string comments, string square_meters, string floor, string area, string details)
        {
           
            this.image.Add(image);
            this.image.Add(image2);
            this.name = name;
            this.comments = comments;
            this.area = area;
            this.details = details;
            this.square_meters = square_meters;
            this.floor = floor;
        }
    }
}
