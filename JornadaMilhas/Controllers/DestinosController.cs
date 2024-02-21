using Jornada_Milhas.Models;
using Jornada_Milhas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jornada_Milhas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DestinosController : ControllerBase
    {
        private readonly IDestinosService _destinosService;

        public DestinosController(IDestinosService destinosService)
        {
            _destinosService = destinosService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Destino destino)
        {
            await _destinosService.AddAsync(destino);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _destinosService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var destinos = await _destinosService.GetListAsync(skip, take);
            return Ok(destinos);
        }

        [HttpGet]
        [Route("nome")]
        public async Task<IActionResult> GetByNameAsync([FromRoute] string name)
        {
            var destino = await _destinosService.GetByNameAsync(name);
            return Ok(destino);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Destino destino)
        {
            await _destinosService.UpdateAsync(destino);
            return Ok();
        }
    }
}
