# Overview

Bifrost targets the line of business type of application development. This space has very often requirements that
are somewhat different than making other types of applications. Unlike creating a web page with content, line of business
applications has more advanced business logic and rules associated with it. In addition, most line of business applications
tend to live for a long time once they are being used by users. Rewrites are often seldom in this space, as it involves a lot of
work to capture existing features in a new way. This often means that one needs to think more about the maintainability
of the product. In addition to this, in a fast moving world, code needs to built in a way that allows for rapidly
adapting to new requirements. It truly can be a life/death situation for a company if the company is not able to adapt
to market changes, competitors or users wanting new features. Traditional techniques for building software has issues
related to this - by design. N-tier architecture tends to mix concerns and responsibilities and thus leading to
software that is hard to maintain. According [Fred Brooks](https://en.wikipedia.org/wiki/Fred_Brooks) and
["The Mythical Man-Month"](https://en.wikipedia.org/wiki/The_Mythical_Man-Month), 90% of the cost
related to a typical system arise in the maintenance phase. This means that we should aim towards building our systems
in a way that makes the maintenance phase as easy as possible.

The goal of Bifrost is to help make this better. By focusing on well established software patterns and practices,
and sticking to them without compromise, we believe this is possible. Bifrost embraces a set of practices described
in this article and tries to adhere to them fully.

## Decoupling & Microservices

At the heart of Bifrost sits the notion of decoupling. Making small focused lego pieces that can be assembled together
in any way one wants to. This is at the core of what is referred to as [Microservices](https://en.wikipedia.org/wiki/Microservices).
The ability to break up the software into smaller more digestable components that makes our software in fact much easier
to understand and maintain. When writing software in a decoupled manner, one gets the opportunity of composing it back
together however one sees fit. You could compose it back in one application running inside a single process, or you could
spread it across a cluster. It really is a deployment choice once the software is giving you this freedom.
When it is broken up you get the benefit of scaling each individual piece on its own, rather than scaling the monolith
equally across a number of machines. This gives a higher density, better resource utilization and ultimately better cost
control.

## SOLID

### Single Responsibility Principle

### Open / Closed Principle

### Liskow Substition Principle

### Interface Segregation Principle

### Dependency Inversion Principle

## Seperation of Concerns

## Cross Cutting Concerns

## Conventions over Configuration

For a team to deliver consistency in the codebase, one should aim for a recipe that makes it easy to the right thing and
hard to the wrong thing. Having conventions to govern the recipe forces the team to deliver in one way. Bifrost is highly
focused around the concept of conventions in place for things, rather than having to configure thing. The conventions instead
are configurable. Since things are discovered and one does not need to configure everything, we adhere more easier to the
Open / Closed principle as mentioned earlier. Meaning that we don't have to open up code to get new things in place, it will
be discovered by how the conventions are and configured.

The simplest example of a convention in play in Bifrost is during initialization, Bifrost will configure whatever [IOC container](https://en.wikipedia.org/wiki/Inversion_of_control)
you have hooked with conventions. One default convention plays a part here saying that an interface named ``IFoo``will be bound to ``Foo``
as long as they both sit in the same namespace. You'll see this throughout Bifrost internally as well, for instance ``ICommandCoordinator`` is 
bound to ``CommandCoordinator``.

Other good examples of conventions in Bifrost is in the Web frontend where one can specify a particular *View* to be loaded and it will
automatically look for a *ViewModel* with the same name but instead of *.html* as extension, it looks for *.js*.

The conventions at play are described throughout the documentation when it is relevant.

### Discovery

In order for all the conventions described above to work, Bifrost is heavily relying on different types of discovering mechanisms.
For the C# code the discovery is all about types. It relies on being able to discover concrete types, but also implementations of interfaces.
Through this it can find the things it needs. You can read more about the type discovery mechanism [here](../Backend/type_discovery.md).

## Automated testing

## CQRS

## Model View View Model

MVVM is a variation of [Martin Fowler's](https://en.wikipedia.org/wiki/Martin_Fowler) [Presentation Model](http://martinfowler.com/eaaDev/PresentationModel.html).
Its the most commonly used pattern in XAML based platforms such as WPF, Silverlight, UWP, Xamarin and more.

### Model

The model refers to state being used typically originating from a server component such as a database.
It is often referred to as the domain model. In the context of Bifrost, this would typically be the [ReadModel](read_model.md).

### View

The view represents the structure and layout on the screen. It observes the ViewModel.

### ViewModel

The ViewModel holds the state; the model and also exposes behaviors that the view can utilize.
In XAML the behaviors is represented by a [command](https://msdn.microsoft.com/en-us/library/system.windows.input.icommand(v=vs.110).aspx),
something that wraps the behavior and provides a point for execution but also the ability to check wether or not
it can execute. This proves very handy when you want to validate things and not be able to execute unless one is valid or is authorized.
Bifrost has the concept of commands, these are slightly different however. In fact, commands in Bifrost is a part of the domain.
It is the thing that describes the users intent. You can read more about them [here](../Backend/Commands/introduction.md).
In the Bifrost JavaScript frontend however, the type of properties found with the XAML platforms
can also be found here. Read more about the frontend commands [here](../Frontend/JavaScript/commands.md).

### Binding

Part of connecting the View with the ViewModel and enabling it to observe it is the concept of binding.
Binding sits between the View and the ViewModel and can with some implementations even understand when values change
and automatically react to the change. In XAML, this is accomplished through implementing interfaces like [INotifyPropertyChanged](https://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
and [INotifyCollectionChanged](https://msdn.microsoft.com/en-us/library/system.collections.specialized.inotifycollectionchanged(v=vs.110).aspx)
for collections.

Bifrost have full client support for both XAML based clients and also for JavaScript / Web based.
For XAML and what is supported, read more in detail [here](../Frontend/XAML).
For the JavaScript support, Bifrost has been built on top of [Knockout](http://knockoutjs.com) that provides ``obervable()`` and ``observableArray()``.
Read more about the JavaScript support [here](../Frontend/JavaScript).


### Figures

A traditional MVVM would look something like this:

![MVVM Architectural Diagram](images/mvvm.png)

With the artifacts found in Bifrost and more separation in place with CQRS, the diagram looks slightly different

![MVVM Architectural Diagram - Bifrost artifacts](images/mvvm_bifrost.png)

You can read more details about the MVVM pattern [here](https://en.wikipedia.org/wiki/Model–view–viewmodel).


## Full Pipeline

Below is the full pipeline of Bifrost when utilizing all its capabilities.

![Bifrost Pipeline](images/full_pipeline.png)