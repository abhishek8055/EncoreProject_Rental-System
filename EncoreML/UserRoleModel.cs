//@AUTHOR ABHISHEK DWIVEDI
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreML
{
    public class UserRoleModel
    {
        public int RId { get; set; }
        public string RName { get; set; }

        public virtual ICollection<UserLoginModel> UserLogins { get; set; }
    }
}
