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

    // Farklı actionlardaki repetetive kısım buraya taşınabilir

    internal void PlayerAction(ActionType actionType)
    {
        if (actionType == ActionType.left)
        {
            LeftAction();
        }
        else if (actionType == ActionType.right)
        {
            RightAction();
        } else
        {
            Console.WriteLine("You were not supposed to be here.");
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
        _singleton = null;
        Player.SetGameController(null);
    }

    // Bu iki fonksiyondaki kod tekrarı harbici bir sorun

    private void LeftAction()
    {
        float newFaith = _gameState.ActiveEncounter.yes._statChange.Faith + _gameState.faith;
        float newPeople = _gameState.ActiveEncounter.yes._statChange.People + _gameState.people;
        float newMoney = _gameState.ActiveEncounter.yes._statChange.Money + _gameState.money;
        float newSecurity = _gameState.ActiveEncounter.yes._statChange.Security + _gameState.security;
        Encounter nextEncounter;

        // Call fire encounter immediately
        int fireEncounterId = _gameState.ActiveEncounter.yes._fireEncounterId; 

        // unlock the encounter by adding it to the active encounters
        List<int>? unlockEncounters = _gameState.ActiveEncounter.yes.unlockEncounters;

        if (fireEncounterId != -1)
        {
            nextEncounter = _activeEncounters[fireEncounterId];
        }
        else
        {
            int randNum = _rnd.Next(_activeEncounters.Count);
            nextEncounter = _activeEncounters[randNum];
        }

        UnlockEncounters(unlockEncounters);

        _gameState.SetState(newFaith, newPeople, newMoney, newSecurity, nextEncounter);
    }

    private void RightAction()
    {
        float newFaith = _gameState.ActiveEncounter.no._statChange.Faith + _gameState.faith;
        float newPeople = _gameState.ActiveEncounter.no._statChange.People + _gameState.people;
        float newMoney = _gameState.ActiveEncounter.no._statChange.Money + _gameState.money;
        float newSecurity = _gameState.ActiveEncounter.no._statChange.Security + _gameState.security;
        Encounter nextEncounter;

        // Call fire encounter immediately
        int fireEncounterId = _gameState.ActiveEncounter.no._fireEncounterId;

        // unlock the encounter by adding it to the active encounters
        List<int>? unlockEncounters = _gameState.ActiveEncounter.no.unlockEncounters;

        if (fireEncounterId != -1)
        {
            nextEncounter = _activeEncounters[fireEncounterId];
        }
        else
        {
            int randNum = _rnd.Next(_activeEncounters.Count);
            nextEncounter = _activeEncounters[randNum];
        }

        UnlockEncounters(unlockEncounters);

        _gameState.SetState(newFaith, newPeople, newMoney, newSecurity, nextEncounter);
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
