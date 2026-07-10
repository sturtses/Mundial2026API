using FluentAssertions;
using Mundial2026API.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;

namespace Mundial2026API.Tests;

public class SeleccionControllerTests
{
    private readonly HttpClient _client;

    public SeleccionControllerTests()
    {
        var factory = new CustomWebApplicationFactory();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ObtenerTodasLasSelecciones_DeberiaDevolverCinco()
    {
        // Act
        var selecciones =
            await _client.GetFromJsonAsync<List<Seleccion>>("/api/selecciones");

        // Assert
        selecciones.Should().NotBeNull();
        selecciones.Should().HaveCount(5);
    }

    [Fact]
    public async Task ObtenerSeleccionExistente_DeberiaDevolverLaSeleccion()
    {
        // Act
        var seleccion =
            await _client.GetFromJsonAsync<Seleccion>("/api/selecciones/1");

        // Assert
        seleccion.Should().NotBeNull();
        seleccion!.Nombre.Should().Be("España");
    }

    [Fact]
    public async Task CrearSeleccion_DeberiaInsertarla()
    {
        // Arrange
        var nueva = new Seleccion
        {
            Nombre = "Francia",
            Grupo = "E",
            Puntos = 0,
            PartidosJugados = 0,
            Favorita = true
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/selecciones",
            nueva);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var selecciones =
            await _client.GetFromJsonAsync<List<Seleccion>>(
                "/api/selecciones");

        selecciones.Should().HaveCount(6);
    }

    [Fact]
    public async Task ModificarSeleccion_DeberiaActualizarLosDatos()
    {
        // Arrange
        var seleccion =
            await _client.GetFromJsonAsync<Seleccion>(
                "/api/selecciones/1");

        seleccion!.Puntos = 9;

        // Act
        var response = await _client.PutAsJsonAsync(
            "/api/selecciones/1",
            seleccion);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var actualizada =
            await _client.GetFromJsonAsync<Seleccion>(
                "/api/selecciones/1");

        actualizada!.Puntos.Should().Be(9);
    }

    [Fact]
    public async Task EliminarSeleccion_DeberiaEliminarla()
    {
        // Act
        var response =
            await _client.DeleteAsync("/api/selecciones/5");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var selecciones =
            await _client.GetFromJsonAsync<List<Seleccion>>(
                "/api/selecciones");

        selecciones.Should().HaveCount(4);
    }
}