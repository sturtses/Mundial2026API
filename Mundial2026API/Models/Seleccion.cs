namespace Mundial2026API.Models;

public class Seleccion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Grupo { get; set; } = string.Empty;

    public int Puntos { get; set; }

    public int PartidosJugados { get; set; }

    public bool Favorita { get; set; }
}