using Jornada_Milhas.Data;
using Jornada_Milhas.Models;
using Jornada_Milhas.Services;
using JornadaMIlhasTestes.Helper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace JornadaMIlhasTestes.Services
{
    public class DepoimentoServiceTeste
    {
        private readonly DepoimentoService _subject;
        private readonly Mock<IDepoimentoContext> _depoimentoContext;
        private readonly Mock<DbSet<Depoimento>> _mockDbSet;
        public DepoimentoServiceTeste()
        {
            _depoimentoContext = new Mock<IDepoimentoContext>();
            _subject = new DepoimentoService(_depoimentoContext.Object);
            _mockDbSet = new Mock<DbSet<Depoimento>>();
        }

        [Fact]
        public async Task Should_Add_Depoimento()
        {
            var depoimento = new Depoimento
            {
                Id = 5,
                Comentario = "Novo comentario",
                Deleted = false,
                DeletedDate = null,
                Foto = "Nova foto",
                NomeUsuario = "UserName"
            };

            var depoimentoList = new List<Depoimento> { depoimento }.AsQueryable();

            SetupDbSet(_mockDbSet, depoimentoList);

            _depoimentoContext
                .SetupGet(x => x.Depoimento)
                .Returns(_mockDbSet.Object);

            await _subject.AddAsync(depoimento);

            Assert.Equal(depoimento, _depoimentoContext.Object.Depoimento.FirstOrDefault());
            _depoimentoContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_Depoimento()
        {
            var depoimento = new Depoimento
            {
                Id = 5,
                Comentario = "Novo comentario",
                Deleted = false,
                DeletedDate = null,
                Foto = "Nova foto",
                NomeUsuario = "UserName"
            };

            var depoimentoList = new List<Depoimento> { depoimento }.AsQueryable();

            SetupDbSet(_mockDbSet, depoimentoList);

            _depoimentoContext
                .SetupGet(x => x.Depoimento)
                .Returns(_mockDbSet.Object);

            await _subject.DeleteAsync(depoimento.Id);

            Assert.True(depoimento.Deleted);
            Assert.NotNull(depoimento.DeletedDate);
            _depoimentoContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetList_Should_Return_Depoimento_List()
        {
            var depoimento = new Depoimento { Id = 5 };
            var depoimento2 = new Depoimento { Id = 8 };
            var depoimento3 = new Depoimento { Id = 7 };

            var depoimentoList = new List<Depoimento> { depoimento, depoimento2, depoimento3 }.AsQueryable();

            SetupDbSet(_mockDbSet, depoimentoList);

            _depoimentoContext
                .SetupGet(x => x.Depoimento)
                .Returns(_mockDbSet.Object);

            var result = await _subject.GetListAsync(0, 2);

            Assert.True(result.Where(x => x.Id == 5).Any());
            Assert.True(result.Where(x => x.Id == 8).Any());
        }


        [Fact]
        public async Task GetRandom_Should_Return_Random_Depoimento_List()
        {
            var depoimento = new Depoimento { Id = 5 };
            var depoimento2 = new Depoimento { Id = 8 };
            var depoimento3 = new Depoimento { Id = 7 };
            var depoimento4 = new Depoimento { Id = 1 };
            var depoimento5 = new Depoimento { Id = 4 };

            var depoimentoList = new List<Depoimento> { depoimento, depoimento2, depoimento3, depoimento4, depoimento5 }.AsQueryable();

            SetupDbSet(_mockDbSet, depoimentoList);

            _depoimentoContext
                .SetupGet(x => x.Depoimento)
                .Returns(_mockDbSet.Object);

            var result = await _subject.GetRandomAsync();

            Assert.True(result.Count() == 3);
        }

        [Fact]
        public async Task Update_Should_Return_Atualizar_Depoimento()
        {
            var depoimento = new Depoimento
            {
                Id = 5,
                Comentario = "Novo comentario",
                Deleted = false,
                DeletedDate = null,
                Foto = "Nova foto",
                NomeUsuario = "UserName"
            };

            var depoimentoAtualizacao = new Depoimento
            {
                Id = 5,
                Comentario = "Comentario Editado",
                Deleted = false,
                DeletedDate = null,
                Foto = "Foto Editada",
                NomeUsuario = "UserName"
            };

            var depoimentoList = new List<Depoimento> { depoimentoAtualizacao }.AsQueryable();


            SetupDbSet(_mockDbSet, depoimentoList);

            _depoimentoContext
                .SetupGet(x => x.Depoimento)
                .Returns(_mockDbSet.Object);

            await _subject.AddAsync(depoimento);
            await _subject.UpdateAsync(depoimentoAtualizacao);

            Assert.Equal(depoimentoAtualizacao, _depoimentoContext.Object.Depoimento.FirstOrDefault());
        }


        private void SetupDbSet(Mock<DbSet<Depoimento>> depoimentoMock, IQueryable<Depoimento> expectedDepoimento)
        {
            depoimentoMock
                .As<IAsyncEnumerable<Depoimento>>()
                .Setup(ca => ca.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestDbAsyncEnumerator<Depoimento>(expectedDepoimento.GetEnumerator()));

            depoimentoMock
                .As<IQueryable<Depoimento>>()
                .Setup(ca => ca.Provider)
                .Returns(new TestDbAsyncQueryProvider<Depoimento>(expectedDepoimento.Provider));

            depoimentoMock
                .As<IQueryable<Depoimento>>()
                .Setup(ca => ca.Expression)
                .Returns(expectedDepoimento.Expression);

            depoimentoMock
                .As<IQueryable<Depoimento>>()
                .Setup(ca => ca.ElementType)
                .Returns(expectedDepoimento.ElementType);

            depoimentoMock
                .As<IQueryable<Depoimento>>()
                .Setup(ca => ca.GetEnumerator())
                .Returns(expectedDepoimento.GetEnumerator());

            _depoimentoContext
                .SetupGet(c => c.Depoimento)
                .Returns(depoimentoMock.Object);
        }
    }
}




