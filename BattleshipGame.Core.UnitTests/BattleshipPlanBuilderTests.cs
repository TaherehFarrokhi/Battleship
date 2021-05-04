using FluentAssertions;
using Moq;
using Xunit;

namespace BattleshipGame.Core.UnitTests
{
    public class BattleshipPlanBuilderTests
    {
        [Fact]
        public void Should_Build_ReturnsCorrectPlan_DependsOnGivenInputs()
        {
            // Arrange
            var ship = new Battleship
            {
                ShipType = ShipType.Destroyer, Cells = new[]
                {
                    new BattleshipCell("A1", 0, 0),
                    new BattleshipCell("A2", 0, 1),
                    new BattleshipCell("A3", 0, 2),
                    new BattleshipCell("A4", 0, 3),
                }
            };
            var placementStrategy = new Mock<IBattleshipPlacementStrategy>();
            placementStrategy
                .Setup(m => m.PlaceShip(It.IsAny<BattleshipCell[,]>(), It.IsAny<ShipTypeDefinition>()))
                .Returns(ship);

            // Act
            var actual = new BattleshipPlanBuilder(new RandomPlacementStrategy())
                .SetDimension(1, 4)
                .AddShip(ShipType.Destroyer)
                .Build();
            
            // Assert
            actual.Cells.Length.Should().Be(4);
            actual.Ships.Length.Should().Be(1);
            actual.Ships[0].ShipType.Should().Be(ShipType.Destroyer);
            actual.Ships[0].Cells.Length.Should().Be(4);
            actual.Columns.Should().Be(1);
            actual.Rows.Should().Be(4);
        }
    }
}