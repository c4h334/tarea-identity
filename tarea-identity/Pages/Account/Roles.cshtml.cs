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
    // CORRECCIÓN: Se le dice al compilador que asumiremos que el valor se inicializará
    public RolesDTO Rol { get; set; } = default!;

    public RolesModel(
        RoleManager<MyRole> roleManager,
        UserManager<MyUser> userManager
        )
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<ActionResult> OnGet()
    {
        var MyRoles = await _roleManager.Roles.ToListAsync();
        ViewData["roles"] = MyRoles;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var newRol = new MyRole();
        newRol.Name = Rol.Name;
        newRol.FechaAlta = DateTime.Now;
        newRol.Seccion = Rol.Seccion;

        var res = await _roleManager.CreateAsync(newRol);

        // Validación para asegurar que el usuario existe antes de asignarle el rol
        var user = await _userManager.FindByEmailAsync("info@maurobernal.com.ar");

        if (user != null)
        {
            var rolassign = await _userManager.AddToRoleAsync(user, Rol.Name);
        }

        return RedirectPermanent("/account/roles");
    }
}