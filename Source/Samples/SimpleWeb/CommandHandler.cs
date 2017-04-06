using System;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.FluentValidation.Commands;
using Bifrost.Read;
using Events.Awesome;
using FluentValidation;

namespace Domain.Awesome
{

    public class InputValidator : CommandInputValidator<MyCommand>
    {
        public InputValidator()
        {
            RuleFor(c => c.Something).NotEmpty().WithMessage("Gotta have this");
        }
    }

    public class CommandHandler : IHandleCommands
    {
        IAggregateRootRepository<MyAggregate> _repository;

        public CommandHandler(IAggregateRootRepository<MyAggregate> repository)
        {
            _repository = repository;
        }

        public void Handle(MyCommand command)
        {
            var es = _repository.Get(Guid.NewGuid());
            es.DoStuff(command.Something);
        }
    }


    public class MyAggregate : AggregateRoot
    {
        
        public MyAggregate(EventSourceId eventSourceId) : base(eventSourceId)
        {

        }


        public void DoStuff(string something)
        {
            Apply(new MyEvent(EventSourceId) { Something = something});
        }
    }
}


namespace Events.Awesome
{

    public class MyEvent : Event
    {
        public MyEvent(EventSourceId eventSourceId) : base(eventSourceId) {}

        public string Something { get; set; }

    }


}

namespace Read.Awesome
{
    public class MyReadModel : IReadModel
    {
        
    }


    public class MyQuery : IQueryFor<MyReadModel>
    {

        public string SomeParameter { get; set; }
        public IQueryable<MyReadModel> Query => new MyReadModel[0].AsQueryable();
    }


    public class EventProcessors : IProcessEvents
    {
        public void Process(MyEvent @event)
        {
            var i=0;
            i++;
        }
    }
}