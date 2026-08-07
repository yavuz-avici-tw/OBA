using OBA;
using System;
using static GameState;
public class Game
{
    private Random? _rnd;
    private GameState? _gameState;

    // Encounters and active encounters
    private List<Encounter> _encounters;
    private List<Encounter> _activeEncounters;
    private Stack<Encounter> _activeStack;
    private const int stackSize = 3;

    // GameControlle must be instantiated only once
    public static Game? Singleton { get; private set; }

    private Game()
    {
        _encounters = GameData.getEncountersFromXmlData();
        _activeEncounters = _encounters.Where(enc => enc.IsLocked == false && enc.IsContinuation == false).ToList();
        _rnd = new Random();
        SetEncounterStack();

        // selecting random encounter from active ones
        //int randNum = _rnd.Next(_activeEncounters.Count);
        //_gameState = new GameState(_activeEncounters[randNum]);
        if (_activeStack == null) { Console.Error.WriteLine("ERROR::ACTIVE_STACK_CANNOT_BE_NULL"); return; }
        
        _gameState = new GameState(_activeStack.Pop());
    }

    public static void Start()
    {
        if (Singleton == null)
        {
            Singleton = new Game();
            Singleton.PrintStatus();

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

        bool isOneTimeEncounter = _gameState.ActiveEncounter.IsOneTime;
        if (isOneTimeEncounter)
        {
            _encounters.Remove(_gameState.ActiveEncounter);
        }

        float newFaith = currentAction._statChange.Faith + _gameState.faith;
        float newPeople = currentAction._statChange.People + _gameState.people;
        float newMoney = currentAction._statChange.Money + _gameState.money;
        float newSecurity = currentAction._statChange.Security + _gameState.security;
        int fireEncounterId = currentAction._fireEncounterId;
        List<int>? unlockEncounters = currentAction.unlockEncounters;

        Encounter? nextEncounter = null;

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
        Singleton = null;
    }

    private void SetEncounterStack()
    {
        var rnd = new Random();

        float totalProbModifier = _activeEncounters.Sum(enc => enc.ProbabilityModifier);

        for (int i = 0; i < stackSize; i++)
        {
            float probability = (float)(_rnd.NextDouble() * (totalProbModifier - 0.0f) + 0.0f);
            _activeEncounters.Sort((x, y) => x.ProbabilityModifier.CompareTo(y.ProbabilityModifier));

            var selectedEncounter = _activeEncounters.SkipWhile(i => i.ProbabilityModifier < probability).First();

            _activeStack.Push(selectedEncounter);
        }
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
        if (_gameState.ActiveEncounter == null) { Console.Error.WriteLine("No active encounter yet"); return; }

        Console.WriteLine("\t_____STATUS____\t");
        Console.WriteLine($"Faith: {_gameState.faith}/10 People: {_gameState.people}/10 Money: {_gameState.money}/10 Security: {_gameState.security}/10");

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
