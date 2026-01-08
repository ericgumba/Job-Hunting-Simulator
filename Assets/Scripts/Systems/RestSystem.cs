public sealed class RestSystem
{
    private readonly TimeDateTracker timeDateTracker;

    public RestSystem(TimeDateTracker timeDateTracker)
    {
        this.timeDateTracker = timeDateTracker;
    }

    public void Rest()
    {
        timeDateTracker.AdvanceTime();
    }
}
