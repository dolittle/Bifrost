using Bifrost.Events;
using NUnit.Framework;

namespace Bifrost.Tests.Events
{
	[TestFixture]
	public class EventMethodBindingTests
	{
		public class MySubscriber : IEventSubscriber
		{
			
		}

		[Test]
		public void GenericForSubscriberShouldSetSubscriberTypeCorrectly()
		{
			var binding = new EventMethodBinding();
			binding.ForSubscriber<MySubscriber>();
			Assert.That(binding.Subscriber, Is.EqualTo(typeof(MySubscriber)));
		}

		[Test]
		public void ForSubscriberShouldSetSubscriberTypeCorrectly()
		{
			var binding = new EventMethodBinding();
			binding.ForSubscriber(typeof (MySubscriber));
			Assert.That(binding.Subscriber, Is.EqualTo(typeof(MySubscriber)));
		}
	}
}
