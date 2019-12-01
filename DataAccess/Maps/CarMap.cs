using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
namespace DataAccess.Maps
{
  public class CarMap : IEntityTypeConfiguration<Car>
  {
    public void Configure(EntityTypeBuilder<Car> builder)
    {
      builder.ToTable("Cars");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Id)
        .HasColumnName("Id")
        .UseIdentityColumn();

      builder.Property(x => x.Title)
        .HasColumnName("Title")        
        .HasMaxLength(100)
        .IsUnicode(false)
        .IsRequired();

      builder.Property(x => x.Purchase)
        .HasColumnName("Purchase")
        .IsRequired();

      builder.Property(x => x.Value)
        .HasColumnName("Value")
        .HasColumnType("decimal(18,2)")  
        .IsRequired();

      builder.Property(x => x.Active)
        .HasColumnName("Active")
        .IsRequired();
    }
  }
}
