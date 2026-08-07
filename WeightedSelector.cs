using OBA;


public class WeightedSelector
{
    private static readonly Random _random = new Random();
    // what we have: pm_1 pm_2 pm_3 pm_4 ... pm_x

    //what we need: cp_1 cp_2 cp_3 cp_4 ... cp_x (cumulative probability) and select one each time with rnd.NextDouble()

    public static List<Encounter> SelectXUniqueEncountersWithProbabilityModifiers(List<Encounter> elements,int x,Random rnd)
    {
        List<Encounter> result = new List<Encounter>();
        
        // each element has probability x.ProbabilityModifier / total_probabilityModifiers
        List<Encounter> pool = new List<Encounter>(elements);
        for (int i = 0; i < x; i++) {
            double target = rnd.NextDouble();
            float cumulative_probability = 0;
            float total_probabilityModifiers = pool.Sum(x => x.ProbabilityModifier);
            for (int j=0;j< pool.Count;j++)
            {
                float probability = pool[j].ProbabilityModifier / total_probabilityModifiers;
                cumulative_probability += probability;
                if (target < cumulative_probability)
                {
                    result.Add(pool[j]);
                    pool.RemoveAt(j);
                    break;
                }
            }
        }
        return result;
    }
    public static void Test()
    {
        Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

        for (int i = 0; i < 1000; i++)
        {
            List<Encounter> _encounters = new List<Encounter>(GameData.getEncountersFromXmlData());
            List<Encounter> result = SelectXUniqueEncountersWithProbabilityModifiers(_encounters, 6, new Random());

            foreach (Encounter encounter in result)
            {
                if (keyValuePairs.ContainsKey(encounter.Id))
                {
                    keyValuePairs[encounter.Id] += 1;
                }
                else
                {
                    keyValuePairs.Add(encounter.Id, 1);
                }
            }
        }
        foreach ((int id, int numofoccurence) in keyValuePairs)
        {
            Console.WriteLine($"{id} occured {numofoccurence} times");
        }
    }

}