using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
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

        List<RealEstate> _data_bind_edit = new List<RealEstate>();

        //Properties
        string Type { get { return type_obj.Text; } set { type_obj.Text = value; } }
        string Area { get { return area_obj.Text; } set { area_obj.Text = value; } }
        string Info { get { string x; info_obj.Document.GetText(Microsoft.UI.Text.TextGetOptions.None, out x); return x; } set { info_obj.Document.SetText(Microsoft.UI.Text.TextSetOptions.None, value); } }
        int Bedrooms { get { return (int)bedrooms_obj.Value; } set { bedrooms_obj.Value = value; } }
        int Price { get { return (int)price_obj.Value; } set { price_obj.Value = value; } }
        int Year { get { return (int)year_obj.Value; } set { year_obj.Value = value; } }
        int Floor { get { return (int)floor_obj.Value; } set { floor_obj.Value = value; } }
        string Property_State { get { return property_state_obj.SelectedIndex.ToString(); } set { property_state_obj.SelectedIndex = 1; } }
        int Size { get { return (int)square_meters_obj.Value; } set { square_meters_obj.Value = value; } }


        RealEstate reToBeEdited = new RealEstate(0, null, null, 0, 0, 0, 0, 0, true, true, null, null, null, new List<string>());

        internal List<RealEstate> Data_bind_Edit { get { return _data_bind_edit; } 
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
        List<string> myproperties = new List<string>();

 

        public Edit_Properties()
        {
            this.InitializeComponent();
            
            myproperties.Add("/Assets/Properties_Pictures/zoo.png");
    
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
            current_file_list.Clear();
            database_file_list.Clear();
           
            reToBeEdited = (RealEstate)e.ClickedItem;
            myproperties = new List<String>(reToBeEdited.images);
            Type = reToBeEdited.type;
            Area = reToBeEdited.area;
            Info = reToBeEdited.details;
            Bedrooms = reToBeEdited.bedrooms;
            Year = reToBeEdited.year;
            Price = reToBeEdited.price;
            Floor= reToBeEdited.floor;
            switch (reToBeEdited.leaseSell)
            {
                case true:
                    Property_State = "1";
                    break;
                case false:
                    Property_State = "0";
                    break;
            }
                

            Size = reToBeEdited.size;
            
        }

        private void Delete_All_Photos_Click(object sender, RoutedEventArgs e)
        {
            Delete_All_Photos.Visibility = Visibility.Collapsed;
            Photos_Selection.Visibility = Visibility.Visible;
            Photos_Changes = true;

            reToBeEdited.images.Clear();
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
            database_file_list.Clear();
        }

        private async void Update_Property_Click(object sender, RoutedEventArgs e)
        {
            if (Photos_Changes)
            {
                foreach (string x in myproperties)
                {
                    string filePath = AppContext.BaseDirectory + x.Substring(1);

                    if (File.Exists(filePath))
                    {
                        try
                        {
                            // Διαγραφή του αρχείου
                            //Debug.WriteLine(x);
                            File.SetAttributes(filePath, System.IO.FileAttributes.Normal);
                            File.Delete(filePath);
                            //Debug.WriteLine("Operation Completed");
                        }catch (Exception ex)
                        {
                            //Debug.WriteLine(ex.ToString());
                        }
                    }
                }
                foreach (StorageFile file in current_file_list)
                {
                    if (file != null)
                    {

                        
                        string assetsFolderPath = AppContext.BaseDirectory + @"Assets\Properties_Pictures";
                        //Debug.WriteLine(assetsFolderPath);
                        
                        if (!Directory.Exists(assetsFolderPath))
                        {
                            Directory.CreateDirectory(assetsFolderPath);
                        }
                        String myString = x.member_variable.GetUsersID() + "_" + file.Name;
                        
                        string destinationFilePath = Path.Combine(assetsFolderPath, myString);
                        
                        await file.CopyAsync(await StorageFolder.GetFolderFromPathAsync(assetsFolderPath), myString, NameCollisionOption.ReplaceExisting);
                        File.SetAttributes(destinationFilePath, System.IO.FileAttributes.Normal);
                        current_file_path = @"/Assets/Properties_Pictures/" + x.member_variable.GetUsersID() + "_" + file.Name;
                        //Debug.WriteLine(current_file_path);
                        database_file_list.Add(current_file_path);
                        


                    }
                    else
                    {
                        //Debug.WriteLine("The operation hasn't completed!");
                    }
                }
            }
           

            if(database_file_list.Count==0)
            {
                database_file_list = reToBeEdited.images;
            }

            if (Property_State.Equals("1"))
            {
                reToBeEdited.ChangeRealEstateAttributes(Price, Size, Floor, Year, Bedrooms, true, true, Area, Type, Info, database_file_list);
            }
            else if (Property_State.Equals("0"))
            {
                reToBeEdited.ChangeRealEstateAttributes(Price, Size, Floor, Year, Bedrooms, true, false, Area, Type, Info, database_file_list);
            }

            database_file_list.Clear();

            x.TeachingTip.Title = "Επιτυχής Ενημέρωση Ακινήτου";
            x.TeachingTip.Subtitle = "Η διαδικασία ολοκληρώθηκε επιτυχώς !";
            x.TeachingTip.IsOpen = true;
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((ComboBox)sender).SelectedValue.ToString().Equals("Πώληση"))
            {
                //Data_bind_Edit = test_list;
                Pane_Type.Text = "προς Πώληση";
                Data_bind_Edit = new List<RealEstate>(DatabaseController.GetMyRealEstates(x.member_variable.GetUsersID(), false));
            }
            else
            {
                //Data_bind_Edit = myproperties;
                Pane_Type.Text = "προς Εκμίσθωση";
                Data_bind_Edit = new List<RealEstate>(DatabaseController.GetMyRealEstates(x.member_variable.GetUsersID(), true));
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
            {
                property_state_obj.IsEnabled = true;
            }
            else
            {
                switch (reToBeEdited.leaseSell)
                {
                    case true:
                        Property_State = "1";
                        break;
                    case false:
                        Property_State = "0";
                        break;
                }
                property_state_obj.IsEnabled = false;
            }
        }
    }
}
