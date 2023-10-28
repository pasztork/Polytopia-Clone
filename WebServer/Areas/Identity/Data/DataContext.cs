using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebServer.Areas.Identity.Data;

public class DataContext : IdentityDbContext<User>
{
	public DataContext(DbContextOptions<DataContext> options) : base(options) 
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
	}
}