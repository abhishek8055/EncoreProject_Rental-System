using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EncoreDAL.Entities
{
    public class RentProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PayableAmount { get; set; }
        public bool PayStatus { get; set; }
        public string ProductImage { get; set; }
        public bool BookingStatus { get; set; }      
        public int CategoryId { get; set; }
        public int VendorId { get; set; }

        public virtual Category Category { get; set; }
        public virtual UserLogin UserLogin { get; set; }
        public virtual Product Product { get; set; }
    }
}
