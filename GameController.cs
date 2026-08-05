using OBA;
using System.Data.SqlTypes;
using System.Xml.Linq;

public class GameController
{
    private GameState gameState;

   
    
    private XDocument xdoc;
    private List<Encounter> encounters;
    private static GameController singleton;
    private GameController()
    {
      
        xdoc = XDocument.Parse(GameData.gameXML);

        encounters = new List<Encounter>();

        IEnumerable<XElement> elmns = xdoc.Descendants("ENCOUNTER");
        
        foreach (XElement elm in elmns)
        {
            
            Encounter enc =parseEncounter(elm);
            encounters.Add(enc);
            
        }
        foreach (Encounter enc in encounters)
        {
            Console.WriteLine(enc.Character);
        }
    }
    private Encounter parseEncounter(XElement encounterElement)
    {
        int id;
        string? character;
        string? text;
        if(!int.TryParse(encounterElement.Attribute("id")?.Value.ToString(), out id))
        {
            Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ID_IS_A_MUST");
            System.Environment.Exit(-1);
        }
        
        character = encounterElement.Element("CHARACTER")?.Value.ToString();
        text = encounterElement.Element("TEXT")?.Value.ToString();
        if (character == null)
        {
            Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::CHARACTER_NOT_FOUND");
            System.Environment.Exit(-1);
        }
        if (text == null)
        {
            Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::TEXT_NOT_FOUND");
            System.Environment.Exit(-1);
        }

        XElement? yesElement = encounterElement.Element("YES");
        XElement? noElement = encounterElement.Element("NO");
        if (yesElement == null || noElement == null) {
            Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::YES_NO_ELEMENT_NOT_FOUND");
            System.Environment.Exit(-1);
        }
        
        Action yes = parseAction(yesElement);
        Action no = parseAction(noElement);
        return new Encounter(id, character, text, false, false, yes , no);
    }
    private Action parseAction(XElement actionElement)
    {
        string text = actionElement.Element("TEXT")?.Value.ToString() ?? actionElement.Name.ToString();
        float stat_faith;
        float stat_people;
        float stat_security;
        float stat_money;
        if (!float.TryParse(actionElement.Element("STAT_CHANGE")?.Element("FAITH")?.Value, out stat_faith)) {
            Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::FAITH_MUST_BE_A_FLOATING_POINT_NUMBER");
            System.Environment.Exit(-1);
        }
        if(!float.TryParse(actionElement.Element("STAT_CHANGE")?.Element("PEOPLE")?.Value, out stat_people)){
            Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::PEOPLE_MUST_BE_A_FLOATING_POINT_NUMBER");
            System.Environment.Exit(-1);
        }
        if(!float.TryParse(actionElement.Element("STAT_CHANGE")?.Element("SECURITY")?.Value, out stat_security)){
            Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::SECURITY_MUST_BE_A_FLOATING_POINT_NUMBER");
            System.Environment.Exit(-1);
        }
        if(!float.TryParse(actionElement.Element("STAT_CHANGE")?.Element("MONEY")?.Value, out stat_money)){
            Console.Error.WriteLine("ERROR::PARSING::ENCOUNTER::ACTION::MONEY_MUST_BE_A_FLOATING_POINT_NUMBER");
            System.Environment.Exit(-1);
        }
        // int fire_y_id = int.Parse(elm.Element("YES")?.Element("FIRE_ENCOUNTER")?.Value);
        return new Action(text, new StatChange(stat_faith,stat_people,stat_security,stat_money));
    }

    public static string initialize()
    {
        if (singleton == null)
        {
            singleton = new GameController();
            return "Hello traveler.";
        } 
        
        return "Game already initialized";
        
    }
}
