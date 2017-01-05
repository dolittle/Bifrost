using System;
using System.Security.Claims;
using System.Security.Principal;
using Bifrost.Commands;
using Bifrost.Configuration;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Security;
using Bifrost.FluentValidation.Commands;
using FluentValidation;

namespace ConsoleApplication
{
    public class MyCommand : Command
    {
        public string Something { get; set; }
    }

    public class MyCommandInputValidator : CommandInputValidator<MyCommand>
    {
        public MyCommandInputValidator()
        {
            RuleFor(c=>c.Something).NotEmpty().WithMessage("It is absolutely required");
        }
    }

    public class MyEvent : Event
    {
        public MyEvent(Guid eventSourceId) : base(eventSourceId) { }
    }

    public class MyAggregate : AggregateRoot
    {
        public MyAggregate(Guid eventSourceId) : base(eventSourceId) { }

        public void DoStuff()
        {
            Apply(new MyEvent(Id));

        }
    }


    public class MyCommandHandlers : IHandleCommands
    {
        IAggregateRootRepository<MyAggregate> _repository;

        public MyCommandHandlers(IAggregateRootRepository<MyAggregate> repository)
        {
            _repository = repository;
        }

        public void Handle(MyCommand command)
        {
            var ar = _repository.Get(Guid.NewGuid());
            ar.DoStuff();
        }
    }

    public class MyEventProcessor : IProcessEvents
    {
        public void Process(MyEvent @event)
        {
            var i = 0;
            i++;
        }

    }

    public class PrincipalResolver : ICanResolvePrincipal
    {
        public IPrincipal Resolve()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim("Name", "Unknown"));
            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            GenericPrincipal.ClaimsPrincipalSelector = () =>
             {
                 var identity = new ClaimsIdentity();
                 identity.AddClaim(new Claim("Name", "Unknown"));
                 var principal = new ClaimsPrincipal(identity);
                 return principal;
             };

            var p = GenericPrincipal.Current;

            Configure.DiscoverAndConfigure();

            var typeDiscoverer = Configure.Instance.Container.Get<ITypeDiscoverer>();

            var commandCoordinator = Configure.Instance.Container.Get<ICommandCoordinator>();
            var command = new MyCommand();
            command.Something = "Hello";
            var result = commandCoordinator.Handle(command);
       }
    }
}