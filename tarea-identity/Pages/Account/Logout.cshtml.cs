using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tarea_identity.Identity;

namespace tarea_identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<MyUser> _signInManager;

        public LogoutModel(SignInManager<MyUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}