### LiveScoreLibrary

This is a technical test for Sportradar company, and the target is create a LiveScore to show any game score, also  handling game status (live, finish) and record a summary of all matches

# Project structure

We have a solution with the Library. That solution was implemented using JetBrains Rider. We have two project:

 - **LiveScoreLib**: Project with complete library
 - **UnitTest**: Project with all unittest about library
 
 ## LiveScoreLib
 Project develop using TDD and CleanCode, applying SOLID Principles. For implementation I use library MediatR, that library is a simple mediator implemented in .Net to process messages without any dependencies based on request/response.
 
 I implment extension for DI, if  you want to use my library in a project with DI you just initialize injections using that extension in your IOC.
 
 To use you must get a instance of mediatR service and send differents request:
 
 ```cs
 var mediator = serviceProvider.GetRequiredService<IMediator>();
 ...
 var result = await _mediator.Send(new CreateMatch("Team1", "Team2"));
 var result = await _mediator.Send(new FinishGame());
 var result = await _mediator.Send(new UpdateScore(1, 3));
 var result = await _mediator.Send(new GetSummary());
```

Also I use a pattern to dont throw Exception, instead of I return Result object with status result of call and exception inside it if result fails.

### Assumptions made

Only can be a one game in LiveScore, if you try to start other game you must finish first the current game

## UnitTest

There are a class test for each "part" of library to testing, I have 100% coverage. I use xunit test using Fact, Theory with inlines data and also ClassData for summary testing.


Thanks for all and for the chance!
