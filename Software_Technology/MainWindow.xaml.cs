using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Display;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT;
using System.Diagnostics;
using Windows.Foundation.Metadata;
using System.Threading;
using Software_Technology.Navigation_UI_Pages;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System.Xml.Linq;
using Software_Technology.Classes;
using System.Data.SQLite;


namespace Software_Technology
{
    public sealed partial class MainWindow : Window
    {
        public ContentDialog dialog = new ContentDialog();

        String connectionString = "Data source=realEstateApp.db;Version=3";
        SQLiteConnection connection;

        // Attributes for Windowing - Start

        IntPtr hWnd = IntPtr.Zero;
        private SUBCLASSPROC SubClassDelegate;
        
        int width = 450;
        int height = 400;

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref bool attrValue, int attrSize);

        // Attributes for Windowing - End

        

        public MainWindow()
        { 
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            String createSQLMembers = "Create table if not exists Members(email Text,usersID Text primary key,username Text,name Text,surname Text,phoneNumber Text,hashedPassword Text,soldRealEstates Text,boughtRealEstates Text,leasedRealEstates Text,rentedRealEstates Text)";
            SQLiteCommand commandMembers = new SQLiteCommand(createSQLMembers, connection);
            commandMembers.ExecuteNonQuery();

            String createSQLAdmins = "Create table if not exists Admins(usersID Text,username Text,name Text,surname Text,hashedPassword Text)";
            SQLiteCommand commandAdmins = new SQLiteCommand(createSQLAdmins, connection);
            commandAdmins.ExecuteNonQuery();
            connection.Close();

            hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            SizeWindow();

            this.InitializeComponent();
            this.SystemBackdrop = new MicaBackdrop();
            dialog.PrimaryButtonClick += Log_In;
            dialog.SecondaryButtonClick += Sign_Up;
            nvSample.SelectedItem = nvSample.MenuItems[0];

            AppWindow.Title = "Real Estate";
            AppWindow.SetIcon("Assets/house_icon.ico");

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(TitleBar);
        }



        // Methods for Window Resize - Start //

        private async Task SizeWindow()
        {
            var displayList = await DeviceInformation.FindAllAsync
                              (DisplayMonitor.GetDeviceSelector());

            if (!displayList.Any())
                return;

            var monitorInfo = await DisplayMonitor.FromInterfaceIdAsync(displayList[0].Id);



            if (monitorInfo == null)
            {
                width = 450;
                height = 400;
                SubClassDelegate = new SUBCLASSPROC(WindowSubClass);
                bool bReturn = SetWindowSubclass(hWnd, SubClassDelegate, 0, 0);

            }
            else
            {
                double dheight = monitorInfo.NativeResolutionInRawPixels.Height / 1.5;
                double dwidth = monitorInfo.NativeResolutionInRawPixels.Width / 1.4;

                height = (int)dheight;
                width = (int)dwidth;
                SubClassDelegate = new SUBCLASSPROC(WindowSubClass);
                bool bReturn = SetWindowSubclass(hWnd, SubClassDelegate, 0, 0);

            }
        }

        private int WindowSubClass(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData)
        {
            switch (uMsg)
            {
                case WM_GETMINMAXINFO:
                    {
                        MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
                        mmi.ptMinTrackSize.X = width;
                        mmi.ptMinTrackSize.Y = height;
                        Marshal.StructureToPtr(mmi, lParam, false);
                        return 0;
                    }
                    break;
            }
            return DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        public delegate int SUBCLASSPROC(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData);

        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern bool SetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, uint uIdSubclass, uint dwRefData);

        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern int DefSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        public const int WM_GETMINMAXINFO = 0x0024;

        public struct MINMAXINFO
        {
            public System.Drawing.Point ptReserved;
            public System.Drawing.Point ptMaxSize;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Point ptMinTrackSize;
            public System.Drawing.Point ptMaxTrackSize;
        }

        // Methods for Window Resize - End //


        internal async void Sign_In_Click(object sender, RoutedEventArgs e)
        {

            
            dialog.Title = "Σύνδεση/Εγγραφή";
            dialog.PrimaryButtonText = "Σύνδεση";
            dialog.SecondaryButtonText = "";
            dialog.CloseButtonText = "Κλείσιμο";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new Sign_In_Page(this);

            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
            {
                dialog.XamlRoot = nvSample.XamlRoot;
            }

            await dialog.ShowAsync();
           
            
        }

        internal void Sign_Up_Click(object sender, RoutedEventArgs e)
        {


            dialog.Title = "Εγγραφή";
            dialog.PrimaryButtonText = "";
            dialog.SecondaryButtonText = "Εγγραφή";
            dialog.CloseButtonText = "Κλείσιμο";
            dialog.DefaultButton = ContentDialogButton.Secondary;
            dialog.Content = new Sign_Up_Page();
            
        }

        internal void Sign_Out_Click(object sender, RoutedEventArgs e)
        {

            //Code will be added here

        }

        private void Log_In(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            List<string> logInValues = Users.LogIn(((Sign_In_Page)sender.Content).Username, ((Sign_In_Page)sender.Content).Password);
            logInValues.Add(((Sign_In_Page)sender.Content).Username);
            if (!logInValues.Count.Equals(1) && logInValues[6].StartsWith("A"))
            {
                //ID=0,Name=1,Surname=2,EncryptedPassword=3,Username=4
                Admins admin = new Admins(logInValues[0], logInValues[4], logInValues[1], logInValues[2], logInValues[3]);

            }
            else if (!logInValues.Count.Equals(1) && logInValues[6].StartsWith("M"))
            {
                //ID=0,Email=1,Name=2,Surname=3,PhoneNumber=4,EncryptedPassword=5,USername=6
                Members member = new Members(logInValues[1], logInValues[0], logInValues[6], logInValues[2], logInValues[3], logInValues[4], logInValues[5]);
                member.UpdateRealEstatesListMember(logInValues[6]); //Show my sold real estates
                Debug.WriteLine(member.soldRealEstates.Count());
                Debug.WriteLine(member.boughtRealEstates.Count());
            }
        }


        private void Sign_Up(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            Random random = new Random();
            string memberID = "M" + random.Next(1000, 5001);
            Members member = new Members(((Sign_Up_Page)sender.Content).Email, memberID, ((Sign_Up_Page)sender.Content).Username, ((Sign_Up_Page)sender.Content).Name, ((Sign_Up_Page)sender.Content).Surname, ((Sign_Up_Page)sender.Content).Phone,((Sign_Up_Page)sender.Content).Password);
            member.SignUpMember(member.email,memberID,member.username,member.name,member.surname,member.phoneNumber, member.GetPassword());
            //app main window (successfull sign in == successfull log in)
        } 



        private void nvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                //contentFrame.Navigate(typeof(Page_Settings), x);
            }
            else if (args.SelectedItem != null)
            {

                NavigationViewItem item = args.SelectedItem as NavigationViewItem;


                if (item != null && item.Tag != null)
                {
                    string selectedTag = item.Tag.ToString();
                    switch (selectedTag)
                    {
                        case "Start":
                            contentFrame.Navigate(typeof(Home));
                            break;
                        case "Homes_for_Sale":
                            contentFrame.Navigate(typeof(Real_Estate_for_Sale));
                            break;
                        case "Homes_for_Rent":
                            contentFrame.Navigate(typeof(Real_Estate_for_Rent));
                            break;
                        case "Submit_Home":
                            contentFrame.Navigate(typeof(Submit_Home), this);
                            break;
                        case "Page5":
                            //contentFrame.Navigate(typeof(Page_Customer_5), x);
                            break;
                        case "Page6":
                            //contentFrame.Navigate(typeof(Page_Customer_6));
                            break;
                        case "Page7":
                            //contentFrame.Navigate(typeof(Page_Customer_7));
                            break;
                        case "Settings":
                            //contentFrame.Navigate(typeof(Page_Settings), x);
                            break;

                    }
                }
            }
        }
    }

}
