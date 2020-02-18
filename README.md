# Donuts

This repository contains sample patterns for solving various cross cutting concerns using Entity Framework
Core.

## Auditing

Tracking who created or edited an entry and when can be solved using middleware that gets invoked before
`SaveChanges` or `SaveChangesAsync` is called on the context and by leveraging the change tracker. See
[here](audit-auto-hydrate) for a minimal CRUD sample of this.

## Event dispatch

This is hardly original (practically every CQRS/DDD toy sample you'll find online does this these days)
but a simple means of achieving domain event dispatch is to:

* Implement an interface or inherit from an abstract class (`IDomainEvent` or `DomainEvent`, respectively)
* Create an `IAfterSaveChangesHandler` that pulls from the change tracker all instances of classes that
implement the interface or inherit from the base class
* Inject `IMediator` into the aforementioned handler, enumerate the set and publish an `INotification` on
each iteration
