using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebStore.Api.Client.Base;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interface.Interfaces;
using WebStore.WebStore.Api.Client.DTO;
using WebStore.Domain.APIAdresses;

namespace WebStore.Api.Client.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(HttpClient client) : base(client, WebAPIAdresses.V1.Products)
        {
        }
        public IEnumerable<Section> GetSections()
        {
            var sections = Get<IEnumerable<SectionDTO>>($"{_Address}/getSection");
            return sections.FromDTO();
        }

        public Section? GetSectionById(int Id)
        {
            //var section = Get<Section>($"{Address}/sections/{Id}");
            var section = Get<SectionDTO>($"{_Address}/getSectionById/{Id}");
            return section.FromDTO();
        }
        public IEnumerable<Brand> GetBrands()
        {
            var brands = Get<IEnumerable<BrandDTO>>($"{_Address}/getBrands");
            return brands.FromDTO();
        }

        public Brand? GetBrandById(int Id)
        {
            var brands = Get<BrandDTO>($"{_Address}/getBrandById/{Id}");
            return brands.FromDTO();
        }

        public IEnumerable<Product> GetProducts(ProductFilter? Filer = null)
        {
            var response = Post($"_Adress/getProducts", Filer ?? new());
            var products = response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>().Result;
            return products.FromDTO();
        }

        public Product? GetProductById(int Id)
        {
            var product = Get<ProductDTO>($"{_Address}/getAdditionalInfromationById/{Id}");
            return product.FromDTO();
        }
    }
}
