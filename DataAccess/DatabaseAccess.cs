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
    public DbSet<State> State { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<Car> Car { get; set; }
    public DbSet<Items> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new PeopleMap());
      modelBuilder.ApplyConfiguration(new StateMap());
      modelBuilder.ApplyConfiguration(new CountryMap());
      modelBuilder.ApplyConfiguration(new ItemsMap());
    }
  }
}
