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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Submit_Home : Page
    {
        int counter = 0;
        MainWindow x;
        StorageFile current_file;
        string current_file_path;
        List<StorageFile> current_file_list = new List<StorageFile>();
        List<string> database_file_list = new List<string>();

        //Properties
        string Type { get { return type_obj.Text; } }
        string Area { get { return area_obj.Text; } }
        string Info { get { return info_obj.Document.ToString(); } }
        int Bedrooms { get { return (int)bedrooms_obj.Value; } }
        int Price { get { return (int)price_obj.Value; } }
        int Year { get { return (int)year_obj.Value; } }
        int Floor { get { return (int)floor_obj.Value; } }
        string Property_State { get { return property_state_obj.Text; } }
        int Size { get { return (int)square_meters_obj.Value; } }

        public Submit_Home()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                x = e.Parameter as MainWindow;
            }
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

        private async void Submit_Property_Click(object sender, RoutedEventArgs e)
        {
            ring.IsActive = true;

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
                    current_file_path = @"Assets/Properties_Pictures/" + file.Name;
                    Debug.WriteLine(current_file_path);
                    database_file_list.Add(current_file_path);
                    //Debug.WriteLine(file_path);
                    //Debug.WriteLine(File.Exists(AppContext.BaseDirectory + file_path).ToString());
                    //Debug.WriteLine("The Operation Completed!");
                    Random random = new Random();
                    int realEstateID = random.Next(1000, 5001);
                    if (Property_State.Equals("Πώληση"))
                    {
                        RealEstate realEstate = new RealEstate(realEstateID, x.member_variable.GetUserID(),null, Price, (int)square_meters_obj.Value, Floor, Year, Bedrooms, true, false, Area, Type, Info, database_file_list);
                        x.member_variable.AddRealEstate(realEstate);
                    }
                    else if (Property_State.Equals("Ενοικίαση"))
                    {
                        RealEstate realEstate = new RealEstate(realEstateID, x.member_variable.GetUserID(), null, Price, (int)square_meters_obj.Value, Floor, Year, Bedrooms, true, true, Area, Type, Info, database_file_list);
                        x.member_variable.AddRealEstate(realEstate);
                    }

                    
                }
                else
                {
                    //Debug.WriteLine("The operation hasn't completed!");
                }
                ring.IsActive = false;
            }
        }

        private void Clean_Photos_Click(object sender, RoutedEventArgs e)
        {
            photos_group.Text = "Δεν έχεις επιλέξει φωτογραφία";
            current_file_list.Clear();
            add_photo_button.IsEnabled = true;
            counter = 0; 
        }
    }
}
