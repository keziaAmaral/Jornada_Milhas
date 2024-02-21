using Jornada_Milhas.Controllers;
using Jornada_Milhas.Models;
using Jornada_Milhas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JornadaMIlhasTestes.Controllers
{
    public class DestinosControllerTeste
    {
        private readonly DestinosController _subject;
        private readonly Mock<IDestinosService> _destinosService;

        private Destino _destino = new Destino
        {
            Id = 1,
            Nome = "Destino de viagem", 
            Preco = 1567M
        };

        public DestinosControllerTeste()
        {
            _destinosService = new Mock<IDestinosService>();
            _subject = new DestinosController(_destinosService.Object);
        }

        [Fact]
        public async Task Should_Add_Create_New_Destino()
        {
            var result = await _subject.AddAsync(_destino) as OkResult; ;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }

        [Fact]
        public async Task Should_GetList_Returns_All_Destinos()
        {
            var destinoList = new List<Destino> { _destino };

            var skip = 0;
            var take = 10;

            _destinosService
                .Setup(x => x.GetListAsync(skip, take))
                .ReturnsAsync(destinoList);

            var result = await _subject.GetListAsync(skip, take) as OkObjectResult;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }

        [Fact]
        public async Task Should_GetByName_Returns_Destino_With_Same_Name()
        {
            var name = "SP";
            _destino.Nome = name;

            var destinoList = new List<Destino> { _destino };

            _destinosService
                .Setup(x => x.GetByNameAsync(name))
                .ReturnsAsync(destinoList);

            var result = await _subject.GetByNameAsync(name) as OkObjectResult;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }

        [Fact]
        public async Task Should_Delete_Depoimento()
        {
            var result = await _subject.DeleteAsync(_destino.Id) as OkResult; ;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }

        [Fact]
        public async Task Should_Update_Destino()
        {
            var result = await _subject.UpdateAsync(_destino) as OkResult;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }
    }
}
