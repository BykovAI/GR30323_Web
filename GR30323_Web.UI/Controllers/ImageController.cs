using GR30323_Web.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GR30323_Web.UI.Controllers
{
    public class ImageController(UserManager<AppUser> userManager) : Controller
    {
        public async Task<IActionResult> GetAvatar()
        { 
            if(User.Identity.IsAuthenticated)
            {
                var email = User.FindFirst(ClaimTypes.Email).Value;
                var user = await userManager.FindByEmailAsync(email);
                if (user.Avatar is not null)
                        return File(user.Avatar, "image/*");
            }
            return File("user.Avatar", "image/*");
        }
    }
}
