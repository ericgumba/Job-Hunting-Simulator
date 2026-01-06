using NUnit.Framework;
using System;
using System.Collections.Generic;

public class InterviewTrackerTests
{
    [Test]
    public void TryAddInterviewDateRejectsSameOrPastDay()
    {
        var timeDateTracker = new TimeDateTracker(startHour: 8, startDay: 2);
        var interviewTracker = new InterviewTracker(timeDateTracker);

        var sameDay = new InterviewTracker.InterviewDate(day: 2, hour: 10);
        var pastDay = new InterviewTracker.InterviewDate(day: 1, hour: 10);

        Assert.IsFalse(interviewTracker.TryAddInterviewDate(sameDay));
        Assert.IsFalse(interviewTracker.TryAddInterviewDate(pastDay));
    }

    [Test]
    public void TryAddInterviewDateAcceptsFutureDay()
    {
        var timeDateTracker = new TimeDateTracker(startHour: 8, startDay: 0);
        var interviewTracker = new InterviewTracker(timeDateTracker);

        var futureDay = new InterviewTracker.InterviewDate(day: 1, hour: 9);

        Assert.IsTrue(interviewTracker.TryAddInterviewDate(futureDay));
    }

    [Test]
    public void InterviewPoppedFiresWhenTimeMatchesAndRemovesEntry()
    {
        var timeDateTracker = new TimeDateTracker(startHour: 8, startDay: 0);
        var interviewTracker = new InterviewTracker(timeDateTracker);
        int fired = 0;

        interviewTracker.InterviewPopped += () => fired++;
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new InterviewTracker.InterviewDate(day: 1, hour: 9)));

        for (int i = 0; i < 16; i++)
        {
            timeDateTracker.AdvanceTime();
        }

        Assert.AreEqual(0, fired);

        timeDateTracker.AdvanceTime();

        Assert.AreEqual(1, fired);
    }

    [Test]
    public void InterviewPoppedFiresForEachMatchingInterview()
    {
        var timeDateTracker = new TimeDateTracker(startHour: 8, startDay: 0);
        var interviewTracker = new InterviewTracker(timeDateTracker);
        int fired = 0;

        interviewTracker.InterviewPopped += () => fired++;
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new InterviewTracker.InterviewDate(day: 1, hour: 8)));
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new InterviewTracker.InterviewDate(day: 1, hour: 8)));

        for (int i = 0; i < 16; i++)
        {
            timeDateTracker.AdvanceTime();
        }

        Assert.AreEqual(2, fired);
    }
}
