using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Technology.Classes
{
    class Admins:Users
    {
        public Admins(int _usersID, string username, string _password) : base(_usersID, username, _password) { }

        public void InsertAreas() { }

        public void DeleteAreas() { }

        public void DeleteRealEstate() { }

        public void DeleteUser() { }
    }
}
