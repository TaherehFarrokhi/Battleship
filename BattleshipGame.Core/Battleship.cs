using System.Linq;

namespace BattleshipGame.Core
{
    public class Battleship
    {
        public ShipType ShipType { get; init; }
        public BattleshipCell[] Cells { get; init; } = { };
        public bool IsSank => Cells.All(c => c.Occupied && c.Tested);

        public override string ToString()
        {
            return string.Join(",", Cells?.Select(c => c.Name).ToArray());
        }
    }
}