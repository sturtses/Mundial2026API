using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Mundial2026API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

if (builder.Environment.IsEnvironment("Testing"))
{
    var connection = new SqliteConnection("DataSource=:memory:");
    connection.Open();

    builder.Services.AddSingleton(connection);

    builder.Services.AddDbContext<MundialContext>((sp, options) =>
    {
        options.UseSqlite(sp.GetRequiredService<SqliteConnection>());
    });
}
else
{
    builder.Services.AddDbContext<MundialContext>(options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("MundialConnection"));
    });
}

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }