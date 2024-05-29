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

        public void DeleteRealEstate(RealEstate realEstateToBeDeleted,Members member) { }

        public void DeleteMember(Members memberToBeDeleted) { }
    }
}
