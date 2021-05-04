using System;
using System.Data;
using FluentAssertions;
using Xunit;

namespace BattleshipGame.Core.UnitTests
{
    public class BattleshipControllerTests : IClassFixture<BattleshipFixture>
    {
        private readonly BattleshipFixture _fixture;

        public BattleshipControllerTests(BattleshipFixture fixture)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }
        
        [Theory]
        [InlineData("A1", ExecutionResultType.Hit)]        
        [InlineData("a1", ExecutionResultType.Hit)]
        [InlineData("A2", ExecutionResultType.Hit)]
        [InlineData("A3", ExecutionResultType.Hit)]
        [InlineData("A4", ExecutionResultType.Hit)]        
        [InlineData("B1", ExecutionResultType.Miss)]
        [InlineData("B2", ExecutionResultType.Miss)]
        [InlineData("B3", ExecutionResultType.Miss)]
        [InlineData("B4", ExecutionResultType.Miss)]
        [InlineData("C4", ExecutionResultType.Error)]
        [InlineData("A0", ExecutionResultType.Error)]
        [InlineData("A5", ExecutionResultType.Error)]
        [InlineData("C5", ExecutionResultType.Error)]
        public void Should_ExecuteCommand_ReturnsHitOrMiss_DependsOnGivenCoordinates(string command, ExecutionResultType expected)
        {
            // Arrange
            var sut = new BattleshipController(_fixture.BattleshipPlan());

            // Act
            var result = sut.ExecuteCommand(command);
            
            // Assert
            result.ResultType.Should().Be(expected);
        }
        
        [Fact]
        public void Should_ExecuteCommand_ReturnsGameOver_WhenAllTheShipsAreDestroyed()
        {
            // Arrange
            var sut = new BattleshipController(_fixture.BattleshipPlan());

            // Act
            var result = sut.ExecuteCommand("A1");
            result = sut.ExecuteCommand("A2");
            result = sut.ExecuteCommand("A3");
            result = sut.ExecuteCommand("A4");
            
            // Assert
            result.ResultType.Should().Be(ExecutionResultType.Hit);
            result.GameOver.Should().BeTrue();
        }
    }
}