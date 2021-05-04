namespace BattleshipGame.Core
{
    public record ExecutionResult
    {
        public ExecutionResultType ResultType { get; init; } = ExecutionResultType.NoExecution;
        public string Description { get; init; }
        
        public bool GameOver { get; init; }

        public static ExecutionResult Error(string description) => 
            new() {ResultType = ExecutionResultType.Error, Description = description};

        public static ExecutionResult Hit(string description, bool gameOver) => 
            new() {ResultType = ExecutionResultType.Hit, Description = description, GameOver = gameOver};

        public static ExecutionResult Miss(string description) => 
            new() {ResultType = ExecutionResultType.Miss, Description = description};

        public static ExecutionResult Nothing() => new();
    }
}