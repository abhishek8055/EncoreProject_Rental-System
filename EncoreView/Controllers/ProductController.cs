using EncoreBL.Repositories;
using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncoreView.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ProductActions pa = new ProductActions();

        public ActionResult Index()
        {
            IEnumerable<ProductModel> productList = null;
            productList = pa.GetProducts();
            return View(productList);
        }

        public ActionResult ProductForm()
        {
            IEnumerable<CategoryModel> categoryList = pa.GetCategories();
            ViewBag.Category = categoryList;
            return View();
        }

        public ActionResult Save(ProductModel product)
        {
            product.VendorId = 1;
            bool status = pa.AddProduct(product);

            return RedirectToAction("Index", "Product");
        }

        public ActionResult EditProduct(int id)
        {
            ProductModel product = pa.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            IEnumerable<CategoryModel> categoryList = pa.GetCategories();
            ViewBag.Category = categoryList;
            return View(product);
        }

        public ActionResult Update(ProductModel product)
        {
            product.VendorId = 1;
            bool status = pa.UpdateProduct(product);

            return RedirectToAction("Index", "Product");
        }

        public ActionResult Delete(int id)
        {
            bool status = pa.DeleteProduct(id);

            return RedirectToAction("Index", "Product");
        }
    }
}