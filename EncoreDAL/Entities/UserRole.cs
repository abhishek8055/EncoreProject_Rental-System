using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreDAL
{
    public class UserRole
    {
        public int RId { get; set; }
        public string RName { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
