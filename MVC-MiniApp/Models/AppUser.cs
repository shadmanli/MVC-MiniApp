using Microsoft.AspNetCore.Identity;

namespace MVC_MiniApp.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }

    }
}
