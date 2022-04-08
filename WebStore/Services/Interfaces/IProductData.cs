using WebStore.Domain;
using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces;

public interface IProductData
{
    IEnumerable<Section> GetSections();

    Section? GetSectionById(int Id);

    IEnumerable<Brand> GetBrands();

    Brand? GetBrandById(int Id);

    IEnumerable<Product> GetProducts(ProductFilter? Filer = null);

    Product? GetProductById(int Id);
}