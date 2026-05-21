using Microsoft.AspNetCore.Identity;

namespace tarea_identity.Identity
{
    public class MyUser : IdentityUser
    {
        public int NroLegajo { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
