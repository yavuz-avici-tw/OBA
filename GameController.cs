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
      
        encounters = GameData.getEncountersFromXmlData(GameData.gameXML);
        foreach (Encounter enc in encounters)
        {
            enc.print_debug();
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
