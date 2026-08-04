using System.IO.Enumeration;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

class GameController
{
    private XmlDocument xDoc = new XmlDocument();
    // TODO:
    const string filename = "C:\\Users\\yavuz.avci\\source\\repos\\OBA\\game.xml";

    GameController()
    {
        xDoc.Load(filename);
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
    private float faith { get; }
    private float people { get; }
    private float money { get; }
    private float security { get; }
    private Encounter activeEncounter { get; set; }
}

class Encounter
{
    string character; // characteri bir class olarak tanımlayıp xml'de id belirtebiliriz
    string text {  get; }

    bool isLocked;

    // stat effects
    float faith;
    float people;
    float security;
    float money;
}
class Deck
{
    List<Encounter> encounters {  get; }

    public void AddEncounter(Encounter enc)
    {
        encounters.Add(enc);
    }
}
