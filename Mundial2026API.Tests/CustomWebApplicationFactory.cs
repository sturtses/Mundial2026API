using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Mundial2026API.Data;
using Mundial2026API.Models;

namespace Mundial2026API.Tests;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var provider = services.BuildServiceProvider();

            using var scope = provider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<MundialContext>();

            db.Database.EnsureCreated();

            SeedData(db);
        });
    }

    private static void SeedData(MundialContext db)
    {
        if (db.Selecciones.Any())
            return;

        db.Selecciones.AddRange(
            new Seleccion
            {
                Nombre = "España",
                Grupo = "A",
                Puntos = 6,
                PartidosJugados = 2,
                Favorita = true
            },
            new Seleccion
            {
                Nombre = "Brasil",
                Grupo = "B",
                Puntos = 4,
                PartidosJugados = 2,
                Favorita = true
            },
            new Seleccion
            {
                Nombre = "Argentina",
                Grupo = "C",
                Puntos = 3,
                PartidosJugados = 2,
                Favorita = true
            },
            new Seleccion
            {
                Nombre = "Japón",
                Grupo = "D",
                Puntos = 1,
                PartidosJugados = 2,
                Favorita = false
            },
            new Seleccion
            {
                Nombre = "Canadá",
                Grupo = "A",
                Puntos = 0,
                PartidosJugados = 2,
                Favorita = false
            });

        db.SaveChanges();
    }
}