using Jornada_Milhas.Data;
using Jornada_Milhas.Services;
using Jornada_Milhas.Services.Pagamentos;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddContext();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.ConfigureServices();

        BuildApp(builder);
    }

    private static void BuildApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        app.UseRouting();

        app.UseCors(options =>
        options.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod());

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


}