using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presidential_elections.Models;



namespace Presidential_elections.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public List<Candidates> ListOfCandidates = new List<Candidates>();

        public int AutoIncrement = 1;

        public IndexModel(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        

        public async void OnGet()
        {
            ListOfCandidates = _db.Candidates.ToList();
            ListOfCandidates = ListOfCandidates.OrderByDescending(x => x.NumberOfVotes).Take(10).ToList();
        }

        public async Task<IActionResult> OnPostCandidate()
        {
            var user = await _userManager.GetUserAsync(User);
            var existingCandidate = _db.Candidates.FirstOrDefault(x => x.Email == user.Email);
            if (existingCandidate == null)
            {
                _db.Candidates.Add(new Candidates
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    NumberOfVotes = 0
                });
                user.Candidated = true;
                _db.SaveChanges();
            }
            ListOfCandidates = _db.Candidates.ToList();
            return RedirectToPage();
        }

    }
}