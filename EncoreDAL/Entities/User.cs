using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EncoreDAL
{
    public class User
    {
        public int UId { get; set; }
        public int URoleId { get; set; }
        public string UName { get; set; }
        public string UEmail { get; set; }
        public string UContact { get; set; }
        public int UAge { get; set; }
        public string UAddress { get; set; }
        public int? UPaymentId { get; set; }
        public bool? UValid { get; set; }
        public string UPhoto { get; set; }

        public HttpPostedFileBase UploadImage { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
