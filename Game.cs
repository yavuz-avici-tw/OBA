using OBA;
public class Game
{
    private Random? _rnd;
    private GameState? _gameState;

    // Encounters and active encounters
    private List<Encounter> _encounters;
    private List<Encounter> _activeEncounters;

    // GameControlle must be instantiated only once
    private static Game? _singleton;
    private Game()
    {
        _encounters = GameData.getEncountersFromXmlData(GameData.gameXML);
        _activeEncounters = _encounters.Where(enc => enc.IsLocked == false && enc.IsContinuation == false).ToList();
        _rnd = new Random();

        // selecting random encounter from active ones
        int randNum = _rnd.Next(_activeEncounters.Count);
        _gameState = new GameState(_activeEncounters[randNum]);

        // initialize the player
        Player.SetGame(this);
    }

    public static void Start()
    {
        if (_singleton == null)
        {
            _singleton = new Game();
            _singleton.PrintStatus();

            return;
        }
        Console.WriteLine("Game already init");
        return;
    }

    

    // Action type can be left or right
    internal void PlayerAction(ActionType actionType)
    {
        if(_gameState== null) { Console.Error.WriteLine("GameState is null"); return; }
        if (_gameState.ActiveEncounter == null) { Console.Error.WriteLine("No active encounter yet"); return; }
        Action currentAction = _gameState.ActiveEncounter.TakeAction(actionType);
        if (currentAction == null)
        {
            Console.Error.WriteLine("ERROR::GAME_CONTROLLER::COULDN'T_FETCH_ACTIVE_ACTION");
            return;
        }

        float newFaith = currentAction._statChange.Faith + _gameState.faith;
        float newPeople = currentAction._statChange.People + _gameState.people;
        float newMoney = currentAction._statChange.Money + _gameState.money;
        float newSecurity = currentAction._statChange.Security + _gameState.security;
        Encounter? nextEncounter = null;

        int fireEncounterId = currentAction._fireEncounterId;
        List<int>? unlockEncounters = currentAction.unlockEncounters;

        if (fireEncounterId != -1)
        {
            nextEncounter = _activeEncounters.FirstOrDefault(enc => enc.Id == fireEncounterId);
        }
        else
        {
            int randNum;
            if(_rnd == null) { _rnd = new Random(); }
            randNum = _rnd.Next(_activeEncounters.Count);
            nextEncounter = _activeEncounters[randNum];
        }

        UnlockEncounters(unlockEncounters);
        _gameState.SetState(newFaith, newPeople, newMoney, newSecurity, nextEncounter);
        PrintStatus();
        if (_gameState.IsGameOver)
        {
            KillYourself();
        }
    }

    // Destructor yazılabilir mi?
    private void KillYourself()
    {
        
        _gameState = null;
        _rnd = null;
        _encounters.Clear();
        _activeEncounters.Clear();
        _singleton = null;
        Player.SetGame(null);
    }
    private void UnlockEncounters(List<int>? encToUnlock)
    {
        if (encToUnlock != null)
        {
            foreach (int id in encToUnlock)
            {
                Encounter? encounter = _encounters.FirstOrDefault(i => i.Id == id);
                if(encounter != null)
                {
                    _activeEncounters.Add(encounter);
                } else
                {
                    Console.Error.WriteLine($"No such encounter with id {id}");
                }
                
            }
        }
    }

    public void PrintStatus()
    {
        if (_gameState == null) { Console.Error.WriteLine("GameState is null"); return; }
        _gameState.PrintStatus();
    }

}
