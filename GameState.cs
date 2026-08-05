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
