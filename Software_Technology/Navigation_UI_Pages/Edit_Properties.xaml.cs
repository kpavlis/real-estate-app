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
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Software_Technology.Navigation_UI_Pages
{
    
    public sealed partial class Edit_Properties : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        MainWindow x;

        bool Photos_Changes = false;
        int counter = 0;
        StorageFile current_file;
        string current_file_path;
        List<StorageFile> current_file_list = new List<StorageFile>();
        List<string> database_file_list = new List<string>();

        List<string> _data_bind_edit = new List<string>();

        //Properties
        string Type { get { return type_obj.Text; } set { type_obj.Text = value; } }
        string Area { get { return area_obj.Text; } set { area_obj.Text = value; } }
        string Info { get { return info_obj.Document.ToString(); } set { info_obj.Document.SetText(Microsoft.UI.Text.TextSetOptions.None, value); } }
        int Bedrooms { get { return (int)bedrooms_obj.Value; } set { bedrooms_obj.Value = value; } }
        int Price { get { return (int)price_obj.Value; } set { price_obj.Value = value; } }
        int Year { get { return (int)year_obj.Value; } set { year_obj.Value = value; } }
        int Floor { get { return (int)floor_obj.Value; } set { floor_obj.Value = value; } }
        string Property_State { get { return property_state_obj.SelectedIndex.ToString(); } set { property_state_obj.SelectedIndex = 1; } }
        int Size { get { return (int)square_meters_obj.Value; } set { square_meters_obj.Value = value; } }

        public List<string> Data_bind_Edit { get { return _data_bind_edit; } 
            set
            {
                if (_data_bind_edit != value)
                {
                    _data_bind_edit = value;
                    OnPropertyChanged(nameof(Data_bind_Edit)); 
                }

            }

        }
        //Temporary list
        List<string> test_list = new List<string>();

        //Temporary list
        List<string> myproperties = new List<string>();

        Test_Real edit_property;

        public Edit_Properties()
        {
            this.InitializeComponent();
            
            myproperties.Add("/Assets/Properties_Pictures/zoo.png");
            //myproperties.Add("Assets/Properties_Pictures/zoo.png");
            //test_list.Add("Hello");
            //NavLinksList.ItemsSource = myproperties;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }

            property_state_obj.SelectedIndex = 1;
            Combo_Selection.SelectedIndex= 0;
        }

        private void List_ItemClick(object sender, ItemClickEventArgs e)
        {
            Delete_All_Photos.Visibility = Visibility.Visible;
            Photos_Selection.Visibility = Visibility.Collapsed;
            //Test_Real x = (Test_Real)e.ClickedItem;
            //edit_property = x;
            //property_state_obj.SelectedIndex = 1;
            Size = 1000;
        }

        private void Delete_All_Photos_Click(object sender, RoutedEventArgs e)
        {
            Delete_All_Photos.Visibility = Visibility.Collapsed;
            Photos_Selection.Visibility = Visibility.Visible;
            Photos_Changes = true;
            
        }

        private async void Add_Photo_Click(object sender, RoutedEventArgs e)
        {
            if (counter == 0)
            {
                photos_group.Text = "";
            }
            var openPicker = new FileOpenPicker();

            var hwnd = WindowNative.GetWindowHandle(x);
            InitializeWithWindow.Initialize(openPicker, hwnd);

            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpg");

            current_file = await openPicker.PickSingleFileAsync();
            if (current_file != null)
            {
                current_file_list.Add(current_file);
                photos_group.Text += "\n" + current_file.Name;
                counter++;



                if (counter == 4)
                {
                    ((Button)sender).IsEnabled = false;
                }
            }
        }

        private void Clean_Photos_Click(object sender, RoutedEventArgs e)
        {
            photos_group.Text = "Δεν έχεις επιλέξει φωτογραφία";
            current_file_list.Clear();
            add_photo_button.IsEnabled = true;
            counter = 0;
        }

        private async void Update_Property_Click(object sender, RoutedEventArgs e)
        {
            if (Photos_Changes)
            {
                foreach (string x in myproperties)
                {
                    string filePath = AppContext.BaseDirectory + x.Substring(1);
                    Debug.WriteLine(filePath);
                    if (File.Exists(filePath))
                    {
                        // Διαγραφή του αρχείου
                        Debug.WriteLine(x);
                        File.SetAttributes(filePath, System.IO.FileAttributes.Normal);
                        File.Delete(filePath);
                        //Debug.WriteLine("Operation Completed");
                    }
                }
                foreach (StorageFile file in current_file_list)
                {
                    if (file != null)
                    {

                        // Προσδιορισμός του πλήρους μονοπατιού του φακέλου Assets
                        string assetsFolderPath = AppContext.BaseDirectory + @"Assets\Properties_Pictures";
                        //Debug.WriteLine(assetsFolderPath);
                        // Βεβαιωθείτε ότι ο φάκελος Assets υπάρχει
                        if (!Directory.Exists(assetsFolderPath))
                        {
                            Directory.CreateDirectory(assetsFolderPath);
                        }

                        // Προσδιορισμός του πλήρους μονοπατιού για το νέο αρχείο
                        string destinationFilePath = Path.Combine(assetsFolderPath, file.Name);
                        //Debug.WriteLine(destinationFilePath);
                        // Αντιγραφή του αρχείου στον φάκελο Assets
                        await file.CopyAsync(await StorageFolder.GetFolderFromPathAsync(assetsFolderPath), file.Name, NameCollisionOption.ReplaceExisting);
                        File.SetAttributes(destinationFilePath, System.IO.FileAttributes.Normal);
                        current_file_path = @"/Assets/Properties_Pictures/" + file.Name;
                        Debug.WriteLine(current_file_path);
                        database_file_list.Add(current_file_path);
                        //Debug.WriteLine(file_path);
                        //Debug.WriteLine(File.Exists(AppContext.BaseDirectory + file_path).ToString());
                        //Debug.WriteLine("The Operation Completed!");



                    }
                    else
                    {
                        //Debug.WriteLine("The operation hasn't completed!");
                    }
                }
            }

        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((ComboBox)sender).SelectedValue.ToString() == "Πώληση")
            {
                //Data_bind_Edit = test_list;
                Pane_Type.Text = "προς Πώληση";
            }
            else
            {
                //Data_bind_Edit = myproperties;
                Pane_Type.Text = "προς Εκμίσθωση";
            }
        }
    }
}
