# TWINS

This game is a college project and the result of a 3 sprint long project working with other 3 devs. The porpuse was to we learnt how to manage a software development team with an agile philosophy for *Software Development Process* subject. Also, we were in the process of learning about design patterns, refactoring and unit testing for the *Software Design* college subject.

## What did I learn
- How to work with an agile philosophy, using flexible, iterative and incremental techniques:
  - 2 week sprints, from Scrum
  - Team-visible Kanban board for tracking user stories/work units
  - Behavior Driven Development (BDD), where acceptance tests led the development and functionality definition
  - Flexible workflows
  - Working with estimates:
    - Making and refine estimates based on estimates differences
    - Adecuating the amount of sprint effort relative to the team capacity
    - Tracking sprint progress with metrics from burndown, cumulattive flow diagram, cycle times and delay time, etc
  - Inmerse the client into the dev process:
    - Sprint reviews with the "client", managing the modifications for the presented product
    - Negotiate functionalities scope with the client rather than increasing time or resources
  - Weekly meetings to review and improve team process performance
  - Constant interaction and comunication with the dev team

<br>

- First time applying the basics of Clean Code, refactoring code, good naming, etc
- The importance and value of programming patterns (mainly GoF book and Game Programming Patterns by Robert Nystrom):
  - Using *Singleton* for having global access **and** a single instance to manager-like clases (such as our Mediator)
  - *Mediator* pattern as a way to centralize communication between different systems, decoupling them between each other
  - The usefulness of the *Template* to allow the definition of a high level set of steps that are later implemented by hook methods in their subclasses
  - *Pub/Sub* as a way of decoupling an event sender and its listener, with the relevance of knowning only that the event has occurred. We had to implement this patterns only knowning the concept and intention behind it, since we couldn't find examples with out having to couple sender and listener by using delegates or C# events (same goes for Unity events)

<br>
 
- First steps into the Unity test runner and testing environment with NUnit:
  - Arrange, Act & Assert, and other basic conventions
  - How to use assemblies references for testing
  - How to (and the importance of) structure the project to be as testeable as possible

<br>

- How to manage a team of other 3 devs in a project with longer duration and scope than a game jam (48h)

@marcoshy99 @Xiphereal and Alberto
