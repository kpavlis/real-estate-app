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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Software_Technology.Navigation_UI_Pages
{
    
    public sealed partial class Delete_Properties : Page, INotifyPropertyChanged
    {
        MainWindow x;

        public event PropertyChangedEventHandler PropertyChanged;

        

        List<int> _data_bind_delete = new List<int>();

        public List<int> Data_bind_Delete
        {
            get { return _data_bind_delete; }
            set
            {
                if (_data_bind_delete != value)
                {
                    _data_bind_delete = value;
                    OnPropertyChanged(nameof(Data_bind_Delete));
                }

            }

        }

        public Delete_Properties()
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

        private void Delete_Property_Click(object sender, RoutedEventArgs e)
        {
            
            ((Button)sender).IsEnabled = false;
            
        
            int realEstateIDtoDelete = (int)((Button)sender).Tag;
            List<String> imagesToDelete = new List<String>(DatabaseController.GetRealEstatesFrorDeleteImages(realEstateIDtoDelete));
            x.member_variable.DeleteMyRealEstateMember(realEstateIDtoDelete);

            

            foreach (string x in imagesToDelete)
            {
                string filePath = AppContext.BaseDirectory + x.Substring(1);

                if (File.Exists(filePath))
                {
                    try { 
                    // Διαγραφή του αρχείου
                    //Debug.WriteLine(x);
                    File.SetAttributes(filePath, System.IO.FileAttributes.Normal);
                    File.Delete(filePath);
                    //Debug.WriteLine("Operation Completed");
                    } catch(Exception ex)
                    {
                        //Debug.WriteLine(ex.ToString());
                    }
                }
            }


            x.TeachingTip.Title = "Επιτυχής Διαγραφή Ακινήτου";
            x.TeachingTip.Subtitle = "Η διαδικασία ολοκληρώθηκε επιτυχώς !";
            x.TeachingTip.IsOpen = true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Combo_Selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (((ComboBox)sender).SelectedValue.ToString().Equals("Πώληση"))
            {
                
                Data_bind_Delete = new List<int>(DatabaseController.GetMyRealEstatesForDelete(x.member_variable.GetUsersID(), false));
                
            }
            else if(((ComboBox)sender).SelectedValue.ToString().Equals("Εκμίσθωση"))
            {
                Data_bind_Delete = new List<int>(DatabaseController.GetMyRealEstatesForDelete(x.member_variable.GetUsersID(), true));
                
            }
        }
    }
}
