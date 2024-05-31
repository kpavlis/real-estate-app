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


namespace Software_Technology.Navigation_UI_Pages
{
    
    public sealed partial class Real_Estate_for_Rent : Page
    {
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Πληροφορίες Ακινήτου";
            dialog.CloseButtonText = "Κλείσιμο";
            dialog.Content = new Real_Estate_Info((Test_Real)(((Button)sender).Tag));

            var result = await dialog.ShowAsync();
        }
    }
}
