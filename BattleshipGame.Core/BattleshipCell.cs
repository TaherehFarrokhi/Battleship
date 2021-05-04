namespace BattleshipGame.Core
{
    public class BattleshipCell
    {
        public string Name { get; }
        public int Column { get; }
        public int Row { get; }

        public BattleshipCell(string name, int column, int row)
        {
            Name = name;
            Column = column;
            Row = row;
        }
        public bool Occupied { get; private set; }
        public bool Tested { get; private set; }
        
        public void Assign()
        {
            Occupied = true;
        }

        public void Test()
        {
            Tested = true;
        }
    }
}