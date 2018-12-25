using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreBL.Interfaces
{
    interface IUser
    {
        IEnumerable<UserModel> GetUsers();
        bool AddUser(UserLoginModel user);
    }
}
