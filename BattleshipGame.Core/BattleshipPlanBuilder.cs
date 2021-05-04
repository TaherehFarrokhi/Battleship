using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame.Core
{
    public class BattleshipPlanBuilder
    {
        private readonly IBattleshipPlacementStrategy _placementStrategy;
        private int _columns;
        private int _rows;
        private readonly List<ShipType> _shipTypes = new();

        private readonly IDictionary<ShipType, ShipTypeDefinition> _shipDefinitionTypes =
            new List<ShipTypeDefinition>
            {
                new() {ShipType = ShipType.Battleship, Length = 5},
                new() {ShipType = ShipType.Destroyer, Length = 4}
            }.ToDictionary(m => m.ShipType);

        public BattleshipPlanBuilder(IBattleshipPlacementStrategy placementStrategy)
        {
            _placementStrategy = placementStrategy ?? throw new ArgumentNullException(nameof(placementStrategy));
        }

        public BattleshipPlanBuilder SetDimension(int columns, int rows)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));

            _columns = columns;
            _rows = rows;

            return this;
        }

        public BattleshipPlanBuilder AddShip(ShipType shipType)
        {
            _shipTypes.Add(shipType);
            return this;
        }

        public BattleshipPlan Build()
        {
            var cells = new BattleshipCell[_columns, _rows];

            for (var i = 0; i < _columns; i++)
            for (var j = 0; j < _rows; j++)
                cells[i, j] = new BattleshipCell(CellNameBuilder.ToName(i, j), i, j);

            var ships = _shipTypes.Select(s => _placementStrategy.PlaceShip(cells, _shipDefinitionTypes[s])).ToArray();

            return new BattleshipPlan
            {
                Cells = cells,
                Ships = ships,
                Columns = _columns,
                Rows = _rows
            };
        }
    }
}