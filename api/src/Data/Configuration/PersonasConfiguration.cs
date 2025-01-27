using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using api.src.Entities;

namespace api.src.Data.Configurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.HasKey(e => e.Identificador)
                .HasName("PK__Personas__F2374EB1B96B7146");

            builder.Property(e => e.Apellidos).HasMaxLength(50);
            builder.Property(e => e.Email).HasMaxLength(100);
            builder.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.NombreCompleto)
                .HasMaxLength(101)
                .HasComputedColumnSql("(([Nombres]+' ')+[Apellidos])", false);
            builder.Property(e => e.Nombres).HasMaxLength(50);
            builder.Property(e => e.NumeroIdentificacion).HasMaxLength(20);
            builder.Property(e => e.NumeroIdentificacionCompleto)
                .HasMaxLength(41)
                .HasComputedColumnSql("(([TipoIdentificacion]+'-')+[NumeroIdentificacion])", false);
            builder.Property(e => e.TipoIdentificacion).HasMaxLength(20);
        }
    }
}
