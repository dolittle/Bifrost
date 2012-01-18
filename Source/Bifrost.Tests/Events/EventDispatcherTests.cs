using System.Collections.Generic;
using Bifrost.Events;
using Moq;
using NUnit.Framework;

namespace Bifrost.Tests.Events
{
	[TestFixture]
	public class EventDispatcherTests
	{
		[Test]
		public void PublishingSingleEventShouldPublishEventToRegisteredBus()
		{
			var busMock = new Mock<IEventBus>();
			var dispatcher = new EventDispatcher();
			var eventMock = new Mock<IEvent>();
			dispatcher.RegisterBus(busMock.Object);
			dispatcher.Publish(eventMock.Object);
			
			busMock.Verify(b => b.Publish(It.IsAny<IEvent>()), Times.Once());
		}

		[Test]
		public void PublishingSingleShouldPublishEventToAllRegisteredBuses()
		{
			var firstBusMock = new Mock<IEventBus>();
			var secondBusMock = new Mock<IEventBus>();
			var dispatcher = new EventDispatcher();
			var eventMock = new Mock<IEvent>();
			dispatcher.RegisterBus(firstBusMock.Object);
			dispatcher.RegisterBus(secondBusMock.Object);
			dispatcher.Publish(eventMock.Object);

			firstBusMock.Verify(b => b.Publish(It.IsAny<IEvent>()), Times.Once());
			secondBusMock.Verify(b => b.Publish(It.IsAny<IEvent>()), Times.Once());
		}

		[Test]
		public void PublishingMultipleEventsShouldPublishEventsToRegisteredBus()
		{
			var busMock = new Mock<IEventBus>();
			var dispatcher = new EventDispatcher();

			var events = new List<IEvent>();
			for( var i=0; i<10; i++ )
			{
				var eventMock = new Mock<IEvent>();
				events.Add(eventMock.Object);
			}
			
			dispatcher.RegisterBus(busMock.Object);
			dispatcher.Publish(events);

			busMock.Verify(b => b.Publish(It.IsAny<IEnumerable<IEvent>>()), Times.Once());
		}

		[Test]
		public void PublishingMultipleEvennntsShouldPublishEventsToAllRegisteredBuses()
		{
			var firstBusMock = new Mock<IEventBus>();
			var secondBusMock = new Mock<IEventBus>();
			var dispatcher = new EventDispatcher();
			dispatcher.RegisterBus(firstBusMock.Object);
			dispatcher.RegisterBus(secondBusMock.Object);
			var events = new List<IEvent>();
			for (var i = 0; i < 10; i++)
			{
				var eventMock = new Mock<IEvent>();
				events.Add(eventMock.Object);
			}


			dispatcher.Publish(events);

			firstBusMock.Verify(b => b.Publish(It.IsAny<IEnumerable<IEvent>>()), Times.Once());
			secondBusMock.Verify(b => b.Publish(It.IsAny<IEnumerable<IEvent>>()), Times.Once());
		}

	}
}
