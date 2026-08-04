using System.IO.Enumeration;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

class GameController
{
    private GameState gameState;

    // TODO:
    const string filename = "C:\\Users\\yavuz.avci\\source\\repos\\OBA\\game.xml";
    XDocument xdoc = XDocument.Load(filename);
    List<Encounter> encounters;

    public GameController()
    {
        encounters = new List<Encounter>();

        IEnumerable<XElement> elmns = xdoc.Descendants("ENCOUNTER");
        Console.WriteLine(elmns);
        foreach (XElement elm in elmns)
        {
            int id = int.Parse(elm.Attribute("id")?.Value.ToString());
            string character = elm.Element("CHARACTER")?.Value.ToString();
            string text = elm.Element("TEXT")?.Value.ToString();

            string text_y = elm.Element("YES")?.Element("TEXT")?.Value.ToString();
            float stat_faith_y = float.Parse(elm.Element("YES")?.Element("STAT_CHANGE")?.Element("FAITH")?.Value);
            float stat_people_y = float.Parse(elm.Element("YES")?.Element("STAT_CHANGE")?.Element("PEOPLE")?.Value);
            float stat_security_y = float.Parse(elm.Element("YES")?.Element("STAT_CHANGE")?.Element("SECURITY")?.Value);
            float stat_money_y = float.Parse(elm.Element("YES")?.Element("STAT_CHANGE")?.Element("MONEY")?.Value);
            // int fire_y_id = int.Parse(elm.Element("YES")?.Element("FIRE_ENCOUNTER")?.Value);

            string text_n = elm.Element("NO")?.Element("TEXT")?.Value.ToString();
            float stat_faith_n = float.Parse(elm.Element("NO")?.Element("STAT_CHANGE")?.Element("FAITH")?.Value);
            float stat_people_n = float.Parse(elm.Element("NO")?.Element("STAT_CHANGE")?.Element("PEOPLE")?.Value);
            float stat_security_n = float.Parse(elm.Element("NO")?.Element("STAT_CHANGE")?.Element("SECURITY")?.Value);
            float stat_money_n = float.Parse(elm.Element("NO")?.Element("STAT_CHANGE")?.Element("MONEY")?.Value);
            // int fire_n_id = int.Parse(elm.Element("NO")?.Element("FIRE_ENCOUNTER")?.Value);

            StatChange enc_st_y = new StatChange(stat_faith_y, stat_people_y, stat_security_y, stat_money_y);
            StatChange enc_st_n = new StatChange(stat_faith_n, stat_people_n, stat_security_n, stat_money_n);

            Action enc_y = new Action(text_y, enc_st_y);
            Action enc_n = new Action(text_n, enc_st_n);

            Encounter enc = new Encounter(id, character, text, false, false, enc_y, enc_n);
            encounters.Add(enc);
        }
        foreach (Encounter enc in encounters)
        {
            Console.WriteLine(enc.Character);
        }
    }
}

class PlayerController
{
    public void left()
    {

    }
    public void right()
    {

    }
}

class GameState
{
    private float _faith { get; }
    private float _people { get; }
    private float _money { get; }
    private float _security { get; }
    private Encounter _activeEncounter { get; set; }
    public GameState(float faith, float people, float money, float security, Encounter activeEncounter)
    {
        _faith = faith;
        _people = people;
        _money = money;
        _security = security;
        _activeEncounter = activeEncounter;
    }

}

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

    // Constructora Encounter fireEncounter da ekle
    public Action(string text, StatChange statChange, int fireEncounterId=-1, Encounter fireEncounter=null)
    {
        _text = text;
        _statChange = statChange;
        _fireEncounterId = fireEncounterId;
        _fireEncounter = fireEncounter;
    }
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

class Program
{
    static void Main()
    {
        GameController controller = new GameController();
    }
}