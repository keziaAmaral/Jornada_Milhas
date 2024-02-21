using Microsoft.EntityFrameworkCore;

namespace Jornada_Milhas.Data
{
    public static class ContextExtension
    {
        public static void AddContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

            builder.Services.AddDbContext<DepoimentoContext>(
                options => options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<DestinoContext>(
                options => options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<PagamentoContext>(
                options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IDepoimentoContext, DepoimentoContext>();
            builder.Services.AddScoped<IDestinoContext, DestinoContext>();
            builder.Services.AddScoped<IPagamentoContext, PagamentoContext>();
        }
    }
}
