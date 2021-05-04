using FluentAssertions;
using Xunit;

namespace BattleshipGame.Core.UnitTests
{
    public class RandomPlacementStrategyTests
    {
        [Fact]
        public void Should_PlaceShip_ReturnsCorrectPlan_DependsOnGivenInputs()
        {
            // Arrange
            var cells = new BattleshipCell[1, 4];
            cells[0,0] = new BattleshipCell("A1", 0, 0);
            cells[0,1] = new BattleshipCell("A2", 0, 1);
            cells[0,2] = new BattleshipCell("A3", 0, 2);
            cells[0,3] = new BattleshipCell("A4", 0, 3);

            var sut = new RandomPlacementStrategy();

            // Act
            var actual = sut.PlaceShip(cells, new ShipTypeDefinition {ShipType = ShipType.Destroyer, Length = 4});

            // Assert
            actual.ShipType.Should().Be(ShipType.Destroyer);
            actual.Cells.Length.Should().Be(4);
            actual.Cells.Should().BeEquivalentTo(cells[0, 0], cells[0, 1], cells[0, 2], cells[0, 3]);
        }
    }
}