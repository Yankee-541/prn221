using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace PrjManagementApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();
        [HttpGet]
        public ActionResult<IEnumerable<Product>> getProducts() => repository.GetProducts();

        [HttpPost]
        public IActionResult PostProduct(Product p)
        {
            repository.SaveProduct(p);
            return NoContent();
        }
        [HttpDelete("id")]
        public IActionResult deleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if(p == null)
            {
                return NotFound();
            }
            repository.DeleteProduct(p);
            return NoContent();
        }
        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var p = repository.GetProductById(id);
            if(p == null)
            {
                return NotFound();
            }
            repository.UpdateProduct(p);
            return NoContent();
        }


    }
}
