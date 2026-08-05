public static class Player
{
    private static GameController _gameController;

    public static void Left()
    {
        _gameController?.PlayerAction(ActionType.left);
        GetInfo();
    }
    public static void Right()
    {
        _gameController?.PlayerAction(ActionType.right);
        GetInfo();
    }

    public static void GetInfo()
    {
        _gameController.PrintStatus();
    }

    internal static void SetGameController(GameController gameController)
    {
        _gameController = gameController;
    }

}
