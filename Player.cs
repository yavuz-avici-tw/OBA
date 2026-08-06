public static class Player
{
    private static Game? _game;

    public static void Left()
    {
        if (_game == null) { Console.WriteLine($"Run {nameof(Game)}.{nameof(Game.Start)} first to initialize the game"); return; }
        _game?.PlayerAction(ActionType.left);
        
    }
    public static void Right()
    {
        if (_game == null) { Console.WriteLine($"Run {nameof(Game)}.{nameof(Game.Start)} first to initialize the game"); return; }
        _game?.PlayerAction(ActionType.right);
        
    }


    internal static void SetGame(Game? game)
    {
        _game = game;
    }

}