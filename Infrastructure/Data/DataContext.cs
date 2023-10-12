using Domain;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure;
public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> opt):base(opt)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
