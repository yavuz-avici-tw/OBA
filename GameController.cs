using System.Data.SqlTypes;
using System.Xml.Linq;
using System.Linq;
using System.Security.Cryptography;
using OBA;
using System.Reflection.Metadata.Ecma335;
public class GameController
{
    private Random _rnd;
    private GameState _gameState;

    // Encounters and active encounters
    private List<Encounter> _encounters;
    private List<Encounter> _activeEncounters;

    // GameControlle must be instantiated only once
    private static GameController _singleton;
    private GameController()
    {
        _encounters = GameData.getEncountersFromXmlData(GameData.gameXML);
        _activeEncounters = _encounters.Where(enc => enc.IsLocked == false && enc.IsContinuation == false).ToList();
        _rnd = new Random();

        // selecting random encounter from active ones
        int randNum = _rnd.Next(_activeEncounters.Count);
        _gameState = new GameState(_activeEncounters[randNum]);

        // initialize the player
        Player.SetGameController(this);
    }

    public static void initialize()
    {
        if (_singleton == null)
        {
            _singleton = new GameController();
            _singleton.PrintStatus();

            return;
        }
        Console.WriteLine("Game already init");
        return;
    }

    

    // Action type can be left or right
    internal void PlayerAction(ActionType actionType)
    {
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
        Encounter nextEncounter = null;

        int fireEncounterId = currentAction._fireEncounterId;
        List<int>? unlockEncounters = currentAction.unlockEncounters;

        if (fireEncounterId != -1)
        {
            nextEncounter = _activeEncounters.FirstOrDefault(enc => enc.Id == fireEncounterId);
        }
        else
        {
            int randNum = _rnd.Next(_activeEncounters.Count);
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
        Player.SetGameController(null);
    }

    private void UnlockEncounters(List<int>? encToUnlock)
    {
        if (encToUnlock != null)
        {
            foreach (int id in encToUnlock)
            {
                _activeEncounters.Add(_encounters.FirstOrDefault(i => i.Id == id));
            }
        }
    }

    public void PrintStatus()
    {
        _gameState.PrintStatus();
    }

}
