using OBA;
using System.Collections.ObjectModel;
using static GameState;
public sealed class Game
{
    private Random? _rnd;
    private GameState? _gameState;

    // Encounters and active encounters
    private ReadOnlyCollection<Encounter> _encounters;
    private List<Encounter> _activeEncounters;

    // GameControlle must be instantiated only once
    private static Game? _singleton;
    public static Game? Singleton
    {
        get { return _singleton ?? (new Game()); } 
        private set {  _singleton = value; } 
    }

    private Game()
    {
        _encounters = GameData.getEncountersFromXmlData();
        _activeEncounters = _encounters.Where(enc => enc.IsLocked == false && enc.IsContinuation == false).ToList();
        _rnd = new Random();

        // selecting random encounter from active ones
        _gameState = new GameState(selectNewEncounter(_activeEncounters,_rnd));
        PrintStatus();
    }
    
    private Encounter selectNewEncounter(List<Encounter> source,Random rnd)
    {
        int randNum = rnd.Next(source.Count);
        return source[randNum];

    }

    public static void Start()
    {
        if (Singleton == null)
        {
            Singleton = new Game();
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

        float newFaith = currentAction._statChange.Faith + _gameState.Faith;
        float newPeople = currentAction._statChange.People + _gameState.People;
        float newMoney = currentAction._statChange.Money + _gameState.Money;
        float newSecurity = currentAction._statChange.Security + _gameState.Security;
        Encounter? nextEncounter = null;

        int fireEncounterId = currentAction._fireEncounterId;
        ReadOnlyCollection<int>? unlockEncounters = currentAction.unlockEncounters;

        if (fireEncounterId != -1)
        {
            nextEncounter = _activeEncounters.FirstOrDefault(enc => enc.Id == fireEncounterId);
        }
        else
        {
            
            if(_rnd == null) { _rnd = new Random(); }
        
            nextEncounter = selectNewEncounter(_activeEncounters,_rnd);
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
        _encounters=null;
        _activeEncounters.Clear();
        Singleton = null;
        
    }
    private void UnlockEncounters(ReadOnlyCollection<int>? encToUnlock)
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

    private void PrintStatus()
    {
        if (_gameState == null) { Console.Error.WriteLine("GameState is null"); return; }
        if (_gameState.ActiveEncounter == null) { Console.Error.WriteLine("No active encounter yet"); return; }

        Console.WriteLine("\t_____STATUS____\t");
        Console.WriteLine($"Faith: {_gameState.Faith}/10 People: {_gameState.People}/10 Money: {_gameState.Money}/10 Security: {_gameState.Security}/10");

        if (_gameState.IsGameOver)
        {
            PrintGameOver(_gameState.gameEndReason);
        }
        else
        {
            if (_gameState.ActiveEncounter == null) { Console.Error.WriteLine("No active encounter yet"); return; }

            Console.WriteLine($"---{_gameState.ActiveEncounter.Character}---");
            Console.WriteLine($"{_gameState.ActiveEncounter.Text}\n");
            int maxLength = Math.Max(_gameState.ActiveEncounter.yes._text.Length, _gameState.ActiveEncounter.no._text.Length);
            string yesText = new string(_gameState.ActiveEncounter.yes._text);
            string noText = new string(_gameState.ActiveEncounter.no._text);
            yesText = yesText.PadRight(maxLength);
            noText = noText.PadRight(maxLength); //⚫ • - -
            string yesEffects = getEffectStringOfAction(_gameState.ActiveEncounter.yes);
            string noEffects = getEffectStringOfAction(_gameState.ActiveEncounter.no);
            Console.WriteLine($"Player.Left()  for {yesText}     Effects: " + yesEffects);
            Console.WriteLine($"Player.Right() for {noText}     Effects: " + noEffects);
        }

    }
    private void PrintGameOver(GameEndReason endReason)
    {
        Console.WriteLine(GameData.GameEndReasonTexts[endReason]);
    }
    private string getEffectStringOfAction(Action action)
    {
        return getEffectStringOfStat(action._statChange.Faith) +
            getEffectStringOfStat(action._statChange.People) +
            getEffectStringOfStat(action._statChange.Money) +
            getEffectStringOfStat(action._statChange.Security);
    }
    private string getEffectStringOfStat(float stat)
    {
        return ((stat > 0.51f || stat < -0.51f) ? "⚫" : (stat > 0.01f || stat < -0.01f) ? "•" : "-");
    }

}
