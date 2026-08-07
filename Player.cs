public static class Player
{
    private static Game? game;
    public static void StartGame()
    {
        game = Game.initialize();
    }
    public static void Left()
    {
        PlayerAction(ActionType.left);
        
    }
    public static void Right()
    {
        PlayerAction(ActionType.right);
        
    }
    private static void PlayerAction(ActionType action)
    {
        if (game == null) { Console.WriteLine($"Run {nameof(Player)}.{nameof(Player.StartGame)} first to initialize the game"); return; }
        game.PlayerAction(action);
    }
}