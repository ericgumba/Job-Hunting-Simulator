using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class InterviewTracker
{
    public struct InterviewDate
    {
        public int Day;
        public int Hour;
        public int Lvl;

        public InterviewDate(int day, int hour, int lvl = 1)
        {
            Day = day;
            Hour = hour;
            Lvl = lvl;
        }
    }

    private readonly List<InterviewDate> _interviewDates = new List<InterviewDate>();
    private readonly TimeDateTracker _timeDateTracker;

    public TimeDateTracker TimeDateTracker => _timeDateTracker;

    public event Action InterviewPopped;

    public InterviewTracker(TimeDateTracker timeDateTracker)
    {
        _timeDateTracker = timeDateTracker;
        _timeDateTracker.Changed += OnTimeChanged;
    }

    public bool TryAddInterviewDate(InterviewDate interviewDate)
    {
        if (interviewDate.Day <= _timeDateTracker.Days)
        {
            return false;
        }

        _interviewDates.Add(interviewDate);
        return true;
    }

    private void OnTimeChanged()
    {
        for (int i = _interviewDates.Count - 1; i >= 0; i--)
        {
            var interviewDate = _interviewDates[i];
            if (interviewDate.Day == _timeDateTracker.Days &&
                interviewDate.Hour == _timeDateTracker.Hours)
            {
                _interviewDates.RemoveAt(i);
                InterviewPopped?.Invoke();
            }
        }
    }
}
