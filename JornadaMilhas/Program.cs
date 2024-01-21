using Jornada_Milhas.Data;
using Jornada_Milhas.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureDatabase(builder);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        ConfigureServices(builder.Services);

        BuildApp(builder);
    }

    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddDbContext<DepoimentoContext>(
            options => options.UseSqlServer(connectionString));

        builder.Services.AddDbContext<DestinoContext>(
            options => options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IDepoimentoContext, DepoimentoContext>();
        builder.Services.AddScoped<IDestinoContext, DestinoContext>();
    }

    private static void BuildApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IDepoimentoService, DepoimentoService>();
        services.AddScoped<IDestinosService, DestinoService>();
    }
}