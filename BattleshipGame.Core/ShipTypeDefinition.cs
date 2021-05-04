namespace BattleshipGame.Core
{
    public record ShipTypeDefinition
    {
        public ShipType ShipType { get; init; }
        public int Length { get; init; }
    }
}