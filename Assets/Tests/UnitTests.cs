using NUnit.Framework;
using System;
using System.Collections.Generic;

public class ScheduledInterviewsTests
{
    [Test]
    public void TryAddInterviewDateAcceptsAnyDay()
    {
        var interviewTracker = new ScheduledInterviews();

        var sameDay = new ScheduledInterviews.InterviewDate(day: 2, hour: 10);
        var pastDay = new ScheduledInterviews.InterviewDate(day: 1, hour: 10);

        Assert.IsTrue(interviewTracker.TryAddInterviewDate(sameDay));
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(pastDay));
    }

    [Test]
    public void TryAddInterviewDateAcceptsFutureDay()
    {
        var interviewTracker = new ScheduledInterviews();

        var futureDay = new ScheduledInterviews.InterviewDate(day: 1, hour: 9);

        Assert.IsTrue(interviewTracker.TryAddInterviewDate(futureDay));
    }

    [Test]
    public void InterviewPoppedFiresWhenTimeMatchesAndRemovesEntry()
    {
        var interviewTracker = new ScheduledInterviews();
        int fired = 0;
        int lvl_result = 0;

        interviewTracker.InterviewPopped += (int level) => {fired++; lvl_result = level;};
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new ScheduledInterviews.InterviewDate(day: 1, hour: 9)));

        interviewTracker.NotifyTimeChanged(day: 1, hour: 8);

        Assert.AreEqual(0, fired);

        interviewTracker.NotifyTimeChanged(day: 1, hour: 9);

        Assert.AreEqual(1, fired);
        Assert.AreEqual(lvl_result, 1);
    }

    [Test]
    public void InterviewPoppedFiresForEachMatchingInterview()
    {
        var interviewTracker = new ScheduledInterviews();
        int fired = 0;

        interviewTracker.InterviewPopped += (int _) => fired++;
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new ScheduledInterviews.InterviewDate(day: 1, hour: 8)));
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new ScheduledInterviews.InterviewDate(day: 1, hour: 8)));

        interviewTracker.NotifyTimeChanged(day: 1, hour: 8);

        Assert.AreEqual(2, fired);
    }

    [Test]
    public void EndOfDayReached()
    {
        var timeDateTracker = new CurrentTimeDate(startHour: 22, startDay: 0);
        int fired = 0;
        timeDateTracker.EndOfDayReached += () => fired++;

        timeDateTracker.AdvanceTime(); // Advances to 24 -> should trigger EndOfDayReached
        Assert.AreEqual(0, fired);

        timeDateTracker.AdvanceTime(); // Advances to 24 -> should trigger EndOfDayReached
        Assert.AreEqual(1, fired);
    }

    [Test]
    public void InterviewSystem_TryPerformInterview_RecordsInterviewEvent()
    {
        var interviewTracker = new ScheduledInterviews();
        var applicationTracker = new ApplicationTracker();
        var timeDateTracker = new CurrentTimeDate(startHour: 9, startDay: 0);
        var playerStats = new PlayerStatistics();

        var interviewSystem = new InterviewSystem(
            interviewTracker,
            applicationTracker,
            timeDateTracker,
            playerStats);

        var interviewDate = new ScheduledInterviews.InterviewDate(day: 0, hour: 10, lvl: 1);
        interviewTracker.TryAddInterviewDate(interviewDate);

        timeDateTracker.AdvanceTime(); // Advance to hour 10

        Assert.AreEqual(1, applicationTracker.TotalOngoingRecruiterScreenings());
    }
}
