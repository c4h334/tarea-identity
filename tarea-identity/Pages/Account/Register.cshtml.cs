using tarea_identity.Identity.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tarea_identity.Identity;
using tarea_identity.Pages.Account;

namespace tarea_identity.Identity.Account;

[AllowAnonymous]
public class RegisterModel : PageModel
{
    private readonly UserManager<MyUser> _userManager;

    [BindProperty]
    public RegisterDTO Register { get; set; }
    public string ReturnUrl { get; set; }

    public RegisterModel(UserManager<MyUser> userManager)
    =>
        _userManager = userManager;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Register.Password != Register.Password2)
        {
            ModelState.AddModelError(string.Empty, "Las contraseñas no coinciden.");
            return Page();
        }

        var user = new MyUser();
        user.NroLegajo = Register.Legajo;
        user.Email = Register.Email;
        user.UserName = Register.Email;

        var res = await _userManager.CreateAsync(user, Register.Password);

        if (res.Succeeded)
        {
            if (ReturnUrl == null)
            {
                ReturnUrl = "/";
            }

            return LocalRedirect(ReturnUrl);
        }

        foreach (var error in res.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}