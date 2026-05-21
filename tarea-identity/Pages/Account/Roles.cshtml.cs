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
    public RolesDTO Rol { get; set; }
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
        var MyRolees = await _roleManager.Roles.ToListAsync();
        ViewData["roles"] = MyRolees;
        return Page();
    } 
    public async Task<IActionResult> OnPostAsync()
    {
        var newRol = new MyRole();
        newRol.Name = Rol.Name;
        newRol.FechaAlta = DateTime.Now;
        newRol.Seccion = Rol.Seccion;
        
        var res= await _roleManager.CreateAsync(newRol);

        //Assign user in rol
        var user = await _userManager.FindByEmailAsync("info@maurobernal.com.ar");
        var rolassign= await _userManager.AddToRoleAsync(user, Rol.Name);

        return RedirectPermanent("/account/roles");
    }
}
