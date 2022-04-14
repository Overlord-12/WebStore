using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Interface.Interfaces;

namespace WebStore.WebApi.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductData _productData;
        public ProductController(IProductData productData )=> _productData = productData;

        [HttpGet("getProductsBySectionAndBrand")]
        public IActionResult GetProductsBySectionAndBrand(int? sectionId, int? brandId)
        {
            var filter = new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
            };

            var products = _productData.GetProducts(filter);

            return Ok(products);
        }

        [HttpGet("getAdditionalInfromationById/{id}")]
        public IActionResult GetAdditionalInfromationById(int id)
        {
            var product = _productData.GetProductById(id);

            return Ok(product);
        }

        [HttpGet("getBrandById/{id}")]
        public IActionResult GetBrandById(int id)
        {
            var brand = _productData.GetBrandById(id);

            return Ok(brand);
        }

        [HttpGet("getProducts")]
        public IActionResult GetProducts()
        {
            var products = _productData.GetProducts();

            return Ok(products);
        }

        [HttpGet("getBrands")]
        public IActionResult GetBrands()
        {
            var brands = _productData.GetBrands();

            return Ok(brands);
        }

        [HttpGet("getSection")]
        public IActionResult GetSections()
        {
            var sections = _productData.GetSections();

            return Ok(sections);
        }

        [HttpGet("getSectionById/{id}")]
        public IActionResult GetSectionById(int id)
        {
            var section = _productData.GetSectionById(id);

            return Ok(section);
        }

    }
}
