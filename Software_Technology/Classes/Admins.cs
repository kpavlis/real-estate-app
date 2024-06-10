using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Technology.Classes
{
    class Admins:Users
    {
        public Admins(string _usersID, string username, string name, string surname, string _password) : base(_usersID, username, name, surname, _password) { }

        public bool DeleteRealEstate(RealEstate realEstateToBeDeleted) {

            if (realEstateToBeDeleted.buyer_tenantID == null) {
                return DatabaseController.DeleteRealEstateFromDatabase(realEstateToBeDeleted.realEstateID);
            }
            else
            {
                return false;
            }
        }

        public void DeleteMember(String memberToBeDeletedID) 
        {
            
            DatabaseController.DeleteMemberFromDatabase(memberToBeDeletedID);
            
        }

        public void HelloWorld()
        {
            Debug.WriteLine("HIII!");

        }
    }
}
