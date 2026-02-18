public sealed class RestSystem
{
    private readonly CurrentTimeDate timeDateTracker;

    public RestSystem(CurrentTimeDate timeDateTracker)
    {
        this.timeDateTracker = timeDateTracker;
    }

    public void Rest()
    {
        timeDateTracker.AdvanceTime();
    }
}
