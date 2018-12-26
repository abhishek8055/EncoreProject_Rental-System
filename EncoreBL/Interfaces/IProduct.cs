using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//@AUTHOR ABHISHEK DWIVEDI
//IPRODUCT INTERFACE DECLEARS ALL THE PRODUCT SPECIFIC METHODS
//ALL METHODS ARE IMPLEMENTED BY PRODUCT_ACTIONS CLASS

namespace EncoreBL.Interfaces
{
    public interface IProduct
    {
        bool AddProduct(ProductModel product);
        ProductModel GetProductById(int productId);
        IEnumerable<ProductModel> GetProducts();
        IEnumerable<ProductModel> GetProductsByVendorId(int vendorId);
        IEnumerable<CategoryModel> GetCategories();
        bool UpdateProduct(ProductModel productModel);
        bool DeleteProduct(int id);
        bool RentNewProduct(RentProductModel rentProductModel);
        IEnumerable<RentProductModel> GetRentedProductsByUserId(int id);
        IEnumerable<RentProductModel> GetRentedProductsByVendorId(int vendorId);
        bool ApproveBookingStatus(int productId);
        bool MakeProductUnavailable(int productId);
    }
}
