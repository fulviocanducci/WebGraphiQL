using DataAccess.Maps;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
  public sealed class DatabaseAccess: DbContext
  {
    public DatabaseAccess(DbContextOptions<DatabaseAccess> options)
      :base(options)
    {
    }
    public DbSet<People> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new PeopleMap());
    }
  }
}
