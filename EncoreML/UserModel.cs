using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreML
{
    public class UserModel
    {
        public int UId { get; set; }
        public string UName { get; set; }
        public string UEmail { get; set; }
        public string UContact { get; set; }
        public int UAge { get; set; }
        public string UAddress { get; set; }
        public int UPaymentId { get; set; }
        public bool UValid { get; set; }
        public string UPhoto { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
