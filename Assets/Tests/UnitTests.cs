using NUnit.Framework;
using System;
using System.Collections.Generic;

public class InterviewTrackerTests
{
    [Test]
    public void TryAddInterviewDateAcceptsAnyDay()
    {
        var interviewTracker = new InterviewTracker();

        var sameDay = new InterviewTracker.InterviewDate(day: 2, hour: 10);
        var pastDay = new InterviewTracker.InterviewDate(day: 1, hour: 10);

        Assert.IsTrue(interviewTracker.TryAddInterviewDate(sameDay));
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(pastDay));
    }

    [Test]
    public void TryAddInterviewDateAcceptsFutureDay()
    {
        var interviewTracker = new InterviewTracker();

        var futureDay = new InterviewTracker.InterviewDate(day: 1, hour: 9);

        Assert.IsTrue(interviewTracker.TryAddInterviewDate(futureDay));
    }

    [Test]
    public void InterviewPoppedFiresWhenTimeMatchesAndRemovesEntry()
    {
        var interviewTracker = new InterviewTracker();
        int fired = 0;

        interviewTracker.InterviewPopped += () => fired++;
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new InterviewTracker.InterviewDate(day: 1, hour: 9)));

        interviewTracker.NotifyTimeChanged(day: 1, hour: 8);

        Assert.AreEqual(0, fired);

        interviewTracker.NotifyTimeChanged(day: 1, hour: 9);

        Assert.AreEqual(1, fired);
    }

    [Test]
    public void InterviewPoppedFiresForEachMatchingInterview()
    {
        var interviewTracker = new InterviewTracker();
        int fired = 0;

        interviewTracker.InterviewPopped += () => fired++;
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new InterviewTracker.InterviewDate(day: 1, hour: 8)));
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new InterviewTracker.InterviewDate(day: 1, hour: 8)));

        interviewTracker.NotifyTimeChanged(day: 1, hour: 8);

        Assert.AreEqual(2, fired);
    }

    [Test]
    public void EndOfDayReached()
    {
        var timeDateTracker = new TimeDateTracker(startHour: 22, startDay: 0);
        int fired = 0;
        timeDateTracker.EndOfDayReached += () => fired++;

        timeDateTracker.AdvanceTime(); // Advances to 24 -> should trigger EndOfDayReached
        Assert.AreEqual(0, fired);

        timeDateTracker.AdvanceTime(); // Advances to 24 -> should trigger EndOfDayReached
        Assert.AreEqual(1, fired);
    }
}
