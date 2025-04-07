using Ambev.Infrastructure.Extensions;

namespace Ambev.UnitTests.Extensions
{
    public class IQueryableExtensionsTests
    {
        public class Produto
        {
            public string? Categoria { get; set; }
            public decimal? Preco { get; set; }
            public string? Titulo { get; set; }
        }

        [Fact]
        public void Filtering_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var produtos = new List<Produto>
        {
            new Produto { Categoria = "Eletrônicos", Preco = 1000, Titulo = "TV" },
            new Produto { Categoria = "Roupas", Preco = 50, Titulo = "Camiseta" },
            new Produto { Categoria = "Eletrônicos", Preco = 500, Titulo = "Rádio" }
        }.AsQueryable();

            var filtros = new Dictionary<string, string>
        {
            { "Categoria", "Eletrônicos" }
        };

            // Act
            var resultado = produtos.Filtering(filtros).ToList();

            // Assert
            Assert.Equal(2, resultado.Count);
            Assert.All(resultado, p => Assert.Equal("Eletrônicos", p.Categoria));
        }
    }
}
