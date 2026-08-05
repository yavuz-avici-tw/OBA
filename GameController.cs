using OBA;
using System.Data.SqlTypes;
using System.Xml.Linq;

public class GameController
{
    private GameState gameState;

   
    
    private XDocument xdoc;
    List<Encounter> encounters;
    private static GameController singleton;
    private GameController()
    {
      
        xdoc = XDocument.Parse(GameData.gameXML);

        encounters = new List<Encounter>();

        IEnumerable<XElement> elmns = xdoc.Descendants("ENCOUNTER");
        
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
