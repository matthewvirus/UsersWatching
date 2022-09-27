using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UsersWebApp.Areas.Identity.Data;

namespace UsersWebApp.Areas.Identity.Pages {

    [Authorize]
    public class UsersModel : PageModel
    {
        private readonly UsersWebAppIdentityDbContext context;

        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;


        public UsersModel(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            UsersWebAppIdentityDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        public IList<User> Users = new List<User>();

        public IList<User> CheckedUsers = new List<User>();

        public void OnGet()
        {
            GetAllUsers();
            GetCheckedUsers();
        }

        // public async Task<IActionResult> OnPostBlockAsync()
        // {
        // }

        // public async Task<IActionResult> OnPostUnblockAsync()
        // {
        // }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            await signInManager.SignOutAsync();
            await userManager.DeleteAsync(currentUser);
            return RedirectToPage("/Account/Login");
        }

        public void GetAllUsers() 
        {
            List<User> AppUsers = context.Users.ToList();
            foreach (User U in AppUsers)
            {
                User user = new User
                {
                    Id = U.Id,
                    Name = U.Name,
                    Email = U.Email,
                    PasswordHash = U.PasswordHash,
                    RegistrationDate = U.RegistrationDate,
                    LastLoginDate = U.LastLoginDate,
                    Status = U.Status
                };
                Users.Add(user);
            }
        }

        public void GetCheckedUsers() {
            foreach (var user in Users)
            {
                if (user.IsChecked) {
                    CheckedUsers.Add(user);
                }
                CheckedUsers.Remove(user);
            }
        }
    }
}