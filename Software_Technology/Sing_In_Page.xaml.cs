using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Software_Technology
{
    
    public sealed partial class Sing_In_Page : Page
    {
        MainWindow x;
        public Sing_In_Page(MainWindow current)
        {
            this.InitializeComponent();
            x = current;
        }

        private async void Pop_Up_Click(object sender, RoutedEventArgs e)
        {

            x.Sign_Up_Click(sender, e);
            
        }
    }
}
