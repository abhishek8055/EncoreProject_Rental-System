//@AUTHOR ABHISHEK DWIVEDI
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EncoreML
{
    public class UserModel
    {
        public int UId { get; set; }
        public int URoleId { get; set; }

        [Display(Name = "Name")]
        public string UName { get; set; }

        [Display(Name = "Email")]
        public string UEmail { get; set; }

        [Display(Name = "Contact")]
        public string UContact { get; set; }

        [Display(Name = "Age")]
        public int UAge { get; set; }

        [Display(Name = "Address")]
        public string UAddress { get; set; }

        [Display(Name = "Payment Id")]
        public int? UPaymentId { get; set; }

        [Display(Name = "Account Verified")]
        public bool? UValid { get; set; }

        public string UPhoto { get; set; }

        [Display(Name = "Upload Photo")]
        [Required(ErrorMessage = "Upload Profile Photo")]
        public HttpPostedFileBase UploadImage { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
