using UnityEngine;
using System;

public sealed class ApplicationTracker
{
    public int TotalInterviews { get; private set; }
    public int TotalRejections { get; private set; }

    public int TotalApplications => TotalInterviews + TotalRejections;
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
}
