using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity;

public class Role : IdentityRole
{
    public const string Adinistrators = "Administrators";

    public const string Users = "Users";
}
