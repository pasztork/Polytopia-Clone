using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebServer.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole 
            { 
                Name = Areas.Identity.Constants.Roles.AdminRole, 
                NormalizedName = Areas.Identity.Constants.Roles.AdminRole.ToUpper() 
            });
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole 
            { 
                Name = Areas.Identity.Constants.Roles.UserRole, 
                NormalizedName = Areas.Identity.Constants.Roles.UserRole.ToUpper() 
            });
    }
}