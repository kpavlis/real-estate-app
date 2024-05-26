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
    
    public sealed partial class Sign_Up_Page : Page
    {

        internal string Username { get { return username_obg.Text; } }
        internal string Password { get { return password_obj.Password; } }
        internal string Phone { get { return phone_obj.Text; } }
        internal string Email { get { return email_obj.Text; } }
        internal string Name { get { return name_obj.Text; } }
        internal string Surname { get { return surname_obj.Text; } }

        public Sign_Up_Page()
        {
            this.InitializeComponent();
            
        }
    }
}
