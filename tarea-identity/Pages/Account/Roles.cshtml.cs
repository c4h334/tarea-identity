using tarea_identity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace tarea_identity.Pages.Account;

public class RolesModel : PageModel
{
    private readonly RoleManager<MyRole> _roleManager;
    private readonly UserManager<MyUser> _userManager;

    [BindProperty]
    public RolesDTO Rol { get; set; } = default!;

    public RolesModel(RoleManager<MyRole> roleManager, UserManager<MyUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<ActionResult> OnGet()
    {
        ViewData["roles"] = await _roleManager.Roles.ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ViewData["roles"] = await _roleManager.Roles.ToListAsync();
            return Page();
        }

        if (!string.IsNullOrWhiteSpace(Rol.Name))
        {
            var roleExist = await _roleManager.RoleExistsAsync(Rol.Name);

            if (!roleExist)
            {
                var newRol = new MyRole
                {
                    Name = Rol.Name,
                    FechaAlta = DateTime.Now,
                    Seccion = Rol.Seccion
                };

                var res = await _roleManager.CreateAsync(newRol);

                if (res.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync("info@maurobernal.com.ar");
                    if (user != null)
                    {
                        await _userManager.AddToRoleAsync(user, Rol.Name);
                    }
                }
            }
        }

        return RedirectToPage("/Account/Roles");
    }
}