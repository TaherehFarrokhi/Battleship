using System;

namespace BattleshipGame.Core
{
    public static class CellNameBuilder
    {
        public static string ToName(int column, int row) => $"{Convert.ToChar('A' + column)}{row + 1}";
        public static (int Column, int Row) FromName(string name)
        {
            var column = name.ToUpper()[0] - 'A';
            if (!int.TryParse(name[1..], out var row))
                row = 0;

            return (column, row - 1);
        }
    }
}