using Jornada_Milhas.Services.Pagamentos;

namespace Jornada_Milhas.Services
{
    public static class ServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services) 
        {
            services.AddScoped<IDepoimentoService, DepoimentoService>();
            services.AddScoped<IDestinosService, DestinoService>();
            services.AddScoped<IPagamentoService, PagamentoService>();
        }
    }
}
