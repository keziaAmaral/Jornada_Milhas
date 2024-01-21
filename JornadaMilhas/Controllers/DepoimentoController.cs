using Jornada_Milhas.Models;
using Jornada_Milhas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jornada_Milhas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepoimentoController : ControllerBase
    {
        private readonly IDepoimentoService _depoimentoService;

        public DepoimentoController(IDepoimentoService depoimentoService)
        {
            _depoimentoService = depoimentoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Depoimento depoimento)
        {
            await _depoimentoService.AddAsync(depoimento);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var depoimentos = await _depoimentoService.GetListAsync(skip, take);
            return Ok(depoimentos);
        }

        [Route("Home")]
        [HttpGet]
        public async Task<IActionResult> GetRandomAsync()
        {
            var depoimentosRandom = await _depoimentoService.GetRandomAsync();

            return Ok(depoimentosRandom);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _depoimentoService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Depoimento depoimento)
        {
            await _depoimentoService.UpdateAsync(depoimento);
            return Ok();
        }
    }
}

