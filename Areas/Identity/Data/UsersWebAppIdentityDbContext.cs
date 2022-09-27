using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsersWebApp.Areas.Identity.Data;

namespace UsersWebApp.Areas.Identity.Data;

public class UsersWebAppIdentityDbContext : IdentityDbContext<User>
{

    public UsersWebAppIdentityDbContext(DbContextOptions<UsersWebAppIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
