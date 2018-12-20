using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreDAL
{
    public class Category
    {
        public int CId { get; set; }
        public string CName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
