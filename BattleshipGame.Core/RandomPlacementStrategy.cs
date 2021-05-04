using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame.Core
{
    public class RandomPlacementStrategy : IBattleshipPlacementStrategy
    {
        public Battleship PlaceShip(BattleshipCell[,] cells, ShipTypeDefinition definition)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));
            if (definition == null) throw new ArgumentNullException(nameof(definition));

            var availableCells = FindUnoccupiedPlace(cells, definition);
            foreach (var battleshipCell in availableCells)
            {
                battleshipCell.Assign();
            }
            return new Battleship {ShipType = definition.ShipType, Cells = availableCells};
        }

        private BattleshipCell[] FindUnoccupiedPlace(BattleshipCell[,] cells, ShipTypeDefinition definition)
        {
            while (true)
            {
                var columnsCount = cells.GetLength(0);
                var column = new Random().Next(columnsCount - 1);
                var rowsCount = cells.GetLength(1);
                var row = new Random().Next(rowsCount - 1);

                var enumerator = Enumerable.Range(1, definition.Length - 1).ToList();

                IEnumerable<BattleshipCell> AvailableCells(Func<int, BattleshipCell> cellSelector)
                {
                    return enumerator.Select(cellSelector).Where(c => !c.Occupied).ToList();
                }

                var cell = cells[column, row];
                if (cell.Occupied) 
                    continue;
                
                var remainingLength = definition.Length - 1;
                var availableCells = new List<BattleshipCell>{cell};
                if (column + remainingLength < columnsCount)
                    availableCells.AddRange(AvailableCells(e => cells[column + e, row]));

                if (row + remainingLength < rowsCount)
                    availableCells.AddRange(AvailableCells(e => cells[column, row + e]));

                if (availableCells.Count == definition.Length)
                    return availableCells.ToArray();
            }
        }
    }
}