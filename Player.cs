public static class Player
{
    private static GameController _gameController;

    public static void Left()
    {
        if (_gameController == null) { Console.WriteLine($"Run {nameof(GameController)}.{nameof(GameController.initialize)} first to initialize the game"); return; }
        _gameController?.PlayerAction(ActionType.left);
        GetInfo();
    }
    public static void Right()
    {
        if (_gameController == null) { Console.WriteLine($"Run {nameof(GameController)}.{nameof(GameController.initialize)} first to initialize the game"); return; }
        _gameController?.PlayerAction(ActionType.right);
        GetInfo();
    }

    private static void GetInfo()
    {
        _gameController.PrintStatus();
    }

    internal static void SetGameController(GameController gameController)
    {
        _gameController = gameController;
    }

}