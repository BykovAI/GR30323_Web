using Microsoft.AspNetCore.Identity;

namespace GR30323_Web.UI.Data
{
    public class AppUser:IdentityUser
    {
        public byte[]? Avatar { get; set ; }

    }
}
