namespace BattleshipGame.Core
{
    public interface IBattleshipPlacementStrategy
    {
        Battleship PlaceShip(BattleshipCell[,] cells, ShipTypeDefinition definition);
    }
}