using OBA;

public class GameState
{
    public float faith;
    public float people;
    public float money;
    public float security;
    public bool IsGameOver { get; private set; }
    public GameEndReason gameEndReason { get; private set; }
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

    
    
    
    private void CheckIsGameOver()
    {
        if(faith < 0.0f)
        {
            IsGameOver=true;
            gameEndReason = GameEndReason.LowFaith;
            return;
        }
        if (faith > 10.0f)
        {
            IsGameOver = true; gameEndReason = GameEndReason.HighFaith;
            return;
        }
        if (people < 0.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.LowPeople;
            return;
        }
        if (people > 10.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.HighPeople;
            return;
        }
        if (money < 0.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.LowMoney;
            return;
        }
        if (money > 10.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.HighMoney;
            return;
        }
        if (security < 0.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.LowSecurity;
            return;
        }
        if (security > 10.0f)
        {
            IsGameOver = true;gameEndReason = GameEndReason.HighSecurity;
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
