using UnityEngine;
using System;
using System.Collections.Generic;

public sealed class ApplicationTracker
{
    public event Action Changed;
    public event Action InterviewRecorded;
    private readonly List<Application> ongoingList = new List<Application>();
    private int lastApplicationIndexProcessed = 0;

    public enum ApplicationType
    {
        ResumeSubmission,
        RecruiterScreening,
        LevelOne,
        LevelTwo,
        LevelThree
    }
    private enum ApplicationStatus
    {
        Ongoing,
        Passed,
        Failed
    }

    struct Application
    {
        public ApplicationType Type;
        public ApplicationStatus Status;
    }

    public void RecordResumeSubmission()
    {
        ongoingList.Add(new Application { Type = ApplicationType.ResumeSubmission, Status = ApplicationStatus.Ongoing });
        Changed?.Invoke();
    }

    private Application CreateOngoingInterview(int level)
    {
        switch (level)
        {
            case 1:
                return new Application { Type = ApplicationType.RecruiterScreening, Status = ApplicationStatus.Ongoing};
            case 2:
                return new Application { Type = ApplicationType.LevelOne, Status = ApplicationStatus.Ongoing};
            case 3:
                return new Application { Type = ApplicationType.LevelTwo, Status = ApplicationStatus.Ongoing};
            case 4:
                return new Application { Type = ApplicationType.LevelThree, Status = ApplicationStatus.Ongoing};
            default:
                Debug.LogWarning($"Unknown interview level {level}. Using RecruiterScreening.");
                return new Application { Type = ApplicationType.RecruiterScreening, Status = ApplicationStatus.Ongoing};
        }
    }

    public void RecordInterviewEvent(int level) 
    {
        ongoingList.Add(CreateOngoingInterview(level));
    }

    public void Bind(ScheduledInterviews interviewTracker)
    {
        interviewTracker.InterviewPopped += OnInterviewPopped;
    }

    public bool DoneProcessingAllOngoingApplications()
    {
        return lastApplicationIndexProcessed >= ongoingList.Count;
    }

    public bool TryGetNextOngoingApplicationType(out ApplicationType type)
    {
        if (lastApplicationIndexProcessed >= ongoingList.Count)
        {
            Debug.LogWarning("No ongoing applications to process.");
            type = default;
            return false;
        }

        var app = ongoingList[lastApplicationIndexProcessed];
        if (app.Status != ApplicationStatus.Ongoing)
        {
            Debug.LogWarning("The application to process is not ongoing.");
            type = default;
            return false;
        }

        type = app.Type;
        return true;
    }

    public void ApplyNextOngoingResult(bool passed)
    {
        if (lastApplicationIndexProcessed >= ongoingList.Count)
        {
            Debug.LogWarning("No ongoing applications to process.");
            return;
        }

        var app = ongoingList[lastApplicationIndexProcessed];
        if (app.Status != ApplicationStatus.Ongoing)
        {
            Debug.LogWarning("The application to process is not ongoing.");
            return;
        }

        app.Status = passed ? ApplicationStatus.Passed : ApplicationStatus.Failed;
        ongoingList[lastApplicationIndexProcessed] = app;

        lastApplicationIndexProcessed++;
        Changed?.Invoke();
    }

    private void OnInterviewPopped(int lvl)
    {
        ApplicationType appType = lvl switch
        {
            1 => ApplicationType.LevelOne,
            2 => ApplicationType.LevelTwo,
            3 => ApplicationType.LevelThree,
            _ => throw new ArgumentOutOfRangeException(nameof(lvl), "Invalid interview level")
        };

        ongoingList.Add(new Application { Type = appType, Status = ApplicationStatus.Ongoing });
        Changed?.Invoke();
    }

    public int TotalApplications() { return ongoingList.Count; }

    private int ApplicationsQuery(ApplicationType type, ApplicationStatus status)
    {
        int count = 0;
        foreach (var app in ongoingList)
        {
            if (app.Type == type && app.Status == status)
            {
                count++;
            }
        }
        return count;
    }

    public int TotalOngoingResumeSubmissions()
    {
        return ApplicationsQuery(ApplicationType.ResumeSubmission, ApplicationStatus.Ongoing);
    }

    public int TotalPassedResumeSubmissions()
    {
        return ApplicationsQuery(ApplicationType.ResumeSubmission, ApplicationStatus.Passed);
    }

    public int TotalFailedResumeSubmissions()
    {
        return ApplicationsQuery(ApplicationType.ResumeSubmission, ApplicationStatus.Failed);
    }

    public int TotalOngoingRecruiterScreenings()
    {
        return ApplicationsQuery(ApplicationType.RecruiterScreening, ApplicationStatus.Ongoing);
    }
    public int TotalPassedRecruiterScreenings()
    {
        return ApplicationsQuery(ApplicationType.RecruiterScreening, ApplicationStatus.Passed);
    }
    public int TotalFailedRecruiterScreenings()
    {
        return ApplicationsQuery(ApplicationType.RecruiterScreening, ApplicationStatus.Failed);
    }

    public int TotalOngoingLvlOneInterviews() 
    { 
        return ApplicationsQuery(ApplicationType.LevelOne, ApplicationStatus.Ongoing); 
    }
    public int TotalPassedLvlOneInterviews() 
    { 
        return ApplicationsQuery(ApplicationType.LevelOne, ApplicationStatus.Passed); 
    }
    public int TotalFailedLvlOneInterviews() 
    { 
        return ApplicationsQuery(ApplicationType.LevelOne, ApplicationStatus.Failed); 
    }
    public int TotalOngoingLvlTwoInterviews() 
    { 
        return ApplicationsQuery(ApplicationType.LevelTwo, ApplicationStatus.Ongoing); 
    }
    public int TotalPassedLvlTwoInterviews()
    { 
        return ApplicationsQuery(ApplicationType.LevelTwo, ApplicationStatus.Passed); 
    }
    public int TotalFailedLvlTwoInterviews()
    { 
        return ApplicationsQuery(ApplicationType.LevelTwo, ApplicationStatus.Failed); 
    }
    public int TotalOngoingLvlThreeInterviews()
    { 
        return ApplicationsQuery(ApplicationType.LevelThree, ApplicationStatus.Ongoing); 
    }
    public int TotalPassedLvlThreeInterviews()
    { 
        return ApplicationsQuery(ApplicationType.LevelThree, ApplicationStatus.Passed); 
    }
    public int TotalFailedLvlThreeInterviews()
    { 
        return ApplicationsQuery(ApplicationType.LevelThree, ApplicationStatus.Failed); 
    }

}
