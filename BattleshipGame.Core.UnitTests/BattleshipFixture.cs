using System.Linq;

namespace BattleshipGame.Core.UnitTests
{
    public class BattleshipFixture
    {
        public BattleshipPlan BattleshipPlan()
        {
            var cells = new BattleshipCell[2, 4];
            for (var i = 0; i < 4; i++)
            {
                cells[0, i] = new BattleshipCell(CellNameBuilder.ToName(0, i), 0, i);
                cells[1, i] = new BattleshipCell(CellNameBuilder.ToName(1, i), 1, i);
            }

            var ship = new Battleship
            {
                ShipType = ShipType.Destroyer,
                Cells = Enumerable.Range(0, 4).Select(e => cells[0, e]).ToArray()
            };
            
            foreach (var cell in ship.Cells)
            {
                cell.Assign();
            }
            
            return new BattleshipPlan
            {
                Cells = cells,
                Ships = new []{ship},
                Columns = 2, 
                Rows = 4
            };
        }
    }
}