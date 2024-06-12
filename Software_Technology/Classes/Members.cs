using Microsoft.WindowsAppSDK.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Software_Technology.Classes
{
    class Members : Users
    {
        public string email { get; private set; }
        public string phoneNumber { get; private set; }
        public string hashedPassword { get; private set; }
        public List<RealEstate> soldRealEstates { get; private set; }
        public List<RealEstate> boughtRealEstates { get; private set; }
        public List<RealEstate> leasedRealEstates { get; private set; }
        public List<RealEstate> rentedRealEstates { get; private set; }






        public Members(string email, string _usersID, string username, string name, string surname, string phoneNumber, string _password, List<RealEstate> soldRealEstates, List<RealEstate> boughtRealEstates, List<RealEstate> leasedRealEstates, List<RealEstate> rentedRealEstates) : base(_usersID, username, name, surname, _password)
        {
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.soldRealEstates = new List<RealEstate>(soldRealEstates);
            this.boughtRealEstates = new List<RealEstate>(boughtRealEstates);
            this.leasedRealEstates = new List<RealEstate>(leasedRealEstates);
            this.rentedRealEstates = new List<RealEstate>(rentedRealEstates);
        }


        public void SignUpMember(string email, string usersID, string username, string name, string surname, string phoneNumber, string password)
        {
            hashedPassword = HashPassword(password);
            DatabaseController.SignUp(email, usersID, username, name, surname, phoneNumber, hashedPassword);
        }


        public void AddRealEstateMember(RealEstate realEstate)
        {
            if (realEstate.leaseSell == true)
            {
                leasedRealEstates.Add(realEstate);
            }
            else
            {
                soldRealEstates.Add(realEstate);
            }
            DatabaseController.AddRealEstate(realEstate);
            
        }



        public static List<RealEstate> ShowRealEstateToBuy_RentMember(String userIDExists, bool leaseSell, int location, int minSize, int minBedrooms, int maxPrice)
        {
            //List<int> mylist = new List<int>(DatabaseController.GetRealEstates());
            //Debug.WriteLine("to apotelesmata einai :" + mylist.Count.ToString());

            String reArea = "";

            


            switch (location)
            {
                case 1:
                    reArea = "Αθήνα";
                    break;
                case 2:
                    reArea = "Θεσσαλονίκη";
                    break;
            }
            List<RealEstate> allRealEstates= new List<RealEstate>(DatabaseController.ShowRealEstateToBuy_Rent(leaseSell));
            //var allRealEstates = soldRealEstates.Concat(boughtRealEstates).Concat(leasedRealEstates).Concat(rentedRealEstates).ToList();
            /*foreach (RealEstate i in allRealEstates)
            {
                Debug.WriteLine(i.realEstateID);
            }
            return allRealEstates;*/
            //Debug.WriteLine("ta akinita ola einai");
            Debug.WriteLine("GGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
            Debug.WriteLine(location);
            Debug.WriteLine(reArea);



            return allRealEstates
                .Where(re => (String.IsNullOrEmpty(reArea) || re.area.Contains(reArea,StringComparison.OrdinalIgnoreCase)) &&
                             (minSize == 0 || re.size >= minSize) &&
                             (String.IsNullOrEmpty(userIDExists) || !(re.seller_lessorID.Equals(userIDExists))) &&
                             (minBedrooms == 0 || re.bedrooms >= minBedrooms) &&
                             (maxPrice == 0 || re.price <= maxPrice))
                .ToList();
            

            

        }


        public bool Buy_Sell_Rent_LeaseRealEstateMember(RealEstate realEstate, string buyer_tenantID)
        {
            switch (realEstate.leaseSell)
            {
                case false:
                    if (!boughtRealEstates.Contains(realEstate))
                    {
                        boughtRealEstates.Add(realEstate);
                    }
                    break;
                case true:
                    if (!rentedRealEstates.Contains(realEstate))
                    {
                        rentedRealEstates.Add(realEstate);
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid list Type");
            }
            DatabaseController.Buy_Sell_Rent_LeaseRealEstate(realEstate, buyer_tenantID);
            return true;
        }
        public List<RealEstate> ShowMyPurchased_Rented_Sold_LeasedRealEstatesMember(string listType)
        {
            //List<RealEstate> membersRealEstates = DatabaseController.RetrieveUsersRealEstates(logInValues[0], logInValues[0]);
            switch (listType.ToLower())
            {
                case "sold":
                    return soldRealEstates;
                case "bought":
                    return boughtRealEstates;
                case "rented":
                    return rentedRealEstates;
                case "leased":
                    return leasedRealEstates;
                default:
                    throw new ArgumentException("Invalid list Type");
            }
        }

        public void DeleteMyRealEstateMember(int realEstateID)
        {

            var allRealEstates = soldRealEstates.Concat(leasedRealEstates).ToList();


            foreach (RealEstate realEstateToBeDeleted in allRealEstates)
            {
                if(realEstateToBeDeleted.realEstateID == realEstateID)
                {

                    if (soldRealEstates.Remove(realEstateToBeDeleted))
                    {
                        Debug.WriteLine("Real estate has been removed from soldRealEstates");

                    }
                    else if (leasedRealEstates.Remove(realEstateToBeDeleted))
                    {
                        Debug.WriteLine("Real estate has been removed from leasedRealEstates");

                    }
                    DatabaseController.DeleteRealEstateFromDatabase(realEstateID);
                }
            }


            
            
        }
        public void ChangeContactDetailsMember(string newEmail, string newPhoneNumber)
        {
            this.email = newEmail;
            this.phoneNumber = newPhoneNumber;
            string _usersID = GetUsersID();
            DatabaseController.UpdateContactDetails(_usersID, newEmail, newPhoneNumber);
        }



    }
}