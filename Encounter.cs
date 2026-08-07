using System.Collections.ObjectModel;

public class Encounter
{
    public int Id { get; }
    public string Character { get; }
    public string Text { get; }
    public bool IsLocked { get; }
    public bool IsContinuation { get; }
    public Action Yes { get; }
    public Action No { get; }
    public float ProbabilityModifier { get; }
    public bool IsOneTime { get; }

    public Encounter(int id, string character, string text, Action yes, Action no, float probabilityModifier=1.0f, bool isLocked = false, bool isContinuation = false, bool isOneTime = false)
    {
        Id = id;
        Character = character;
        Text = text;
        IsLocked = isLocked;
        IsContinuation = isContinuation;
        this.Yes = yes;
        this.No = no;
        ProbabilityModifier = probabilityModifier;
        IsOneTime = isOneTime;
    }

    public Action TakeAction(ActionType action)
    {
        if (action == ActionType.left)
        {
            return Yes;
        }
        else if (action == ActionType.right)
        {
            return No;
        }

        // All code paths need to return a value
        return Yes;
    }
}
public class Action
{
    public string Text { get; }
    public StatChange statChange { get; }
    public int FireEncounterId { get; }
    public Encounter? FireEncounter { get; }
    public ReadOnlyCollection<int>? unlockEncounters { get; }

    public Action(string text, StatChange statChange, int fireEncounterId = -1, Encounter? fireEncounter = null, ReadOnlyCollection<int>? unlockEncounters =null)
    {
        Text = text;
        this.statChange = statChange;
        FireEncounterId = fireEncounterId;
        FireEncounter = fireEncounter;
        this.unlockEncounters = unlockEncounters;
        
        
    }

}
public class StatChange
{
    public float Faith {get;}
    public float People {get;}
    public float Security {get;}
    public float Money { get; }

    public StatChange(float faith, float people, float security, float money)
    {
        Faith = faith;
        People = people;
        Security = security;
        Money = money;
    }
}
