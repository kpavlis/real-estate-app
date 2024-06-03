using Microsoft.WindowsAppSDK.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Software_Technology.Classes
{
    class Members:Users
    {
        public string email { get; private set; }
        public string phoneNumber { get; private set; }
        public string hashedPassword { get; private set; }

        public List<RealEstate> soldRealEstates = new List<RealEstate>();
        public List<RealEstate> boughtRealEstates = new List<RealEstate>();
        List<RealEstate> leasedRealEstates = new List<RealEstate>();
        List<RealEstate> rentedRealEstates = new List<RealEstate>();
        
        
        public Members(string email, string _usersID, string username, string  name, string surname,string phoneNumber, string _password) :base(_usersID, username, name, surname, _password)
        {
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        public void SignUpMember(string email, string usersID, string username, string name, string surname, string phoneNumber, string password)
        {
            hashedPassword = HashPassword(password);
            DatabaseController.SignUp(email, usersID, username, name, surname, phoneNumber, hashedPassword);
        }

        public void AddRealEstate(RealEstate realEstate)
        {
            if(realEstate.leaseSell==true)
            {
                leasedRealEstates.Add(realEstate);
            }
            else
            {
                soldRealEstates.Add(realEstate);
            }

            
           
        }
        
        public static string ShowRealEstateToBuy() { return ""; }
        public static string ShowRealEstateToRent() { return ""; }
        public static string ViewRealEstateInformation() { return ""; }
        public void BuyRealEstate(RealEstate realEstateToBeBought) { }
        public void SellRealEstate(RealEstate realEstateToBeSold) { }
        public void RentRealEstate(RealEstate realEstateToBeRented) { }
        public void LeaseRealEstate(RealEstate realEstateToBeDeLeased) { }
        public void ShowMyPurchased_RentedRealEstates() { }
        public void ShowMySold_LeasedRealEstates() { }
        public void DeleteMyRealEstate(RealEstate realEstateToBeDeleted) { }
        public void ChangeContactDetails(string newEmail,string newPhoneNumber)
        {
            this.email = newEmail;
            this.phoneNumber = newPhoneNumber;
            //DatabaseController.UpdateContactDetails()
        }

        
    }
}
