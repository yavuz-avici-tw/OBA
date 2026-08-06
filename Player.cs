public static class Player
{
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
        if (Game.Singleton == null) { Console.WriteLine($"Run {nameof(Game)}.{nameof(Game.Start)} first to initialize the game"); return; }
        Game.Singleton?.PlayerAction(action);
    }
}