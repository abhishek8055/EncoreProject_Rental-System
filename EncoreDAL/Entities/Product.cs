using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EncoreDAL
{
    public class Product
    {
        public int PId { get; set; }
        public int VendorId { get; set; }
        public string PName { get; set; }
        public string PDescription { get; set; }
        public string PImage1 { get; set; }
        public string PImage2 { get; set; }
        public string PImage3 { get; set; }
        public bool PAvailability { get; set; }
        public System.DateTime PStartDate { get; set; }
        public System.DateTime PEndDate { get; set; }
        public int CategoryId { get; set; }
        public double PUnitCost { get; set; }

        
        public HttpPostedFileBase UploadImage1 { get; set; }
        public HttpPostedFileBase UploadImage2 { get; set; }
        public HttpPostedFileBase UploadImage3 { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}
