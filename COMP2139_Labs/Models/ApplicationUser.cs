using Microsoft.AspNetCore.Identity;

namespace COMP2139_Labs.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int usernameChamgeLimit { get; set; } = 10;
        public byte[]? ProfilePicture { get; set; }
    }
}
