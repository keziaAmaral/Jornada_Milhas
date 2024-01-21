using Azure;
using Jornada_Milhas.Controllers;
using Jornada_Milhas.Models;
using Jornada_Milhas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JornadaMIlhasTestes.Controllers
{
    public class DepoimentoControllerTeste
    {
        private readonly DepoimentoController _subject;
        private readonly Mock<IDepoimentoService> _depoimentoService;

        private Depoimento _depoimento = new Depoimento
        {
            Id = 1,
            Comentario = "Esse é um novo depoimento",
            Foto = "Nova foto",
            NomeUsuario = "Nome do primeiro usuário",
            Deleted = false
        };

        public DepoimentoControllerTeste()
        {
            _depoimentoService = new Mock<IDepoimentoService>();
            _subject = new DepoimentoController(_depoimentoService.Object);
        }

        [Fact]
        public async Task Should_Add_Create_New_Depoimento()
        {
            var result = await _subject.AddAsync(_depoimento) as OkResult; ;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }

        [Fact]
        public async Task Should_GetList_Returns_All_Depoimentos()
        {
            var depoimentoList = new List<Depoimento> { _depoimento };

            var skip = 0;
            var take = 10;

            _depoimentoService
                .Setup(x => x.GetListAsync(skip, take))
                .ReturnsAsync(depoimentoList);

            var result = await _subject.GetListAsync(skip, take) as OkObjectResult;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }

        [Fact]
        public async Task Should_GetRandom_Returns_Random_Depoimentos()
        {
            var depoimentoList = new List<Depoimento> { _depoimento };

            _depoimentoService
                .Setup(x => x.GetRandomAsync())
                .ReturnsAsync(depoimentoList);

            var result = await _subject.GetRandomAsync() as OkObjectResult;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }

        [Fact]
        public async Task Should_Delete_Depoimento()
        {
            var result = await _subject.DeleteAsync(_depoimento.Id) as OkResult;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }

        [Fact]
        public async Task Should_Update_Depoimento()
        {
            var result = await _subject.UpdateAsync(_depoimento) as OkResult;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }
    }
}