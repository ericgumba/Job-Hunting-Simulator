using UnityEngine;
using System;

public sealed class CurrentTimeDate
{
    public int Hours { get; private set;}
    public int Days {get; private set;}

    // calculate time in AM/PM format
    public string CurrentTime => 
        $"{(Hours % 12 == 0 ? 12 : Hours % 12)} {(Hours < 12 ? "AM" : "PM")}";
    
    public event Action Changed;
    public event Action EndOfDayReached;

    public CurrentTimeDate(int startHour = 8, int startDay = 0)
    {
        Hours = startHour;
        Days = startDay;
    }

    public void AdvanceTime()
    {
        Hours++;
        if (Hours >= 24)
        {
            EndOfDayReached?.Invoke();
            Hours = 8;
            Days++;
        }
        Changed?.Invoke();
    }
}