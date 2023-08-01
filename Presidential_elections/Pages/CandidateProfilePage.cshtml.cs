using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presidential_elections.Models;

namespace Presidential_elections.Pages
{
    public class CandidateProfilePageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUser CandidateProfile { get; set; }

        public CandidateProfilePageModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(string id)
        {
            CandidateProfile = _context.Users.Find(id);
        }
    }
}
