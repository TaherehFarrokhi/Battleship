using System;
using System.Xml;
using FluentAssertions;
using Xunit;

namespace BattleshipGame.Core.UnitTests
{
    public class CellNameBuilderTests
    {
        [Theory]
        [InlineData(1, 1, "B2")]
        [InlineData(10, 10, "K11")]
        [InlineData(0, 0, "A1")]
        [InlineData(0, 9, "A10")]
        public void Should_ToName_ReturnCorrectNameForGivenIndexes(int column, int row, string expectedName)
        {
            // Arrange
            // Act
            var actual = CellNameBuilder.ToName(column, row);
            
            // Assert
            actual.Should().Be(expectedName);
        }
        
        [Theory]
        [InlineData("B2", 1, 1)]
        [InlineData("K11", 10, 10)]
        [InlineData("A1", 0, 0)]
        [InlineData("A10", 0, 9)]
        [InlineData("a10", 0, 9)]
        [InlineData("aa", 0, -1)]
        public void Should_FromName_ReturnCorrectIndexesGivenName(string name, int expectedColumn, int expectedRow)
        {
            // Arrange
            // Act
            var (column, row) = CellNameBuilder.FromName(name);
            
            // Assert
            column.Should().Be(expectedColumn);
            row.Should().Be(expectedRow);
        }
    }
}