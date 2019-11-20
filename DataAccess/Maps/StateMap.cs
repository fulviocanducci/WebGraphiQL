using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
namespace DataAccess.Maps
{
  public class StateMap : IEntityTypeConfiguration<State>
  {
    public void Configure(EntityTypeBuilder<State> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Id)
        .HasColumnName("Id")
        .ValueGeneratedNever();

      builder.Property(x => x.Uf)
        .IsRequired()
        .IsUnicode(false)
        .HasMaxLength(2)
        .HasColumnName("Uf");

      builder.HasMany(x => x.Country)
        .WithOne(x => x.State)
        .HasForeignKey(x => x.StateId);
    }
  }
}
