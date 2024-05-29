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


namespace Software_Technology.Navigation_UI_Pages
{
    
    public sealed partial class Real_Estate_for_Sale : Page
    {
        ObservableCollection<Test_Real> collect = new ObservableCollection<Test_Real>();

        public Real_Estate_for_Sale()
        {
            this.InitializeComponent();
            // This is a temporary code. It will be removed in a future update.
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Μονοκατοικία", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Μονοκατοικία", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Μονοκατοικία", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Διαμέρισμα", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό."));
            collect.Add(new Test_Real("/Assets/interior_test.jpg", "/Assets/house_icon.png", "Μονοκατοικία", "Ωραίο και καλό. Έχει μεγάλα δωμάτια με επαρκή εξαερισμό."));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // This is a temporary code. It will be removed in a future update.
            Debug.WriteLine(((Test_Real)(((Button)sender).Tag)).name);
            Debug.WriteLine(((Test_Real)(((Button)sender).Tag)).comments);
        }
    }

    // This is a temporary class. It will be removed in a future update.
    class Test_Real
    {
        internal List<string> image = new List<string>();
        internal string name;
        internal string comments;

        public Test_Real(string image, string image2, string name, string comments)
        {
            this.image.Add(image);
            this.image.Add(image2);
            this.name = name;
            this.comments = comments;
        }
    }
}
