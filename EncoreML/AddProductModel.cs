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
    public class AddProductModel
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Upload Image 1")]
        public string Image1 { get; set; }

        [Display(Name = "Upload Image 2")]
        public string Image2 { get; set; }

        [Display(Name = "Upload Image 3")]
        public string Image3 { get; set; }
        public bool Availability { get; set; }

        [Display(Name = "From Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "To Date")]
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "Cost Per Day")]
        public double UnitPrice { get; set; }


        [Display(Name = "Upload Image 1")]
        [Required(ErrorMessage = "Upload Image 1")]
        public HttpPostedFileBase UploadImage1 { get; set; }


        [Display(Name = "Upload Image 2")]
        public HttpPostedFileBase UploadImage2 { get; set; }

        [Display(Name = "Upload Image 3")]
        public HttpPostedFileBase UploadImage3 { get; set; }
    }
}
