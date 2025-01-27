using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using api.src.Entities;

namespace api.src.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.HasKey(e => e.Identificador)
                .HasName("PK__Usuarios__F2374EB1BB993407");

            builder.Property(e => e.Persona)
                .HasColumnType("int");
               
            builder.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.Pass).HasMaxLength(25);
            builder.Property(e => e.Usuario)
                .HasMaxLength(50)
                .HasColumnName("Usuario");
        }
    }
}
