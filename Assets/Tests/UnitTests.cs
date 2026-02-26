using NUnit.Framework;
using System;
using System.Collections.Generic;

public class ScheduledInterviewsTests
{
    [Test]
    public void ApplyForJobSystem_Apply_RecordsResumeSubmissionAndAdvancesTime()
    {
        var applicationTracker = new ApplicationTracker();
        var currentTime = new CurrentTimeDate(startHour: 9, startDay: 0);
        var applySystem = new ApplyForJobSystem(applicationTracker, currentTime);

        applySystem.Apply();

        Assert.AreEqual(1, applicationTracker.TotalOngoingResumeSubmissions());
        Assert.AreEqual(10, currentTime.Hours);
    }

    [Test]
    public void TryAddInterviewDateAcceptsAnyDay()
    {
        var interviewTracker = new ScheduledInterviews();

        var sameDay = new ScheduledInterviews.InterviewDate(day: 2, hour: 10, type: ApplicationType.RecruiterScreening);
        var pastDay = new ScheduledInterviews.InterviewDate(day: 1, hour: 10, type: ApplicationType.RecruiterScreening);

        Assert.IsTrue(interviewTracker.TryAddInterviewDate(sameDay));
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(pastDay));
    }

    [Test]
    public void TryAddInterviewDateAcceptsFutureDay()
    {
        var interviewTracker = new ScheduledInterviews();

        var futureDay = new ScheduledInterviews.InterviewDate(day: 1, hour: 9, type: ApplicationType.RecruiterScreening);

        Assert.IsTrue(interviewTracker.TryAddInterviewDate(futureDay));
    }

    [Test]
    public void ResultsAreShownAtEndOfDay()
    {
        EndOfDaySystem endOfDaySystem = null;
        var applicationTracker = new ApplicationTracker();
        var currentTimeDate = new CurrentTimeDate(startHour: 22, startDay: 0);
        var playerStats = new PlayerStatistics();
        endOfDaySystem = new EndOfDaySystem(
            currentTimeDate,
            applicationTracker,
            playerStats);
        
        applicationTracker.RecordResumeSubmission();
        applicationTracker.RecordResumeSubmission();
        applicationTracker.RecordResumeSubmission();
        applicationTracker.RecordResumeSubmission();
        applicationTracker.RecordResumeSubmission();
        applicationTracker.RecordInterviewEvent(ApplicationType.RecruiterScreening);
        applicationTracker.RecordInterviewEvent(ApplicationType.RecruiterScreening);
        applicationTracker.RecordInterviewEvent(ApplicationType.RecruiterScreening);
        applicationTracker.RecordInterviewEvent(ApplicationType.RecruiterScreening);
        applicationTracker.RecordInterviewEvent(ApplicationType.FirstTechnical);
        applicationTracker.RecordInterviewEvent(ApplicationType.FirstTechnical);
        applicationTracker.RecordInterviewEvent(ApplicationType.FirstTechnical);
        applicationTracker.RecordInterviewEvent(ApplicationType.SecondTechnical);
        applicationTracker.RecordInterviewEvent(ApplicationType.SecondTechnical);
        applicationTracker.RecordInterviewEvent(ApplicationType.HiringManager);
        Assert.AreEqual(5, applicationTracker.TotalOngoingResumeSubmissions());
        Assert.AreEqual(4, applicationTracker.TotalOngoingRecruiterScreenings());
        Assert.AreEqual(3, applicationTracker.TotalOngoingFirstTechnicalInterviews());
        Assert.AreEqual(2, applicationTracker.TotalOngoingSecondTechnicalInterviews());
        Assert.AreEqual(1, applicationTracker.TotalOngoingHiringManagerInterviews());

        currentTimeDate.AdvanceTime(); // Advances to 23

        Assert.AreEqual(5, applicationTracker.TotalOngoingResumeSubmissions());
        Assert.AreEqual(4, applicationTracker.TotalOngoingRecruiterScreenings());
        Assert.AreEqual(3, applicationTracker.TotalOngoingFirstTechnicalInterviews());
        Assert.AreEqual(2, applicationTracker.TotalOngoingSecondTechnicalInterviews());
        Assert.AreEqual(1, applicationTracker.TotalOngoingHiringManagerInterviews());

        currentTimeDate.AdvanceTime(); // Advances to 24 -> should trigger EndOfDayReached
        Assert.AreEqual(0, applicationTracker.TotalOngoingResumeSubmissions());
        Assert.AreEqual(0, applicationTracker.TotalOngoingRecruiterScreenings());
        Assert.AreEqual(0, applicationTracker.TotalOngoingFirstTechnicalInterviews());
        Assert.AreEqual(0, applicationTracker.TotalOngoingSecondTechnicalInterviews());
        Assert.AreEqual(0, applicationTracker.TotalOngoingHiringManagerInterviews());

        Assert.AreEqual(5, applicationTracker.TotalPassedResumeSubmissions() + applicationTracker.TotalFailedResumeSubmissions());
        Assert.AreEqual(4, applicationTracker.TotalPassedRecruiterScreenings() + applicationTracker.TotalFailedRecruiterScreenings());
        Assert.AreEqual(3, applicationTracker.TotalPassedFirstTechnicalInterviews() + applicationTracker.TotalFailedFirstTechnicalInterviews());
        Assert.AreEqual(2, applicationTracker.TotalPassedSecondTechnicalInterviews() + applicationTracker.TotalFailedSecondTechnicalInterviews());
        Assert.AreEqual(1, applicationTracker.TotalPassedHiringManagerInterviews() + applicationTracker.TotalFailedHiringManagerInterviews());

    }

    [Test]
    public void InterviewPoppedFiresWhenTimeMatchesAndRemovesEntry()
    {
        var interviewTracker = new ScheduledInterviews();
        int fired = 0;
        ApplicationType type_result = ApplicationType.ResumeSubmission;

        interviewTracker.InterviewPopped += (ApplicationType type) => {fired++; type_result = type;};
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new ScheduledInterviews.InterviewDate(day: 1, hour: 9, type: ApplicationType.RecruiterScreening)));

        interviewTracker.NotifyTimeChanged(day: 1, hour: 8);

        Assert.AreEqual(0, fired);

        interviewTracker.NotifyTimeChanged(day: 1, hour: 9);

        Assert.AreEqual(1, fired);
        Assert.AreEqual(type_result, ApplicationType.RecruiterScreening);
    }

    [Test]
    public void InterviewPoppedFiresForEachMatchingInterview()
    {
        var interviewTracker = new ScheduledInterviews();
        int fired = 0;

        interviewTracker.InterviewPopped += (ApplicationType type) => fired++;
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new ScheduledInterviews.InterviewDate(day: 1, hour: 8, type: ApplicationType.RecruiterScreening)));
        Assert.IsTrue(interviewTracker.TryAddInterviewDate(new ScheduledInterviews.InterviewDate(day: 1, hour: 8, type: ApplicationType.RecruiterScreening)));

        interviewTracker.NotifyTimeChanged(day: 1, hour: 8);

        Assert.AreEqual(2, fired);
    }

    [Test]
    public void EndOfDayReached()
    {
        var currentTimeDate = new CurrentTimeDate(startHour: 22, startDay: 0);
        int fired = 0;
        currentTimeDate.EndOfDayReached += () => fired++;

        currentTimeDate.AdvanceTime(); // Advances to 23 
        Assert.AreEqual(0, fired);

        currentTimeDate.AdvanceTime(); // Advances to 24 -> should trigger EndOfDayReached
        Assert.AreEqual(1, fired);
    }

    [Test]
    public void InterviewSystem_TryPerformInterview_RecordsInterviewEvent()
    {
        var interviewTracker = new ScheduledInterviews();
        var applicationTracker = new ApplicationTracker();
        var currentTimeDate = new CurrentTimeDate(startHour: 9, startDay: 0);
        var playerStats = new PlayerStatistics();

        var interviewSystem = new InterviewSystem(
            interviewTracker,
            applicationTracker,
            currentTimeDate,
            playerStats);

        var interviewDate = new ScheduledInterviews.InterviewDate(day: 0, hour: 10, type: ApplicationType.RecruiterScreening);
        interviewTracker.TryAddInterviewDate(interviewDate);

        currentTimeDate.AdvanceTime(); // Advance to hour 10

        Assert.AreEqual(1, applicationTracker.TotalOngoingRecruiterScreenings());
    }
}
