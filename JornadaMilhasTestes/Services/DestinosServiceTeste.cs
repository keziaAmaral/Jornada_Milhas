using Jornada_Milhas.Data;
using Jornada_Milhas.Models;
using Jornada_Milhas.Services;
using JornadaMIlhasTestes.Helper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace JornadaMIlhasTestes.Services
{
    public class DestinosServiceTeste
    {
        private readonly DestinoService _subject;
        private readonly Mock<IDestinoContext> _destinoContext;
        private readonly Mock<DbSet<Destino>> _mockDbSet;
        public DestinosServiceTeste()
        {
            _destinoContext = new Mock<IDestinoContext>();
            _subject = new DestinoService(_destinoContext.Object);
            _mockDbSet = new Mock<DbSet<Destino>>();
        }

        [Fact]
        public async Task Should_Add_Destino()
        {
            var destino = new Destino
            {
                Id = 1,
                Nome = "Destino de viagem",
                Preco = 1567M
            };

            var destinoList = new List<Destino> { destino }.AsQueryable();

            SetupDbSet(_mockDbSet, destinoList);

            _destinoContext
                .SetupGet(x => x.Destino)
                .Returns(_mockDbSet.Object);

            await _subject.AddAsync(destino);

            Assert.Equal(destino, _destinoContext.Object.Destino.FirstOrDefault());
            _destinoContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_Destino()
        {
            var destino = new Destino
            {
                Id = 1,
                Nome = "Destino de viagem",
                Preco = 1567M
            };

            var depoimentoList = new List<Destino> { destino }.AsQueryable();

            SetupDbSet(_mockDbSet, depoimentoList);

            _destinoContext
                .SetupGet(x => x.Destino)
                .Returns(_mockDbSet.Object);

            await _subject.DeleteAsync(destino.Id);

            Assert.True(destino.Deleted);
            Assert.NotNull(destino.DeletedDate);
            _destinoContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetList_Should_Return_Destino_List()
        {
            var destino = new Destino { Id = 5 };
            var destino2 = new Destino { Id = 8 };
            var destino3 = new Destino { Id = 7 };

            var destinoList = new List<Destino> { destino, destino2, destino3 }.AsQueryable();

            SetupDbSet(_mockDbSet, destinoList);

            _destinoContext
                .SetupGet(x => x.Destino)
                .Returns(_mockDbSet.Object);

            var result = await _subject.GetListAsync(0, 2);

            Assert.True(result.Where(x => x.Id == 5).Any());
            Assert.True(result.Where(x => x.Id == 8).Any());
        }

        [Fact]
        public async Task GetByName_Should_Return_Destino_List()
        {
            var destino = new Destino { Id = 5, Nome = "Sao Paulo" };
            var destino2 = new Destino { Id = 8, Nome = "Minas Gerais" };

            var name = "Minas";

            var destinoList = new List<Destino> { destino, destino2 }.AsQueryable();

            SetupDbSet(_mockDbSet, destinoList);

            _destinoContext
                .SetupGet(x => x.Destino)
                .Returns(_mockDbSet.Object);

            var result = await _subject.GetByNameAsync(name);

            Assert.True(result.Where(x => x.Id == 8).Any());
            Assert.False(result.Where(x => x.Id == 5).Any());
        }

        [Fact]
        public async Task Update_Should_Return_Atualizar_Destino()
        {
            var destino = new Destino
            {
                Id = 1,
                Nome = "Destino de viagem",
                Preco = 1567M
            };

            var destinoAtualizacao = new Destino
            {
                Id = 1,
                Nome = "Destino de viagem atualizado",
                Preco = 999M
            };

            var destinoList = new List<Destino> { destinoAtualizacao }.AsQueryable();


            SetupDbSet(_mockDbSet, destinoList);

            _destinoContext
                .SetupGet(x => x.Destino)
                .Returns(_mockDbSet.Object);

            await _subject.AddAsync(destino);
            await _subject.UpdateAsync(destinoAtualizacao);

            Assert.Equal(destinoAtualizacao, _destinoContext.Object.Destino.FirstOrDefault());
        }


        private void SetupDbSet(Mock<DbSet<Destino>> destinoMock, IQueryable<Destino> expectedDestino)
        {
            destinoMock
                .As<IAsyncEnumerable<Destino>>()
                .Setup(ca => ca.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestDbAsyncEnumerator<Destino>(expectedDestino.GetEnumerator()));

            destinoMock
                .As<IQueryable<Destino>>()
                .Setup(ca => ca.Provider)
                .Returns(new TestDbAsyncQueryProvider<Destino>(expectedDestino.Provider));

            destinoMock
                .As<IQueryable<Destino>>()
                .Setup(ca => ca.Expression)
                .Returns(expectedDestino.Expression);

            destinoMock
                .As<IQueryable<Destino>>()
                .Setup(ca => ca.ElementType)
                .Returns(expectedDestino.ElementType);

            destinoMock
                .As<IQueryable<Destino>>()
                .Setup(ca => ca.GetEnumerator())
                .Returns(expectedDestino.GetEnumerator());

            _destinoContext
                .SetupGet(c => c.Destino)
                .Returns(destinoMock.Object);
        }
    }
}

