using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace tarea_identity.Identity
{
    public class MyIdentityDBContext : IdentityDbContext<MyUser, MyRole, string>
    {
        public MyIdentityDBContext(DbContextOptions<MyIdentityDBContext> options) : base(options)
        {

        }
    }
}
