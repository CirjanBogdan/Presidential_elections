using Microsoft.AspNetCore.Identity;

namespace Presidential_elections.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public bool Candidated { get; set; }
        public bool Voted { get; set; }
    }
}
