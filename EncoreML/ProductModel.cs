using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace EncoreML
{
    public class ProductModel
    {
        [Display(Name ="Product id")]
        public int PId { get; set; }

        [Display(Name ="Vendor id")]
        public int VendorId { get; set; }

        [Display(Name="Name")]
        public string PName { get; set; }

        [Display(Name = "Description")]
        public string PDescription { get; set; }

        [Display(Name = "Display Picture")]
        public string PImage1 { get; set; }

        [Display(Name = "Image 2")]
        public string PImage2 { get; set; }

        [Display(Name = "Image 3")]
        public string PImage3 { get; set; }

        [Display(Name = "Is Available")]
        public bool PAvailability { get; set; }

        [Display(Name = "Start Date")]
        public System.DateTime PStartDate { get; set; }

        [Display(Name = "End Date")]
        public System.DateTime PEndDate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Cost per unit")]
        public double PUnitCost { get; set; }

        public virtual CategoryModel Category { get; set; }
        public virtual UserModel User { get; set; }

        [Display(Name = "Upload Image 1")]
        [Required(ErrorMessage = "Upload Image 1")]
        public HttpPostedFileBase UploadImage1 { get; set; }


        [Display(Name = "Upload Image 2")]
        public HttpPostedFileBase UploadImage2 { get; set; }

        [Display(Name = "Upload Image 3")]
        public HttpPostedFileBase UploadImage3 { get; set; }
    }
}
