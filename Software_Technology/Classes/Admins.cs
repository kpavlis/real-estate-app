using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Technology.Classes
{
    class Admins:Users
    {
        public Admins(string _usersID, string username, string name, string surname, string _password) : base(_usersID, username, name, surname, _password) { }

        public void InsertAreas() { }

        public void DeleteAreas() { }

        public void DeleteRealEstate() { }

        public void DeleteUser() { }
    }
}
