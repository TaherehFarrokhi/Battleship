using System;
using BattleshipGame.Core;

namespace BattleshipGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var plan = new BattleshipPlanBuilder(new RandomPlacementStrategy())
                .SetDimension(10, 10)
                .AddShip(ShipType.Battleship)
                .AddShip(ShipType.Destroyer)
                .AddShip(ShipType.Destroyer)
                .Build();
            var controller = new BattleshipController(plan);
            
            while (true)
            {
                Console.Write("Enter your command: ");
                var command = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(command))
                    continue;
                
                if (command.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
                    Environment.Exit(0);

                var result = controller.ExecuteCommand(command);
                if (result.ResultType != ExecutionResultType.NoExecution)
                    Console.WriteLine(result.Description);

                if (!result.GameOver) 
                    continue;
                
                Console.WriteLine("Game over ...");
                Environment.Exit(0);
            }
        }
    }
}