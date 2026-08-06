internal class GameState
{
    public float faith;
    public float people;
    public float money;
    public float security;

    // acitve encounter set etme kısmı tehlikeli GameState'in internalına güvendim
    public Encounter ActiveEncounter { get; set; }

    public GameState(Encounter activeEncounter, float faith = 5.0F, float people = 5.0f, float money = 5.0f, float security = 5.0f)
    {
        this.faith = faith;
        this.people = people;
        this.money = money;
        this.security = security;
        ActiveEncounter = activeEncounter;
    }

    internal protected void SetState(float faith, float people, float money, float security, Encounter activeEncounter)
    {
        this.faith = faith;
        this.people = people;
        this.money = money;
        this.security = security;
        ActiveEncounter = activeEncounter;
    }

    public void PrintStatus()
    {
        Console.WriteLine("\t_____STATUS____\t");
        Console.WriteLine($"Faith: {faith}/10 People: {people}/10 Money: {money}/10 Security: {security}/10");
        Console.WriteLine($"---{ActiveEncounter.Character}---");
        Console.WriteLine($"{ActiveEncounter.Text}");
        Console.WriteLine($"Player.Left for {ActiveEncounter.yes._text}");
        Console.WriteLine($"PLayer.Right for {ActiveEncounter.no._text}");

    }
}