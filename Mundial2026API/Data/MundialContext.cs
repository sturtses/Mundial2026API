using Microsoft.EntityFrameworkCore;
using Mundial2026API.Models;

namespace Mundial2026API.Data;

public class MundialContext : DbContext
{
    public MundialContext(DbContextOptions<MundialContext> options)
        : base(options)
    {
    }

    public DbSet<Seleccion> Selecciones => Set<Seleccion>();
}