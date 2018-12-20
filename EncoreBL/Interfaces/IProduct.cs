using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreBL.Interfaces
{
    public interface IProduct
    {
        IEnumerable<ProductModel> GetProducts();
        ProductModel GetProductById(int productId);
        bool AddProduct(ProductModel product);
    }
}
