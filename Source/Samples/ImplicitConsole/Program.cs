using System;
using Bifrost.Commands;
using Bifrost.Configuration;            
using Bifrost.Ninject;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using ImplicitConsole.Domain;
using ImplicitConsole.Views;

namespace ImplicitConsole
{
	class Program
	{
		static void Main(string[] args)
		{
            var kernel = new StandardKernel();
            var container = new Container(kernel);

            Configure.With(container)
                .Commands
                    .UsingJson("JsonDB")
                .Events
                    .UsingJson("JsonDB")
                .Initialize();

            var commandCoordinator = ServiceLocator.Current.GetInstance<ICommandCoordinator>();

			var command = new CreatePerson { Id = Guid.NewGuid(), FirstName = "First", LastName = "Person" };
			var result = commandCoordinator.Handle(command);
			if( !result.Success )
			{
				Console.WriteLine("Handling of command failed");
				Console.WriteLine("Exception : {0}\nStack Trace : {1}", result.Exception.Message, result.Exception.StackTrace);
			}

			var queries = container.Get<IPersonView>();
			var persons = queries.GetAll();
			foreach (var person in persons)
			{
				Console.WriteLine("Person ({0}) - {1} {2}", person.Id, person.FirstName, person.LastName);
			}
		}
	}
}
