using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
   public partial class QLContext : DbContext
   {
      public QLContext()
      {
      }

      public QLContext(DbContextOptions<QLContext> options)
          : base(options)
      {
      }

      public virtual DbSet<Country> Country { get; set; }
      public virtual DbSet<State> State { get; set; }
      public virtual DbSet<Car> Car { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
         }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

         modelBuilder.Entity<Country>(entity =>
         {
            entity.HasIndex(e => e.StateId);

            entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(150)
                   .IsUnicode(false);

            entity.HasOne(d => d.State)
                   .WithMany(p => p.Country)
                   .HasForeignKey(d => d.StateId);
         });

         modelBuilder.Entity<State>(entity =>
         {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Uf)
                   .IsRequired()
                   .HasMaxLength(2)
                   .IsUnicode(false);
         });

         modelBuilder.Entity<Car>(entity =>
         {
            entity.ToTable("Car");
            entity.Property(e => e.Title)
                   .IsRequired()                   
                   .IsUnicode(false);
            entity.Property(x => x.Value).HasColumnType("decimal(18,2)");
         });

         OnModelCreatingPartial(modelBuilder);
      }

      partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
   }
}