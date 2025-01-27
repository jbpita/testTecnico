using Microsoft.EntityFrameworkCore;
using api.src.Entities;

namespace api.src.Data;

public partial class SqlServerContext : DbContext
{
    
    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }
    public SqlServerContext()
    {
    }

    public SqlServerContext(DbContextOptions<SqlServerContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=sqlserver,1433;Database=TestDB;User Id=sa;Password=YourStrongPass@2023!;Encrypt=False;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerContext).Assembly);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
