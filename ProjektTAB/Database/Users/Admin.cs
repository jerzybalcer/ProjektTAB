using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Users
{
    public class Admin : User
    {
        public Admin(string name, string surname, UserAccount userAccount) : base(name, surname, userAccount)
        {
        }

        public Admin() : base()
        {

        }
    }
}
