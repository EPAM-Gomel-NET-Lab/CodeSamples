using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserApi.DataAccess
{
    public class UserApiDbContext : IdentityDbContext
    {
        public UserApiDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
