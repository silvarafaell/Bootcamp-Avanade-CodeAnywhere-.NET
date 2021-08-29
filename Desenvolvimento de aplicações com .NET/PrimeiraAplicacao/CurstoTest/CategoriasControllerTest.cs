using CursoApi.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using PrimeiraAplicacao.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CurstoTest
{
    public class CategoriasControllerTest
    {
        private readonly Mock<DbSet<Categoria>> _mockSet;
        private readonly Mock<Context> _mockContext;
        private readonly Categoria _categoria;
        public CategoriasControllerTest()
        {
            _mockSet = new Mock<DbSet<Categoria>>();
            _mockContext = new Mock<Context>();
            _categoria = new Categoria { Id = 1, Descricao = "Teste Categoria" };

            _mockContext.Setup(expression: m => m.Categorias).Returns(_mockSet.Object);

            _mockContext.Setup(expression: m => m.Categorias.FindAsync(1))
                .ReturnsAsync(_categoria);
        }

        [Fact]
        public async Task Get_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);

            var testCategoria = await service.GetCategoria(id: 1);

            _mockSet.Verify(expression: m => m.FindAsync(1),
            Times.Once());

            Assert.Equal(expected: _categoria, actual: testCategoria);
        }

        [Fact]
        public async Task Put_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);
            await service.PostCategoria(_categoria);

        }

        [Fact]
        public async Task Post_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);
            await service.PostCategoria(_categoria);

            _mockSet.Verify(expression: x => x.Add(_categoria), Times.Once);
           
        }
    }
}
