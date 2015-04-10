using Bifrost.Messaging;
using Machine.Specifications;

namespace Bifrost.Client.Specs.Messaging.for_Messenger
{
    public class when_publishing_message_with_two_subscribers_for_different_messages
    {
        static Messenger messenger;

        static SomeMessage message_published;

        static SomeMessage first_subscriber_message;
        static bool second_subscriber_called;

        Establish context = () =>
        {
            messenger = new Messenger();
            messenger.SubscribeTo<SomeMessage>(m => first_subscriber_message = m);
            messenger.SubscribeTo<SomeOtherMessage>(m => second_subscriber_called = true);

            message_published = new SomeMessage();
        };

        Because of = () => messenger.Publish(message_published);

        It should_send_it_to_the_first_subscriber = () => first_subscriber_message.ShouldEqual(message_published);
        It should_not_send_it_to_the_second_subscriber = () => second_subscriber_called.ShouldBeFalse();
    }
}
