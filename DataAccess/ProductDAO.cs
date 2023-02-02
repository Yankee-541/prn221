using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProduct()
        {
            var listProduct = new List<Product>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listProduct = context.Products.ToList();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProduct;
        }

        public static Product FindProductById(int pId)
        {
            Product p = new Product();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    p = context.Products.SingleOrDefault(x => x.ProductId == pId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;

        }
        public static void SaveProduct(Product p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateProduct(Product p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteProduct(Product p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var p1 = context.Products.SingleOrDefault(x => x.ProductId == p.ProductId);
                    context.Products.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
