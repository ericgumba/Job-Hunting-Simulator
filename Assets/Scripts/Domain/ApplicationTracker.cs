using UnityEngine;
using System;
using System.Collections.Generic;

public sealed class ApplicationTracker
{
    public event Action Changed;
    private readonly List<Application> ongoingList = new List<Application>();
    private int lastApplicationIndexProcessed = 0;

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
  
    public void RecordInterviewEvent(ApplicationType type) 
    {
        Debug.Log($"ERICGUMBA Recording interview event for type {type}");
        ongoingList.Add(new Application { Type = type, Status = ApplicationStatus.Ongoing });
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

    public void PassFailApplication(bool passed)
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

    public int TotalOngoingFirstTechnicalInterviews() 
    { 
        return ApplicationsQuery(ApplicationType.FirstTechnical, ApplicationStatus.Ongoing); 
    }
    public int TotalPassedFirstTechnicalInterviews() 
    { 
        return ApplicationsQuery(ApplicationType.FirstTechnical, ApplicationStatus.Passed); 
    }
    public int TotalFailedFirstTechnicalInterviews() 
    { 
        return ApplicationsQuery(ApplicationType.FirstTechnical, ApplicationStatus.Failed); 
    }
    public int TotalOngoingSecondTechnicalInterviews() 
    { 
        return ApplicationsQuery(ApplicationType.SecondTechnical, ApplicationStatus.Ongoing); 
    }
    public int TotalPassedSecondTechnicalInterviews()
    { 
        return ApplicationsQuery(ApplicationType.SecondTechnical, ApplicationStatus.Passed); 
    }
    public int TotalFailedSecondTechnicalInterviews()
    { 
        return ApplicationsQuery(ApplicationType.SecondTechnical, ApplicationStatus.Failed); 
    }
    public int TotalOngoingHiringManagerInterviews()
    { 
        return ApplicationsQuery(ApplicationType.HiringManager, ApplicationStatus.Ongoing); 
    }
    public int TotalPassedHiringManagerInterviews()
    { 
        return ApplicationsQuery(ApplicationType.HiringManager, ApplicationStatus.Passed); 
    }
    public int TotalFailedHiringManagerInterviews()
    { 
        return ApplicationsQuery(ApplicationType.HiringManager, ApplicationStatus.Failed); 
    }

}
