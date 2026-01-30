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

    public event Action<int> InterviewPopped;

    public event Action Changed;

    public InterviewTracker()
    {
    }

    private void sortInterviewDates()
    {
        _interviewDates.Sort((a, b) =>
        {
            if (a.Day != b.Day)
            {
                return a.Day.CompareTo(b.Day);
            }
            return a.Hour.CompareTo(b.Hour);
        });
        Changed?.Invoke();
    }

    public bool ContainsInterviewAt(int day, int hour)
    {
        foreach (var interviewDate in _interviewDates)
        {
            if (interviewDate.Day == day && interviewDate.Hour == hour)
            {
                return true;
            }
        }
        return false;
    }

    public InterviewDate? PeekNextInterviewDate()
    {
        if (_interviewDates.Count == 0)
        {
            return null;
        }
        return _interviewDates[0];
    }

    public bool TryAddInterviewDate(InterviewDate interviewDate)
    {
        Debug.Log($"Trying to add interview date: Day {interviewDate.Day}, Hour {interviewDate.Hour}");
        _interviewDates.Add(interviewDate);
        sortInterviewDates();
        return true;
    }

    public void NotifyTimeChanged(int day, int hour)
    {
        for (int i = _interviewDates.Count - 1; i >= 0; i--)
        {
            var interviewDate = _interviewDates[i];
            if (interviewDate.Day == day &&
                interviewDate.Hour == hour)
            {
                _interviewDates.RemoveAt(i);
                InterviewPopped?.Invoke(interviewDate.Lvl);
                Changed?.Invoke();
                sortInterviewDates();
            }
        }
    }
}
