using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities;

//[Table("ProductBrands")]
[Index(nameof(Name), IsUnique = true)]
public class Brand : NamedEntity, IOrderedEntity
{
    //[Column("BrandOrder")]
    public int Order { get; set; }

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}
