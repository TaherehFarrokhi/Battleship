using System;
using System.Linq;

namespace BattleshipGame.Core
{
    public class BattleshipController
    {
        private readonly BattleshipPlan _battleshipPlan;

        public BattleshipController(BattleshipPlan battleshipPlan)
        {
            _battleshipPlan = battleshipPlan ?? throw new ArgumentNullException(nameof(battleshipPlan));
        }

        public ExecutionResult ExecuteCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command) || command.Length < 2)
                throw new ArgumentNullException(nameof(command));

            var (column, row) = CellNameBuilder.FromName(command);

            if (column < 0 || column >= _battleshipPlan.Columns) 
                return ExecutionResult.Error("Column is out of range");
            
            if (row < 0 || row >= _battleshipPlan.Rows) 
                return ExecutionResult.Error("Row is out of range");

            return ShotCell(column, row, command);
        }

        private ExecutionResult ShotCell(int column, int row, string command)
        {
            var cell = _battleshipPlan.Cells[column, row];
            var ship = _battleshipPlan.Ships.FirstOrDefault(s => s.Cells.Contains(cell));

            if (cell.Tested) 
                return ExecutionResult.Nothing();
            
            cell.Test();
            
            if (!cell.Occupied) 
                return ExecutionResult.Miss($"Command {command} was a miss");
            
            var description = ship is {IsSank: true}
                ? $"Command {command} was a hit. Ship {ship.ShipType} is sank"
                : $"Command {command} was a hit";
            return ExecutionResult.Hit(description, _battleshipPlan.IsOver) ;
        }
    }
}