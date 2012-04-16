using System;
using Bifrost.Commands;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Ninject;
using ExplicitConsole.Domain;
using ExplicitConsole.Views;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace ExplicitConsole
{
    public class Application : IApplication
    {
        
        public IContainer Container { get { return Program.Container; } }

        public void OnConfigure(IConfigure configure)
        {
        }

        public void OnStarted()
        {
        }

        public void OnStopped()
        {
        }
    }


	class Program
	{
        public static IContainer Container;

		public static void Main (string[] args)
		{
		    var kernel = new StandardKernel();
		    Container = new Container(kernel);

		    Configure.With(Container)
                .UsingJsonStorage("JsonDB")
                .Initialize();

		    var commandCoordinator = ServiceLocator.Current.GetInstance<ICommandCoordinator>();

			var command = new CreatePerson(Guid.NewGuid(), "First", "Person");
			var result = commandCoordinator.Handle(command);
			if (!result.Success)
			{
				Console.WriteLine("Handling of command failed");
				Console.WriteLine("Exception : {0}\nStack Trace : {1}", result.Exception.Message, result.Exception.StackTrace);
			}

			var queries = Container.Get<IPersonView>();
			var persons = queries.GetAll();
			foreach (var person in persons)
			{   
				Console.WriteLine("Person ({0}) - {1} {2}", person.Id, person.FirstName, person.LastName);
			}
		}
	}
}