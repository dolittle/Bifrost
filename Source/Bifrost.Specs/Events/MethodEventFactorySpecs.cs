using System;
using Bifrost.Events;
using NUnit.Framework;

namespace Bifrost.Specs.Events
{
	[TestFixture]
	public class MethodEventFactorySpecs
	{
		public class MyObject
		{
			public void SimpleMethod()
			{
				
			}

			public void SimpleMethodWithOneArgument(string argument)
			{
				
			}

			public void SimpleMethodWithTwoArguments(string firstArgument, int secondArgument)
			{
				
			}
		}

		[Test]
		public void CreatingAnEventFromASimpleMethodWithoutArgumentsShouldReturnMethodEventForThatMethod()
		{
			var obj = new MyObject();
			var @event = MethodEventFactory.CreateMethodEventFromExpression(Guid.NewGuid(), () => obj.SimpleMethod());
			Assert.That(@event, Is.Not.Null);
			Assert.That(@event.Name,Is.EqualTo("SimpleMethod"));
		}

		[Test]
		public void CreatingAnEventFromASimpleMethodWithOneArgumentsShouldReturnMethodEventWithArgumentForThatMethod()
		{
			var obj = new MyObject();
			var expectedArgument = "Something";
			var @event = MethodEventFactory.CreateMethodEventFromExpression(Guid.NewGuid(), () => obj.SimpleMethodWithOneArgument(expectedArgument));
			Assert.That(@event, Is.Not.Null);
			Assert.That(@event.Name, Is.EqualTo("SimpleMethodWithOneArgument"));
			dynamic arguments = @event.Arguments;
			Assert.That(@arguments.argument,Is.EqualTo(expectedArgument));
		}

		[Test]
		public void CreatingAnEventFromASimpleMethodWithTwoArgumentsShouldReturnMethodEventWithArgumentsForThatMethod()
		{
			var obj = new MyObject();
			var expectedFirstArgument = "Something";
			var expectedSecondArgument = 42;
			var @event = MethodEventFactory.CreateMethodEventFromExpression(Guid.NewGuid(), () => obj.SimpleMethodWithTwoArguments(expectedFirstArgument, expectedSecondArgument));
			Assert.That(@event, Is.Not.Null);
			Assert.That(@event.Name, Is.EqualTo("SimpleMethodWithTwoArguments"));
			dynamic arguments = @event.Arguments;
			
			Assert.That(arguments.firstArgument, Is.EqualTo(expectedFirstArgument));
			Assert.That(arguments.secondArgument, Is.EqualTo(expectedSecondArgument));
		}

		[Test]
		[TestCase("A string")]
		public void CreatingAnEventFromASimpleMethodWithOneArgumentAndNotUsingConstantsShouldReturnMethodEventWithArgumentsForThatMethod(string expectedArgument)
		{
			var obj = new MyObject();
			var @event = MethodEventFactory.CreateMethodEventFromExpression(Guid.NewGuid(), () => obj.SimpleMethodWithOneArgument(expectedArgument));
			Assert.That(@event, Is.Not.Null);
			dynamic arguments = @event.Arguments;
			Assert.That(@event.Name, Is.EqualTo("SimpleMethodWithOneArgument"));
			Assert.That(arguments.argument, Is.EqualTo(expectedArgument));
		}
	}
}
