﻿using System;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.FluentValidation.Commands;
using Bifrost.Read;
using Bifrost.Tenancy;
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

        public CommandHandler(IAggregateRootRepository<MyAggregate> repository, ITenant tenant)
        {
            _repository = repository;
        }

        public void Handle(MyCommand command)
        {
            var g = Guid.Parse("28ca41b6-68d8-4464-b8f8-e270cc928371");
            var es = _repository.Get(g);
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


        void On(MyEvent @event)
        {
            var i = 0;
            i++;
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