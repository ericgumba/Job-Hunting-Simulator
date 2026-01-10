using UnityEngine;
using System;

public sealed class ApplicationTracker
{
    public int TotalInterviews { get; private set; }
    public int TotalRejections { get; private set; }

    public int OngoingApplications { get; private set; }

    public int TotalApplications => TotalInterviews + TotalRejections;

    public int TotalFailedLvlOneInterviews { get; set; }
    public int TotalPassedLvlOneInterviews { get; set; }

    public int TotalFailedLvlTwoInterviews { get; set; }
    public int TotalPassedLvlTwoInterviews { get; set; }

    public int TotalFailedLvlThreeInterviews { get; set; }
    public int TotalPassedLvlThreeInterviews { get; set; }

    public int TotalFailedLvlFourInterviews { get; set; }
    public int TotalPassedLvlFourInterviews { get; set; }
    public event Action Changed;
    public event Action InterviewRecorded;

    public void RecordInterview() {
        TotalInterviews++;
        InterviewRecorded?.Invoke();
        Changed?.Invoke();
    }

    public void RecordRejection(){
        TotalRejections++;
        Changed?.Invoke();
    }

    public void RecordInterviewEvent(bool passed, int level)
    {
        switch (level)
        {
            case 1:
                if (passed)
                    TotalPassedLvlOneInterviews++;
                else
                    TotalFailedLvlOneInterviews++;
                break;
            case 2:
                if (passed)
                    TotalPassedLvlTwoInterviews++;
                else
                    TotalFailedLvlTwoInterviews++;
                break;
            case 3:
                if (passed)
                    TotalPassedLvlThreeInterviews++;
                else
                    TotalFailedLvlThreeInterviews++;
                break;
            case 4:
                if (passed)
                    TotalPassedLvlFourInterviews++;
                else
                    TotalFailedLvlFourInterviews++;
                break;
            default:
                Debug.LogError($"Invalid interview level: {level}");
                return;
        }
        Changed?.Invoke();
    }

    
}
