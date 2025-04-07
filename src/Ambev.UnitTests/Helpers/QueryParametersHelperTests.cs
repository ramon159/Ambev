using Ambev.Shared.Helpers;
using Ambev.Shared.Models;
using Ambev.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Ambev.UnitTests.Helpers
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
    }

    public class Endereco
    {
        public Cidade Cidade { get; set; }
    }

    public class Cidade
    {
        public string Nome { get; set; }
    }
    public class QueryParametersHelperTests
    {
        [Fact]
        public void SortingParser_QuotedInputWithTwoFields_ReturnsCorrectSortFields()
        {
            // Arrange
            string order = "\"price desc, title asc\"";
            var expectedResult = new List<SortField>
            {
                new() { Field = "price", Ascending = false },
                new() { Field = "title", Ascending = true }
            };

            // Act
            var result = QueryParametersHelper.SortingParser(order);

            // Assert

            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void SortingParser_InputWithMissingSortDirection_ReturnsDefaultSortFields()
        {
            // Arrange
            string order = "\"price desc, Endereco.Cidade.Nome\"";
            var expectedResult = new List<SortField>
            {
                new() { Field = "price", Ascending = false },
                new() { Field = "endereco.cidade.nome", Ascending = true }
            };

            // Act
            var result = QueryParametersHelper.SortingParser(order);

            // Assert
            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void OrderByNested_WithNestedProperty_SortsAscending()
        {
            // Arrange
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Nome = "A", Endereco = new Endereco { Cidade = new Cidade { Nome = "C" } } },
                new Pessoa { Nome = "B", Endereco = new Endereco { Cidade = new Cidade { Nome = "B" } } },
                new Pessoa { Nome = "A", Endereco = new Endereco { Cidade = new Cidade { Nome = "Z" } } },
                new Pessoa { Nome = "C", Endereco = new Endereco { Cidade = new Cidade { Nome = "A" } } },

            };

            var propertyName = "endereco.cidade.nome";
            var isAscending = true;


            var ordered = pessoas.AsQueryable().OrderByNested(propertyName, isAscending).ToList();

            // Assert
            Assert.Equal("A", ordered[0].Endereco.Cidade.Nome); 

        }
        [Fact]
        public void OrderByNested_WithNestedProperty_SortsDescending()
        {
            // Arrange
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Nome = "A", Endereco = new Endereco { Cidade = new Cidade { Nome = "C" } } },
                new Pessoa { Nome = "B", Endereco = new Endereco { Cidade = new Cidade { Nome = "B" } } },
                new Pessoa { Nome = "A", Endereco = new Endereco { Cidade = new Cidade { Nome = "Z" } } },
                new Pessoa { Nome = "C", Endereco = new Endereco { Cidade = new Cidade { Nome = "A" } } },

            };

            var propertyName = "endereco.cidade.nome";
            var isAscending = false;


            var ordered = pessoas.AsQueryable().OrderByNested(propertyName, isAscending).ToList();

            // Assert
            Assert.Equal("Z", ordered[0].Endereco.Cidade.Nome);

        }

    }
}
