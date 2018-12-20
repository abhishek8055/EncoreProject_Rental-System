using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreML
{
    public class CategoryModel
    {
        public int CId { get; set; }
        public string CName { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
