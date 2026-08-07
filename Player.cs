public static class Player
{
    private static Game? _game;
    public static void StartGame()
    {
        Game? newgame = Game.initialize();
        if(newgame == null && _game != null) {
            Console.WriteLine("Game was already intialized");
            return;
        }
        _game = newgame; 
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
        if (_game == null) { Console.WriteLine($"Run {nameof(Player)}.{nameof(Player.StartGame)}() first to initialize the game"); return; }
        _game.PlayerAction(action);
    }
}