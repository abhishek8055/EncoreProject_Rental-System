//@AUTHOR ABHISHEK DWIVEDI
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreML
{
    public class RentProductModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        [Display(Name="Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Booked From")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Booked To")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Payable Amount")]
        public double PayableAmount { get; set; }

        [Display(Name = "Payment Status")]
        public bool PayStatus { get; set; }

        [Display(Name = "Display Photo")]
        public string ProductImage { get; set; }

        [Display(Name = "Booking Status")]
        public bool BookingStatus { get; set; }

        public int CategoryId { get; set; }

        public int VendorId { get; set; }

        public virtual CategoryModel Category { get; set; }
        public virtual UserLoginModel User { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}
