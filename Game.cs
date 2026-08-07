using OBA;
using System.Collections.ObjectModel;
using System.Globalization;
using static GameState;
public sealed class Game
{
    private Random? _rnd;
    private GameState? _gameState;

    // Encounters and active encounters
    private ReadOnlyCollection<Encounter> _encounters;
    private List<Encounter> _activeEncounters;
    private Stack<Encounter> _activeStack;
    private const int stackSize = 3;
    private int _dayProgress;
    private DateOnly _date;
    private CultureInfo _turkishCulture = new CultureInfo("tr-TR");

    private static Game? _singleton = null;
    private Game()
    {
        _encounters = GameData.getEncountersFromXmlData();
        _activeEncounters = _encounters.Where(enc => enc.IsLocked == false && enc.IsContinuation == false).ToList();
        _rnd = new Random();
        SetEncounterStack();

        // selecting random encounter from active ones

        if (_activeStack == null) { Console.Error.WriteLine("ERROR::ACTIVE_STACK_CANNOT_BE_NULL"); return; }
        
        _gameState = new GameState(_activeStack.Pop());
        _dayProgress = 0;
        _date = new DateOnly(1085,3,28);
        PrintStatus();
    }
    public static Game? initialize()
    {
        
        if (_singleton == null)
        {
            _singleton = new Game();
            return _singleton;
            
        }
        
        Console.WriteLine("Game already initialized");
        return null;
    }
    
    


    // Action type can be left or right
    public void PlayerAction(ActionType actionType)
    {
        if (_singleton == null) { Console.WriteLine($"Run {nameof(Player)}.{nameof(Player.StartGame)} first to initialize the game"); return; }
        if (_gameState== null) { Console.Error.WriteLine("GameState is null"); return; }
        if (_gameState.ActiveEncounter == null) { Console.Error.WriteLine("No active encounter yet"); return; }
        Action currentAction = _gameState.ActiveEncounter.TakeAction(actionType);
        if (currentAction == null)
        {
            Console.Error.WriteLine("ERROR::GAME_CONTROLLER::COULDN'T_FETCH_ACTIVE_ACTION");
            return;
        }


        bool isOneTimeEncounter = _gameState.ActiveEncounter.IsOneTime;
        if (isOneTimeEncounter)
        {
            _activeEncounters.Remove(_gameState.ActiveEncounter);
        }

        float newFaith = currentAction.statChange.Faith + _gameState.Faith;
        float newPeople = currentAction.statChange.People + _gameState.People;
        float newMoney = currentAction.statChange.Money + _gameState.Money;
        float newSecurity = currentAction.statChange.Security + _gameState.Security;
        Encounter? nextEncounter = null;

        int fireEncounterId = currentAction.FireEncounterId;

        ReadOnlyCollection<int>? unlockEncounters = currentAction.unlockEncounters;

        if (fireEncounterId != -1)
        {
            _activeStack.Push(_activeEncounters.FirstOrDefault(enc => enc.Id == fireEncounterId));
        }
        nextEncounter = _activeStack.Pop();

        UnlockEncounters(unlockEncounters);

        _gameState.SetState(newFaith, newPeople, newMoney, newSecurity, nextEncounter);

        if (_activeStack.Count <= 0)
        {
            SetEncounterStack();
        }
        int _daysToProgress = _rnd.Next(12, 36);
        
        _dayProgress += _daysToProgress;
        _date = _date.AddDays(_daysToProgress);
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
        _singleton = null;


    }

    private void SetEncounterStack()
    {
        if (_rnd == null)
        {
            _rnd = new Random();
        }
        _activeStack = new Stack<Encounter> (WeightedSelector.SelectXUniqueEncountersWithProbabilityModifiers(_activeEncounters, stackSize, _rnd));

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

        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("-:::STATUS:::-");
        Console.WriteLine("");
        Console.WriteLine($"{_date.ToString("D", _turkishCulture)}");
        Console.WriteLine("");
        Console.WriteLine($"İnanç: {_gameState.Faith}/10 | Halk Refahı: {_gameState.People}/10 | Maddi durum: {_gameState.Money}/10 | Oba Güvenliği: {_gameState.Security}/10");
        Console.WriteLine("___________________________________________________");

        if (_gameState.IsGameOver)
        {
            PrintGameOver(_gameState.gameEndReason);
        }
        else
        {
            if (_gameState.ActiveEncounter == null) { Console.Error.WriteLine("No active encounter yet"); return; }

            Console.WriteLine($"~{_gameState.ActiveEncounter.Character}:\t\t");
            Console.WriteLine($"->{_gameState.ActiveEncounter.Text}\n");

            int maxLength = Math.Max(_gameState.ActiveEncounter.Yes.Text.Length, _gameState.ActiveEncounter.No.Text.Length);
            string yesText = new string(_gameState.ActiveEncounter.Yes.Text);
            string noText = new string(_gameState.ActiveEncounter.No.Text);
            yesText = yesText.PadRight(maxLength);
            noText = noText.PadRight(maxLength); //⚫ • - -
            string yesEffects = getEffectStringOfAction(_gameState.ActiveEncounter.Yes);
            string noEffects = getEffectStringOfAction(_gameState.ActiveEncounter.No);

            Console.WriteLine($"Player.Left()\t: {yesText}     Effects: " + yesEffects);
            Console.WriteLine($"Player.Right()\t: {noText}     Effects: " + noEffects);
        }

    }

    private void PrintGameOver(GameEndReason endReason)
    {
        Console.WriteLine(GameData.GameEndReasonTexts[endReason]);
    }
    private string getEffectStringOfAction(Action action)
    {
        return getEffectStringOfStat(action.statChange.Faith) +
            getEffectStringOfStat(action.statChange.People) +
            getEffectStringOfStat(action.statChange.Money) +
            getEffectStringOfStat(action.statChange.Security);
    }
    private string getEffectStringOfStat(float stat)
    {
        return ((stat > 0.51f || stat < -0.51f) ? "⚫" : (stat > 0.01f || stat < -0.01f) ? "•" : "-");
    }

}
