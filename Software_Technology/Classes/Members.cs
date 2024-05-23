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
        public string email { get; private set; }
        public string phoneNumber { get; private set; }
        public Members(string email, string phoneNumber, int _usersID, string username,string _password) :base(_usersID, username, _password)
        {
            this.email = email;
            this.phoneNumber = phoneNumber;
        }
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
        public void ChangeContactDetails(string newEmail, string newPhoneNumber)
        {
            this.email = newEmail;
            this.phoneNumber = newPhoneNumber;
        }
    }
}
