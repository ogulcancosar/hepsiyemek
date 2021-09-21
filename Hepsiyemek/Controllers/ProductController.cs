using Hepsiyemek.Models;
using Hepsiyemek.Redis;
using Hepsiyemek.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiyemek.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly ICacheService _cacheService;


        public ProductController(ProductService productService, CategoryService categoryService, ICacheService cacheService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cacheService = cacheService;
        }

        [HttpGet]
        public List<Product> Get()
        {
            var allProduct = _productService.Get();

            foreach (var item in allProduct)
            {
                if (item.categoryId != null)
                {
                    var productcat = _categoryService.Get(item.categoryId);
                    item.category = productcat;
                }
            }

            return allProduct;
        }

        [HttpGet("{id:length(24)}", Name = "GetProducts")]
        public ActionResult<Product> Get(string id)
        {

            var cached = _cacheService.Get<Product>(id.ToString());

            if (cached != null) return cached;
            else
            {
                var product = _productService.Get(id);

                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    var productcat = _categoryService.Get(product.categoryId);
                    product.category = productcat;
                }

                // insert into cache for future calls
                return _cacheService.Set<Product>(id.ToString(), product);
            }
          
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _productService.Create(product);
            return CreatedAtRoute("GetProduct", new { id = product.id.ToString() }, product);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Product bookIn)
        {

            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Update(id, bookIn);


            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Remove(product.id);

            return NoContent();
        }
    }
}