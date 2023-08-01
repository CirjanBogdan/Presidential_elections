using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presidential_elections.Models;

namespace Presidential_elections.Pages
{
    public class ListOfCandidatesModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<Candidates> ListOfCandidates = new List<Candidates>();
        public List<ApplicationUser> UserList { get; set; }
        public ApplicationUser SignedUser { get; set; }
        public int AutoIncrement = 1;

        public ListOfCandidatesModel(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            SignedUser = await _userManager.GetUserAsync(User);
            UserList = _db.Users.ToList();
            UserList = UserList.OrderBy(x => x.FirstName).ToList();
        }

        public async Task OnPostVote(string id)
        {
            var candidate = _db.Users.FirstOrDefault(c => c.Id == id);
            ListOfCandidates = _db.Candidates.ToList();
            for (int i = 0; i < ListOfCandidates.Count; ++i)
            {
                if (candidate.Email == ListOfCandidates[i].Email)
                {
                    var user = _db.Candidates.FirstOrDefault(u => u.Email == ListOfCandidates[i].Email);
                    ++user.NumberOfVotes;
                    
                }
            }
            SignedUser = await _userManager.GetUserAsync(User);
            SignedUser.Voted = true;
            _db.SaveChanges();
            UserList = _db.Users.ToList();
            UserList = UserList.OrderBy(x => x.FirstName).ToList();
        }
    }
}
