# JHS

Unity game to simulate the exciting adventure of job hunting. 

It's my first forray into game dev, so I wanted to build something a little bit simple.

The main entry point of the program is in GameInstaller.cs

It’s essentially a layered architecture with a light MV* flavor:

Domain = state/data (TimeDateTracker, InterviewTracker)
Systems = application/business logic (ApplyForJobSystem, ConfirmInterviewSystem, InterviewSystem)
Presentation = UI/controllers (PopupCalendarController, HUD views)


completed:

upcoming interview added

calendar doesnt let you increment/decrement days

Gray out buttons that we already have an interview in

Passing the interview date doesnt update the main UI

We need to add text showing passed interviews

basic animation when applying to job

We need to forward time when undergoing an interview

todo:

Switch to calculating application results at end of day

We need an event scroll

We need to disable buttons when animation is playing

We need to figure out a fun minigame for the actual interviews

Lastly when we win we need a flow chart showing how many applications etc...