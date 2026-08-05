class Encounter
{
    private int _id;
    private string _character { get; }
    public string Character { get { return _character; } }
    private string _text {  get; }
    private bool _isLocked { get; }
    private bool _isContinuation { get; }

    Action yes;
    Action no;

    public Encounter(int id, string character, string text, bool isLocked, bool isContinuation, Action yes, Action no)
    {
        _id = id;
        _character = character;
        _text = text;
        _isLocked = isLocked;
        _isContinuation = isContinuation;
        this.yes = yes;
        this.no = no;
    }

}
class Action
{
    string _text;
    StatChange _statChange;
    int _fireEncounterId;
    Encounter _fireEncounter;

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