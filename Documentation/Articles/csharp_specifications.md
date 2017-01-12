# CSharp Specifications

All the C# code has been specified by using [Machine Specifications](http://github.com/machine/machine.specifications) with an adapted style.
Since we're using this for specifying units as well, we have a certain structure to reflect this. The structure is reflected in the folder structure and naming of files.

The basic folder structure we have is:

    (project to specify).Specs
        (namespace)
            for_(unit to specify)
                given
                    a_(context).cs
                when_(behavior to specify).cs


A concrete sample of this would be:

    Bifrost.Specs
        Commands
            for_CommandContext
                given
                    a_command_context_for_a_simple_command_with_one_tracked_object.cs
                when_committing.cs


The implementation can then look like this :


```csharp
    public class when_committing : given.a_command_context_for_a_simple_command_with_one_tracked_object_with_one_uncommitted_event
    {
        static UncommittedEventStream   event_stream;

        Establish context = () => event_store_mock.Setup(e=>e.Save(Moq.It.IsAny<UncommittedEventStream>())).Callback((UncommittedEventStream s) => event_stream = s);

        Because of = () => command_context.Commit();

        It should_call_save = () => event_stream.ShouldNotBeNull();
        It should_call_save_with_the_event_in_event_stream = () => event_stream.ShouldContainOnly(uncommitted_event);
        It should_commit_aggregated_root = () => aggregated_root.CommitCalled.ShouldBeTrue();
    }
```

The specifications should read out very clearly in plain English, which makes the code look very different from what we do for our units. For instance we use underscore (_) as space in type names, variable names and the specification delegates. We also want to keep things as one-liners, so your Establish, Because and It statements should preferably be on one line. There are some cases were this does not make any sense, when you need to verify more complex scenarios. This also means that an It statement should be one assert. 
[Moq](http://code.google.com/p/moq/) is used for for handling mocking / faking of objects.