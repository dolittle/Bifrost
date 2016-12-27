# The Goal

The purpose of this tutorial is to take you through the entire stack of Bifrost, all the steps you need
and can do to successfully build software using Bifrost. The philosophy behind Bifrost is somewhat 
different than what you find in more established architectures. For instance, you'll hardly see any
talk about data and doing good old CRUD (Create Read Update Delete), instead we talk about behaviors
and their consequences which in most cases result in data being generated, but not necessarily.
Often we also find that data is just one of the consequences of a behavior going through the system.
Another good point is that we don't necessarily believe in the one size fits all when it comes to
data in a system. Some data is best suited to sit inside a relational database, while some might be
better off in a document based, and many times its perfectly fine to have it be directly generated
to a static file sitting on the file system. Also, any combinations are also in many cases valid.
This opens for a few opportunities of focusing in on every feature of your software instead of 
looking at it as a whole and trying to optimize for the edge cases; leave the edge cases behind 
and model them explicitly when needed.

Building software is hard, and trying to focus on an entire application all the time really does not
make any sense in our oppinion. Instead we believe that applications are actually composed of smaller
more focused applications brought together in a bigger setting. When approaching software in this
manner you end up with code that is more decoupled, more maintainable and less prone to errors and
regression. 

## The user story

In this tutorial we're going to create a simple feature in a corner of an imagined system.
The feature we're looking at creating is the ability to register an employee. We're not going
to even try to model this remotely close to anything real, as that is too involved in this tutorial,
but rather try to capture the process of creating an end to end feature from the top-down.


### Getting started

### Projects

The way we recommend working is to have multiple projects that are focused on the different parts.
With separating it out and be very clear what their purpose is, we believe you end up with 
something that is more maintainable and easy to understand were to put things.
So, lets get started by setting up a couple of projects. We're going to need the following C# class 
library projects:

* Concepts
* Domain
* Events
* Read

Once you've created the projects some of these projects will need to reference some of the others.
The Domain and the Read project should have a reference to the Concepts and the Events projects.
The Events project represents the contract between the Domain and the Read side.

For the client that we are going to make, we will be needing a totally empty ASP.net project 
without any files in it, except for Web.config.

Name the project "Web".

### Nuget

Bifrost is available on Nuget, so we will be using Nuget to pull down Bifrost and any dependencies
it has.

For the Concepts, Domain, Events and Read projects, we need to add a dependency only to Bifrost, 
the core part:

    PM> Install-Package Bifrost


For the Web project we will need more, as we are going to have to configure things. Bifrost has support
for all kinds of combinations of IOC containers, database choices and so forth, but for this tutorial
we will be very specific and use a set of extensions to Bifrost that we have neatly packed into a 
Nuget package called Bifrost.Defaults.

So select the Web project and do:

    PM> Install-Package Bifrost.Defaults

What this does is setup Bifrost with Ninject as IOC Container, RavenDB embedded and SignalR.
It also configures Bifrost to treat this as a single page application and adds an HTML file that
just sets up Bifrost for you to start working with it.

### Frontend

We're fond of doing top-down development, starting in the frontend and move downwards.

The feature we're going to create is going to be the registration of Employees.
Inside your web project, add a folder called Features and inside it add another folder called Employees. 
Now we're going to add the view for the registration. Add an HTML file called register.html inside the
Employees folder. We're going to add some HTML within the body tag in the newly created file.

    <fieldset>
        <strong>First Name</strong>
        <input type="text" />
        <br />

        <strong>Last Name</strong>
        <input type="text" />
        <br />

        <strong>Social Security Number</strong>
        <input type="text" />
        <br />

        <button>Register</button>
    </fieldset>

Now we're going to add a viewmodel that will be associated with your feature.

    Bifrost.namespace("Features.Employees", {
        register: Bifrost.Type.extend(function() {
        })
    });

In the index.html file sitting at the root of your Web project, go and add the following within the body 
tag:

    <div data-view="Employees/register"></div>

Running your application with index.html as your startup page now should show the registration page you created.
For now we're going to leave it at that. We will revisit this when we have the necessary bits ready in the
C# code.


## Domain

The domain is were you define your systems behaviors, and its what represents the vocabulary of your system, 
the verbs and the nouns. We will be structuring our domain according to principles found in 
[Domain Driven Design](http://en.wikipedia.org/wiki/Domain-driven_design).

### Bounded Contexts

A bounded context is a context that contains its own vocabulary or own representations of elements found in
the domain in your particular system. For instance if you were doing an e-commerce solution, something that
tends to show up is the term product. But in most places within an e-commerce business, product is hardly
used by the people in the different bounded contexts. Take the warehouse, they do not see products but 
rather the boxes and the characteristics of those boxes such as dimensions and weight, while the name of 
the product inside the box is irrelevant to them. Another bounded context in e-commerce would be the
purchasing department were they see other aspects of the product, they are mostly interested in margins
and other economical data. While for the user hitting the e-commerce site, they are interested in a lot of
details about the product. The purpose of establishing the bounded contexts for these are then to be able
to model what is relevant to the different parts of an organization like this.

For this tutorial we will have one bounded context, its going to be called "Human Resources".

### Modules

Within a bounded context, we talk about modules. These are more technical in nature. Its a way of grouping
relevant elements within a bounded context together. Take the e-commerce example, anything related to 
Products within a bounded context could for instance be put into a module with the name of Products.
We consider Modules as optional, but helpful in structuring an application. One of the things that we 
highly recommend to do is to not try to make one big application, but rather see them as many small ones
and compose them together. Modules is part of this fragmentation and helps focusing on decoupling your
software.

### Concepts

A concept is a lightweight representation of something you have in your domain vocabulary. Instead of losing
what things are by using primitives such as booleans, integers or strings, we encapsulate these parts of
the vocabulary into what we call concepts. Bifrost has a baseclass to help you out with this and something
that is supported throughout Bifrost; ConceptAs<>. We recommend making concepts available for both your
read side and your execution / behavior side, this is were the Concepts project we created earlier comes
into play.

For this particular tutorial we will be needing one Concept. We are working with persons, more specifically
employees that we are going to register, a natural key that represent a person is the social security number
assigned at birth.

Inside the Concept project create a folder called Persons. 
Add a new class inside the new folder called SocialSecurityNumber.

    using Bifrost.Concepts;

    namespace Concepts.Persons
    {
        public class SocialSecurityNumber : ConceptAs<string>
        {
            public static implicit operator SocialSecurityNumber(string socialSecurityNumber)
            {
                return new SocialSecurityNumber { Value = socialSecurityNumber };
            }
        }
    }

The concept can now be used and be capable of going back and forth from primitives, something you'll see
be handy when we get to applying events. We only need to implement the implicit operator for going from
the underlying type; string, to the concept itself. The other implicit operator sits at the base-class.


### Command

A command represents the behavior you want to happen in your system. It represents the intent of the user
and is optimized for one purpose and the idea is to not reuse commands across the system. 
With a command, one can also express so much more than just what it is doing, but also the why, its
all just a question of how you name the command. And don't be afraid to dive in and make many commands
that effectively is doing the same thing, remember that inheritance is fine and you can have a base
one representing the actual behavior that you want to happen in the system but be very explicit on
the deriviatives of the users intentions. It is very important to recognize a command to be something
you want to happen in the system, so we model it so.

For our tutorial we will not be all too creative, we will simply be adding a RegisterEmployee command.

    using Bifrost.Commands;
    using Concepts.Persons;

    namespace Domain.HumanResources.Employees
    {
        public class RegisterEmployee : Command
        {
            public SocialSecurityNumber SocialSecurityNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }

### Input validation

Performing a behavior in a system needs to be validated that it is correct before we can apply it.
Validation is divided into two steps; Input and Business. Input being typically connected with
properties and consumed on a client. Bifrost generates metadata that can be used directly by
clientside validators.

Lets start by adding an Input validator for our command. Add a file next to the command called
RegisterEmployeeInputValidator and make it look like below:

    using Bifrost.Validation;
    using FluentValidation;

    namespace Domain.HumanResources.Employees
    {
        public class RegisterEmployeeInputValidator :
                         CommandInputValidator<RegisterEmployee>
        {
            public RegisterEmployeeInputValidator()
            {
                RuleFor(c=>c.FirstName)
                    .NotEmpty()
                        .WithMessage("You have to specify the first name");

                RuleFor(c=>c.LastName)
                    .NotEmpty()
                        .WithMessage("You have to specify the last name");
            }
        }
    }

### Business validation

Business validation is the next step after basic input validation has been executed. 
You would normally use business validation to perform more complex scenarios and things
that are more cross cutting within the command and not necessarily linked to a property.
Instead of using the RuleFor() method, you might find the ModelRule() method more 
convenient in this.

Business validators tend to be more involved than property level, they can access 
resources like the database or similar to get to their validation.

For now, business validation is not something we will be performing at this stage. But
the way you would write one is very similar to that of an input validator:

    using Bifrost.Validation;
    using FluentValidation;

    namespace Domain.HumanResources.Employees
    {
        public class RegisterEmployeeBusinessValidator :
                         CommandBusinessValidator<RegisterEmployee>
        {
        }
    }


### Security

Normally we would at this stage be looking at security and Bifrost has a very similar
approach as with validation to how to setup security. We will not be doing this in this
tutorial, it will be the subject of another tutorial.

### Events

Now that we have our command and it has been validated through the system, its time to
turn what we want to have happen to the system into something that has happened, the
things that make up the truth in the system; the events. Naming plays an important role
here. We turn the naming around and say that things have happened, so registering an
employee becomes; EmployeeRegistered. Notice that we also don't use the domain concept
on the event. This is due to the fact that we want to keep our events as simple as possible.
This because of serialization, persistence and in general avoid potential problems with
versioning of types and such. So only primitives for this particular part.

Recreate the bounded context and module structure in the Events project and add the 
EmployeeRegistered class into the Employees module and make it look like this:

    using System;
    using Bifrost.Events;

    namespace Events.HumanResources.Employees
    {
        public class EmployeeRegistered : Event
        {
            public EmployeeRegistered(Guid eventSourceId) : base(eventSourceId) {}

            public string SocialSecurityNumber { get; set; }
            public string FirstName { get; set; )
            public string LastName { get; set; }
        }
    }

### Aggregate Root

Once we have our event we need something to apply it, this is were we create an aggregate, a
transactional boundary that can apply the event. These aggregates do not expose any public
state, only public behaviors - or methods as we call them in C#. They can hold internal state
and have their state be populated from the events they self have generated over time by
implementing a private On() method that takes in the event it wants to respond to.

The aggregate should be modelled as a transaction, any invariants that are related to each
other and goes together as a transaction should be kept together. This is very different
from more traditional ways of modelling a domain were a transaction is just whatever 
change you are doing.

Going back in the Domain project in the HumanResources and Employees folder, add a class 
called Employee, it should look like below:

    using System;
    using Bifrost.Domain;
    using Events.HumanResources.Employee;

    namespace Domain.HumanResources.Employees
    {
        public class Employee : AggregateRoot
        {
            public Employee(Guid id) : base(id) {}

            public void Register(
                            SocialSecurityNumber socialSecurityNumber,
                            string firstName,
                            string lastName)
            {
                Apply(new EmployeeRegistered(Id)
                {
                    SocialSecurityNumber = socialSecurityNumber,
                    FirstName = firstName,
                    LastName = lastName
                }):
            }
        }
    }

### Command Handler

All we've done now is introduce a command, some validation, an event and an aggregate that 
can apply the event. But we haven't added any code that can handle the command. 
CommandHandlers are responsible for taking a command and performing the necessary actions
in the domain. Implementing command handlers is very easy, all one has to do is create a class
and stick a marker interface called IHandleCommands in there and just start implementing by
convention public methods called Handle() that takes the specific command you want to handle
in as a parameter. There can only be one handle method in the system per command.

In the domain project, lets add a class called CommandHandlers next to the command and the
validators and the aggregate. Make it look like below:

    using Bifrost.Commands;
    using Bifrost.Domain;

    namespace Domain.HumanResources.Employees
    {
        public class CommandHandlers : IHandleCommands
        {
            IAggregateRootRepository<Employee> _repository;

            public CommandHandlers(IAggregateRootRepository<Employee> repository)
            {
                _repository = repository;
            }

            public void Handle(RegisterEmployee command)
            {
                var employee = _repository.Get(Guid.NewGuid());
                employee.Register(
                    command.SocialSecurityNumber,
                    command.FirstName,
                    command.LastName);
            }
        }
    }

### CommandCoordinator

In Bifrost sits an entry point for commands, this is the place that all commands go through.
Using the entire stack of Bifrost, you don't necessarily see this system.
The CommandCoordinator is responsible for coordinating the pipeline of a command, the unit of 
work in which a command lives in. The unit of work is called CommandContext and is unique
for every command instance that goes through the system. At the very end of such a unit of
work, Bifrost commits any events that was applied during the unit of work. This is similar
to a transaction.

### CommandResult

From the CommandCoordinator comes an object containing information about the result of handling
a command; CommandResult. In this you'll find details about validation, possible exceptions,
security rules that might have been broken and such. It does not hold details about what happened
in any event subscribers as they are not part of the unit of work of a command, but can be 
asynchronously processed.

### ReadModel

Now that we've taken care of business and applied events, we must look at the consequences of
this. Lets start with the end result, the data what we will have. In Bifrost we refer to this
as a readmodel, and we have a marker interface called IReadModel. 

Lets go into the Read project and recreate the bounded context and the module structure; 
HumanResources.Employees. Add a class called Employee and make it look like below:

    using System;
    using Bifrost.Read;

    namespace Read.HumanResources.Employees
    {
        public class Employee : IReadModel
        {
            public Guid Id { get; set; }
            public SocialSecurityNumber	SocialSecurityNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }

### Event processor

With the readmodel in place, we can finally start processing the event and actually insert
the data we generate from it into our datasource. We call these processors EventSubscribers
and we register subscriptions for them. In order to be able to process events, we do things
similar to how we did the command handler. We create a class, mark it with an interface called
IProcessEvents and implement a public method called Process() taking the specific event that
you want to process as a parameter. You can have multiple of these Process() methods around the
system responding differently to the event coming through. For instance, you could have one
subscriber that would deal with the data change that the event causes and another dealing with
sending an email or similiar, and they would be separated out in different files blissfully 
unaware of the other subscribers existense.


    using Bifrost.Events;
    using Bifrost.Read;
    using Events.HumanResources.Employees;

    namespace Read.HumanResources.Employees
    {
        public class EventSubscribers : IProcessEvents
        {
            IReadModelRepositoryFor<Employee> _repository;

            public EventSubscribers(IReadModelRepositoryFor<Employee> repository)
            {
                _repository = repository;
            }

            public void Process(EmployeeRegistered @event)
            {
                _repository.Insert(new Employee
                {
                    Id = @event.EventSourceId,
                    SocialSecurityNumber = @event.SocialSecurityNumber,
                    FirstName = @event.FirstName,
                    LastName = @event.LastName
                });
            }
        }
    }

### Query

In order to get the data out, Bifrost comes with a formalization of querying that we
will be using. The formalization involves creating a class representing the different
queries one needs with the name of the class giving away the name of the query. 
All one needs to do then is to implement the IQueryFor<> generic interface and implement
the query. The reasoning behind this model is to move the concerns of the client away
from the server, so typically paging and similar things is not something you have to
concern yourself about - Bifrost will amend this information to the IQueryable<> that you
return.

In the Read project inside the Employees folder, add a class called AllEmployees and
make it look like below:

    using Bifrost.Read;

    namespace Read.HumanResources.Employees
    {
        public class AllEmployees : IQueryFor<Employee>
        {
            IReadModelRepositoryFor<Employee> _repository:

            public AllEmployees(IReadModelRepositoryFor<Employee> repository)
            {
                _repository = repository;
            }

            public IQueryable<Employee> Query
            {
                get
                {
                    return _repository.Query;
                }
            }
        }
    }


## Going back up into the frontend

With all the artifacts we now in C#, Bifrost produces a set of proxies at runtime that the JavaScript can take advantage of.

### ViewModel

Remember the ViewModel that we put in, not very exciting - in fact, nothing happening in it at all. 
With everything in place now we can basically start taking dependencies into the viewmodel and use data-binding
to hook it all up in the view.

Lets modify the viewmodel to look like this:

    Bifrost.namespace("Features.Employees", {
        register: Bifrost.Type.extend(function(registerEmployee, allEmployees) {
            var self = this;
            this.register = registerEmployee;
            this.employees = allEmployees.all();
        })
    });

Basically what we`ve done now is to take a dependency on the command we created and the query we created.
Bifrost generates a proxy for these and you can just use them directly like above. Inside Bifrost there sits
an IOC (Inversion of Control) container that resolves dependencies in different ways, one being well known
commands and queries coming from proxies.

### View

Now that we have that we can go ahead and modify the view to be take advantage of the command and the query.
Going into the view file, we need to make it look like this:

    <fieldset>
        <strong>First Name</strong>
        <input type="text" data-bind="value: register.firstName" />
        <span data-bind="validationMessageFor: register.firstName"></span>
        <br />

        <strong>Last Name</strong>
        <input type="text" data-bind="value: register.lastName" />
        <span data-bind="validationMessageFor: register.lastName"></span>
        <br />

        <strong>Social Security Number</strong>
        <input type="text" data-bind="value: register.socialSecurityNumber" />
        <span data-bind="validationMessageFor: register.socialSecurityNumber"></span>
        <br />

        <button data-bind="command: register">Register</button>
    </fieldset>

What the above alteration has done is to add binding of the values on the commands into the inputs.
In addition we add validation messages using a binding handler that comes with Bifrost called 
"validationMessageFor" which points to the same values as its input is bound to. 
The button gets bound up using another binding handler from Bifrost; command, and is bound 
directly to the command sitting on the viewmodel.

The app should now be capable of executing the command and have all validation hooked up automatically.
You can confirm this by not entering anything into the fields and clicking the button should 
yield the error messages in the client. If you use rules that can only be run on the server,
the error messages will still propagate into the client.

### Showing the consequence; data

Now that we are firing off commands, we want to be able to actually show the data this produced.
In our view we just add the following below the fieldset.

    <table>
        <thead>
            <tr>
                <th>Social Security Number</th>
                <th>First Name</th>
                <th>Last Name</th>
            </tr>
        </thead>
        <tbody data-bind="foreach: employees">
            <tr>
                <td data-bind="text: socialSecurityNumber"></td>
                <td data-bind="text: firstName"></td>
                <td data-bind="text: lastName"></td>
            </tr>
        </tbody>
    </table>

This should now be showing the result.