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

        public void OnGet()
        {
            GetAllUsers();
        }

        [HttpPost]
        public async Task<IActionResult> UsersForm(string submitButton, string[] selectedUsers)
        {
            if (selectedUsers == null)
                return RedirectToPage("Account/Users");
            switch (submitButton)
            {
                case "Block":
                    return await Block(selectedUsers);
                case "Unlock":
                    return await Unblock(selectedUsers);
                case "Delete":
                    return await Delete(selectedUsers);
                default:
                    break;
            }
            return RedirectToPage("Account/Users");
        }

        [HttpPost]
        public async Task<IActionResult> Block(string[] selectedUsers)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (selectedUsers != null)
            {
                foreach (var uid in selectedUsers)
                {
                    var user = await userManager.FindByIdAsync(uid);
                    if (user != null)
                    {
                        user.Status = UserStatus.Blocked;
                        await userManager.SetLockoutEnabledAsync(user, true);
                        await userManager.UpdateAsync(user);
                        if (user == currentUser) 
                        {
                            return RedirectToPage("/Account/Login");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Not Found");
                    }
                }
            }
            return RedirectToPage("Account/Users");
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(string[] selectedUsers)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (selectedUsers != null)
            {
                foreach (var uid in selectedUsers)
                {
                    var user = await userManager.FindByIdAsync(uid);
                    if (user != null)
                    {
                        user.Status = UserStatus.Unblocked;
                        await userManager.SetLockoutEnabledAsync(user, false);
                        await userManager.UpdateAsync(user);
                        if (user == currentUser) 
                        {
                            return RedirectToPage("/Account/Login");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Not Found");
                    }
                }
            }
            return RedirectToPage("Account/Users");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string[] selectedUsers)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (selectedUsers != null)
            {
                foreach (var uid in selectedUsers)
                {
                    var user = await userManager.FindByIdAsync(uid);
                    if (user != null)
                    {
                        await userManager.DeleteAsync(user);
                        if (currentUser == user)
                        {
                            return RedirectToPage("/Account/Login");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Not Found");
                    }
                }
            }
            return RedirectToPage("Account/Users");
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
    }
}