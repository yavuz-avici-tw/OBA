using OBA;

public sealed class GameState
{
    public float Faith { get; private set; }
    public float People { get; private set; }
    public float Money { get; private set; }
    public float Security { get; private set; }
    public bool IsGameOver { get; private set; }
    public GameEndReason gameEndReason { get; private set; }
    // acitve encounter set etme kısmı tehlikeli GameState'in internalına güvendim
    public Encounter? ActiveEncounter { get; private set; }

    public int dayProgress { get; private set; }
    private int _winDays = 365 * 5;

    public GameState(Encounter activeEncounter, float faith = 5.0F, float people = 5.0f, float money = 5.0f, float security = 5.0f)
    {
        this.Faith = faith;
        this.People = people;
        this.Money = money;
        this.Security = security;
        ActiveEncounter = activeEncounter;
        IsGameOver = false;
        dayProgress = 0;
    }

    internal void SetState(float faith, float people, float money, float security, Encounter? activeEncounter, int dayProgress=0)
    {
        this.Faith = faith;
        this.People = people;
        this.Money = money;
        this.Security = security;
        ActiveEncounter = activeEncounter;
        this.dayProgress = dayProgress;
        CheckIsGameOver();
    }

    
    
    
    private void CheckIsGameOver()
    {
        if(Faith < 0.0f)
        {
            IsGameOver=true;
            gameEndReason = GameEndReason.LowFaith;
            return;
        }
        if (Faith > 10.0f)
        {
            IsGameOver = true; gameEndReason = GameEndReason.HighFaith;
            return;
        }
        if (People < 0.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.LowPeople;
            return;
        }
        if (People > 10.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.HighPeople;
            return;
        }
        if (Money < 0.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.LowMoney;
            return;
        }
        if (Money > 10.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.HighMoney;
            return;
        }
        if (Security < 0.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.LowSecurity;
            return;
        }
        if (Security > 10.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.HighSecurity;
            return;
        }
        if (dayProgress > _winDays)
        {
            IsGameOver = true;gameEndReason = GameEndReason.Win;
            return;
        }
    }
    
    public enum GameEndReason
    {
        LowFaith, HighFaith,
        LowPeople, HighPeople,
        LowMoney, HighMoney,
        LowSecurity, HighSecurity,
        Win
    }
}
