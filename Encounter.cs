using System.Diagnostics;


class Encounter
{
    private int _id;
    private string _character { get; }
    public string Character { get { return _character; } }
    private string _text {  get; }
    private bool _isLocked { get; }
    private bool _isContinuation { get; }

    private Action _yes;
    private Action _no;

    public Encounter(int id, string character, string text, Action yes, Action no, bool isLocked = false, bool isContinuation = false)
    {
        _id = id;
        _character = character;
        _text = text;
        _isLocked = isLocked;
        _isContinuation = isContinuation;
        _yes = yes;
        _no = no;
    }

    public void print_debug()
    {
        Console.WriteLine(_text + "\n" + _id + "\n" + _character + "\n" + _isLocked.ToString() + "\n" + _isContinuation.ToString() + "\n" );
    }

}
class Action
{
    private string _text;
    private StatChange _statChange;
    private int _fireEncounterId;
    private Encounter _fireEncounter;

    public Action(string text, StatChange statChange, int fireEncounterId = -1, Encounter fireEncounter = null)
    {
        _text = text;
        _statChange = statChange;
        _fireEncounterId = fireEncounterId;
        _fireEncounter = fireEncounter;
    }



    // Constructora Encounter fireEncounter da ekle

}
class StatChange
{
    private float _faith;
    private float _people;
    private float _security;
    private float _money;

    public StatChange(float faith, float people, float security, float money)
    {
        _faith = faith;
        _people = people;
        _security = security;
        _money = money;
    }
}