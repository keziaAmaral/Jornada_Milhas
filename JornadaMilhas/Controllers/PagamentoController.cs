using Jornada_Milhas.Models;
using Jornada_Milhas.Services.Pagamentos;
using Microsoft.AspNetCore.Mvc;

namespace Jornada_Milhas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagamentoController : ControllerBase
    {
        private IPagamentoService _pagamentoService;

        public PagamentoController(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        [HttpPost]
        public async Task<IActionResult> EfetuarPagamento([FromBody] Pagamento pagamento)
        {
            await _pagamentoService.EfetuarPagamento(pagamento);
            return Ok();
        }
    }
}