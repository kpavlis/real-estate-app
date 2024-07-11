using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Software_Technology.Navigation_UI_Pages;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Software_Technology.Classes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Software_Technology.Special_UI_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Real_Estate_Info : Page
    {
        

        RealEstate _object;

        string Phone_Number { get { return phone_number.Text; } set { phone_number.Text = value; } }
        string Email { get { return email.Text; } set { email.Text = value; } }

        internal Real_Estate_Info(RealEstate x)
        {
            this.InitializeComponent();
            _object = x;


            List<String> contactDetailsList = new List<String>(DatabaseController.GetMemberContactDetails(x.seller_lessorID));
            Email = contactDetailsList[0];
            Phone_Number = contactDetailsList[1];

        }



        
    }
}
