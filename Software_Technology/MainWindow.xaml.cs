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


namespace Software_Technology
{
    
    public sealed partial class MainWindow : Window
    {
        public ContentDialog dialog = new ContentDialog();
        IAsyncOperation<ContentDialogResult> result;

        // Attributes for Mica - Start

        WindowsSystemDispatcherQueueHelper m_wsdqHelper;
        Microsoft.UI.Composition.SystemBackdrops.MicaController m_micaController;
        Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration m_configurationSource;

        // Attributes for Mica - End


        // Attributes for Windowing - Start

        IntPtr hWnd = IntPtr.Zero;
        private SUBCLASSPROC SubClassDelegate;

        int x = 1;

        int width = 450;
        int height = 400;

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref bool attrValue, int attrSize);

        // Attributes for Windowing - End



        public MainWindow()
        {
            hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            SizeWindow(appWindow);

            this.InitializeComponent();
            this.SystemBackdrop = new MicaBackdrop();
            dialog.PrimaryButtonClick += Log_In;
            dialog.SecondaryButtonClick += Sign_Up;
            
            

            AppWindow.Title = "Real Estate";

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(TitleBar);
        }



        // Methods for Window Resize - Start //

        private async Task SizeWindow(AppWindow appWindow)
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




        // Methods for Mica - Start //
        bool TrySetMicaBackdrop()
        {
            if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
            {
                m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
                m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

                // Hooking up the policy object
                m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();
                this.Activated += Window_Activated;
                this.Closed += Window_Closed;
                ((FrameworkElement)this.Content).ActualThemeChanged += Window_ThemeChanged;

                // Initial configuration state.
                m_configurationSource.IsInputActive = true;
                SetConfigurationSourceTheme();

                m_micaController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();

                // Enable the system backdrop.
                // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                m_micaController.AddSystemBackdropTarget(this.As<ICompositionSupportsSystemBackdrop>());
                m_micaController.SetSystemBackdropConfiguration(m_configurationSource);
                return true; // Succeeded.
            }

            return false; // Mica is not supported on this system.
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            // Make sure any Mica/Acrylic controller is disposed so it doesn't try to
            // use this closed window.
            if (m_micaController != null)
            {
                m_micaController.Dispose();
                m_micaController = null;
            }
            this.Activated -= Window_Activated;
            m_configurationSource = null;
        }

        private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (m_configurationSource != null)
            {
                SetConfigurationSourceTheme();
            }
        }

        private void SetConfigurationSourceTheme()
        {
            switch (((FrameworkElement)this.Content).ActualTheme)
            {
                case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
                case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
            }
        }

        // Methods for Mica - End //

        internal async void Sign_In_Click(object sender, RoutedEventArgs e)
        {

            
            dialog.Title = "Σύνδεση/Εγγραφή";
            dialog.PrimaryButtonText = "Σύνδεση";
            dialog.SecondaryButtonText = "";
            dialog.CloseButtonText = "Κλείσιμο";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new Sing_In_Page(this);

            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
            {
                dialog.XamlRoot = nvSample.XamlRoot;
            }
            
            result = dialog.ShowAsync();
            
            await result;
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

        private void Log_In(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Debug.WriteLine(((Sing_In_Page)sender.Content).username.Text);
            Debug.WriteLine(((Sing_In_Page)sender.Content).password.Password);
        }

        private void Sign_Up(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Debug.WriteLine(((Sign_Up_Page)sender.Content).name.Text);
            Debug.WriteLine(((Sign_Up_Page)sender.Content).surname.Text);
            Debug.WriteLine(((Sign_Up_Page)sender.Content).email.Text);
        }


    }



    class WindowsSystemDispatcherQueueHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        struct DispatcherQueueOptions
        {
            internal int dwSize;
            internal int threadType;
            internal int apartmentType;
        }

        [DllImport("CoreMessaging.dll")]
        private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

        object m_dispatcherQueueController = null;
        public void EnsureWindowsSystemDispatcherQueueController()
        {
            if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
            {
                // one already exists, so we'll just use it.
                return;
            }

            if (m_dispatcherQueueController == null)
            {
                DispatcherQueueOptions options;
                options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
                options.threadType = 2;    // DQTYPE_THREAD_CURRENT
                options.apartmentType = 2; // DQTAT_COM_STA

                CreateDispatcherQueueController(options, ref m_dispatcherQueueController);
            }
        }
    }
}
