using OBA;

internal class GameState
{
    public float faith;
    public float people;
    public float money;
    public float security;
    public bool IsGameOver { get; private set; }
    private GameEndReason _gameOverReason;
    // acitve encounter set etme kısmı tehlikeli GameState'in internalına güvendim
    public Encounter? ActiveEncounter { get; private set; }

    public GameState(Encounter activeEncounter, float faith = 5.0F, float people = 5.0f, float money = 5.0f, float security = 5.0f)
    {
        this.faith = faith;
        this.people = people;
        this.money = money;
        this.security = security;
        ActiveEncounter = activeEncounter;
        IsGameOver = false;
    }

    internal protected void SetState(float faith, float people, float money, float security, Encounter? activeEncounter)
    {
        this.faith = faith;
        this.people = people;
        this.money = money;
        this.security = security;
        ActiveEncounter = activeEncounter;
        CheckIsGameOver();
    }

    public void PrintStatus()
    {
        
        Console.WriteLine("\t_____STATUS____\t");
        Console.WriteLine($"Faith: {faith}/10 People: {people}/10 Money: {money}/10 Security: {security}/10");
        if (IsGameOver)
        {
            PrintGameOver(_gameOverReason);
        }
        else {
            if (ActiveEncounter == null) { Console.Error.WriteLine("No active encounter yet"); return; }
            Console.WriteLine($"---{ActiveEncounter.Character}---");
            Console.WriteLine($"{ActiveEncounter.Text}\n");
            int maxLength = Math.Max(ActiveEncounter.yes._text.Length, ActiveEncounter.no._text.Length);
            string yesText = new string(ActiveEncounter.yes._text);
            string noText = new string(ActiveEncounter.no._text);
            yesText =yesText.PadRight(maxLength);
            noText =noText.PadRight(maxLength); //⚫ • - -
            string yesEffects = getEffectStringOfAction(ActiveEncounter.yes);
            string noEffects = getEffectStringOfAction(ActiveEncounter.no);
            Console.WriteLine($"Player.Left()  for {yesText}     Effects: "+yesEffects);
            Console.WriteLine($"Player.Right() for {noText}     Effects: "+noEffects);
        }
        
    }
    private string getEffectStringOfAction(Action action)
    {
        return getEffectStringOfStat(action._statChange.Faith) +
            getEffectStringOfStat(action._statChange.People) +
            getEffectStringOfStat(action._statChange.Money) +
            getEffectStringOfStat(action._statChange.Security);
    }
    private string getEffectStringOfStat(float stat)
    {
        return ((stat > 0.51f || stat<-0.51f) ? "⚫" : (stat > 0.01f || stat < -0.01f) ? "•" : "-");
    }
    private void PrintGameOver(GameEndReason endReason)
    {
        Console.WriteLine(GameData.GameEndReasonTexts[endReason]);
    }
    private void CheckIsGameOver()
    {
        if(faith < 0.0f)
        {
            IsGameOver=true;
            _gameOverReason = GameEndReason.LowFaith;
            return;
        }
        if (faith > 10.0f)
        {
            IsGameOver = true; _gameOverReason = GameEndReason.HighFaith;
            return;
        }
        if (people < 0.0f)
        {
            IsGameOver = true;_gameOverReason = GameEndReason.LowPeople;
            return;
        }
        if (people > 10.0f)
        {
            IsGameOver = true;_gameOverReason = GameEndReason.HighPeople;
            return;
        }
        if (money < 0.0f)
        {
            IsGameOver = true;_gameOverReason = GameEndReason.LowMoney;
            return;
        }
        if (money > 10.0f)
        {
            IsGameOver = true;_gameOverReason = GameEndReason.HighMoney;
            return;
        }
        if (security < 0.0f)
        {
            IsGameOver = true;_gameOverReason = GameEndReason.LowSecurity;
            return;
        }
        if (security > 10.0f)
        {
            IsGameOver = true;_gameOverReason = GameEndReason.HighSecurity;
            return;
        }
    }
    
    public enum GameEndReason
    {
        LowFaith, HighFaith,
        LowPeople, HighPeople,
        LowMoney, HighMoney,
        LowSecurity, HighSecurity

    }
}
