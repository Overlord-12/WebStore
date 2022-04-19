using System.Security.Claims;

namespace WebStore.Api.Client.DTO
{
    public class ClaimDTO : UserDTO
    {
        public IEnumerable<Claim> Claims { get; set; } = null!;
    }


    public class ReplaceClaimDTO : UserDTO
    {
        public Claim Claim { get; set; } = null!;

        public Claim NewClaim { get; set; } = null!;
    }
}
