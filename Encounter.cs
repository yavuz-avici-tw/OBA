using System.Diagnostics;


internal class Encounter
{
    public int Id { get; }
    public string Character { get; }
    public string Text { get; }
    public bool IsLocked { get; }
    public bool IsContinuation { get; }

    public Action yes;
    public Action no;

    public Encounter(int id, string character, string text, Action yes, Action no, bool isLocked = false, bool isContinuation = false)
    {
        Id = id;
        Character = character;
        Text = text;
        IsLocked = isLocked;
        IsContinuation = isContinuation;
        this.yes = yes;
        this.no = no;
    }
}
internal class Action
{
    public string _text { get; }
    public StatChange _statChange { get; }
    public int _fireEncounterId { get; }
    public Encounter _fireEncounter { get; }

    public Action(string text, StatChange statChange, int fireEncounterId = -1, Encounter fireEncounter = null)
    {
        _text = text;
        _statChange = statChange;
        _fireEncounterId = fireEncounterId;
        _fireEncounter = fireEncounter;
    }

}
internal class StatChange
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
