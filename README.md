# JHS

Unity game to simulate the exciting adventure of job hunting. 

It's my first forray into game dev, so I wanted to build something a little bit simple.

The main entry point of the program is in GameInstaller.cs

It’s essentially a layered architecture with a light MV* flavor:

Domain = state/data (TimeDateTracker, InterviewTracker)
Systems = application/business logic (ApplyForJobSystem, ConfirmInterviewSystem, InterviewSystem)
Presentation = UI/controllers (PopupCalendarController, HUD views)

