public static class Player
{
    private static GameController _gameController;

    public static void Left()
    {
        if (_gameController == null) { Console.WriteLine($"Run {nameof(GameController)}.{nameof(GameController.initialize)} first to initialize the game"); return; }
        _gameController?.PlayerAction(ActionType.left);
        
    }
    public static void Right()
    {
        if (_gameController == null) { Console.WriteLine($"Run {nameof(GameController)}.{nameof(GameController.initialize)} first to initialize the game"); return; }
        _gameController?.PlayerAction(ActionType.right);
        
    }


    internal static void SetGameController(GameController gameController)
    {
        _gameController = gameController;
    }

}