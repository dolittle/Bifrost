# Contributing to Bifrost

You can contribute through the [issue list](https://github.com/dolittle/Bifrost/issues) or discussing issues in the list.
If you want to contribute with code, you can submit a pull request with your changes.
This project has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/) to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](http://www.dotnetfoundation.org/code-of-conduct). Read the [Code of Conduct](../CODE_OF_CONDUCT.md).

If you want to contribute to the documentation, you can do so by either using the "Improve this Doc" link at the top or
submit a pull request with documentation changes. The documentation sits inside the ``Documentation``folder in the repository.
All documentation is written using the GitHub flavor of [markdown](https://guides.github.com/features/mastering-markdown/).
In addition to this, all API documentation comes from the code itself.
For C# code, one has to provide XML documentation in the code, without it the build will be broken.

When submitting a pull request, your code will be run on the build server where it will be compiled and automated tests are ran.
If this fail, your pull request will be marked as failing the build.

## The vision

There sits a vision behind Bifrost. Its not necessarily a fixed vision. But the primary objectives are to build a platform that is easy
to use, with high productivity and is easy to maintain. On top of this, the vision has always been and will continue to be to solve
problems for line-of-business applications. It is not aimed to solve arbitrary problems, even though Bifrost might be solving something
outside of the primary focus. This does mean that contributions to Bifrost might not be accepted. Its hard to keep it focused, but
the team behind it will do its best to maintain the focus and not let it diverge from the vision.

The vision of what needs to be implemented shifts over time as one learns and gains experiences from usage of Bifrost. Plus new
techniques and technologies emerges and Bifrost will adapt to these when it makes sense as well. These things, combined with a good
and open dialog with often makes the concrete plans less fixed.

## The Source

Bifrost is hosted on [GitHub](http://github.com) [here](http://github.com/dolittle/bifrost), a social hub for software projects.

## Contributing code and content

> ![Note]
> As of 28th of December 2016:

Bifrost has been submitted for review to .NET Foundation - if it is accepted, you will need to sign a
[Contributor License Agreement](https://cla2.dotnetfoundation.org/) before submitting your pull request.
If Bifrost is not accepted into the foundation, we will have to create our own CLA and require one to sign it.

## Code review

When submitting a pull request, the code will be reviewed.
The things that are being looked at is as follows:

- Does it adhere to the vision. Get an idea from [here](overview.md)
- Naming of types, variables
- Does the code adhere to the patterns, practices and principles Bifrost is built upon (SOLID, SOC ++) - read more [here](overview.md)

## Specifications / Tests

All code should have automated tests.
Bifrost uses a branch of [Behavior Driven Development](http://en.wikipedia.org/wiki/Behavior-driven_development) referred to
as [Specification by Example](http://specificationbyexample.com). These are run continuously to make sure we keep the code as
we specified it to be and we don't get regressions.

Every pull request must have specifications that are passing associated with it as described below.

### C-Sharp

All the C# code has been specified by using [MSpec](http://github.com/machine/machine.specifications) with an adapted style. 
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


    public class when_committing : given.a_command_context_for_a_simple_command_with_one_tracked_object_with_one_uncommitted_event
    {
        static UncommittedEventStream   event_stream;

        Establish context = () => event_store_mock.Setup(e=>e.Save(Moq.It.IsAny<UncommittedEventStream>())).Callback((UncommittedEventStream s) => event_stream = s);

        Because of = () => command_context.Commit();

        It should_call_save = () => event_stream.ShouldNotBeNull();
        It should_call_save_with_the_event_in_event_stream = () => event_stream.ShouldContainOnly(uncommitted_event);
        It should_commit_aggregated_root = () => aggregated_root.CommitCalled.ShouldBeTrue();
    }

The specifications should read out very clearly in plain English, which makes the code look very different from what we do for our units. For instance we use underscore (_) as space in type names, variable names and the specification delegates. We also want to keep things as one-liners, so your Establish, Because and It statements should preferably be on one line. There are some cases were this does not make any sense, when you need to verify more complex scenarios. This also means that an It statement should be one assert. 
[Moq](http://code.google.com/p/moq/) is used for for handling mocking / faking of objects.

### JavaScript

All JavaScript code has been specified using [Jasmine](http://pivotal.github.com/jasmine/) with a similar style and structure as with the C# code. The folder structure is pretty much the same, except for the given folder. And since we've for now settled on Jasmine, the code is a bit different.

    describe("when creating with configuration", function () {
        var options = {
            error: function () {
                print("Error");
            },
            success: function () {
            }
        };
        var command = Bifrost.commands.Command.create(options);

        it("should create an instance", function () {
            expect(command).toBeDefined();
        });

        it("should include options", function () {
            for (var property in options) {
                expect(command.options[property]).toEqual(options[property]);
            }
        });
    );
