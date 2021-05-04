using System.Linq;

namespace BattleshipGame.Core
{
    public class BattleshipPlan
    {
        public int Columns { get; init; }
        public int Rows { get; init; }
        public BattleshipCell[,] Cells { get; init; }
        public Battleship[] Ships { get; init; }
        public bool IsOver => Ships.All(s => s.IsSank);
    }
}