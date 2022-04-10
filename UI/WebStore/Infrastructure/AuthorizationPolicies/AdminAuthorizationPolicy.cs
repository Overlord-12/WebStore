using Microsoft.AspNetCore.Authorization;

namespace WebStore.Infrastructure.AuthorizationPolicies;

public class AdminAuthorizationPolicy : IAuthorizationRequirement
{
    public string RoleName { get; set; }

    public AdminAuthorizationPolicy()
    {
        
    }

    public AdminAuthorizationPolicy(string RoleName) => this.RoleName = RoleName;
}
