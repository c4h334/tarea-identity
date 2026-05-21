using Microsoft.AspNetCore.Identity;

namespace tarea_identity.Identity
{
    public class MyRole : IdentityRole
    {
        public string Seccion { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
