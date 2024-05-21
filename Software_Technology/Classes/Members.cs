using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Software_Technology.Classes
{
    class Members:Users
    {
        private readonly int _customerID;
        private string _email { get; set; }
        private string _phoneNumber { get; set; }
        public Members() { }

        public static string ShowRealEstateToBuy() { return ""; }
        public static string ShowRealEstateToRent() { return ""; }
        public static string ViewRealEstateInformation() { return ""; }
        public void BuyRealEstate() { }
        public void SellRealEstate() { }
        public void RentRealEstate() { }
        public void LeaseRealEstate() { }
        public void ShowMyPurchased_RentedRealEstates() { }
        public void ShowMySold_LeasedRealEstates() { }
        public void DeleteMyRealEstate() { }
        public void ChangeContactDetails() { }
    }
}
