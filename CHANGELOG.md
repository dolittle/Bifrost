# Release Notes

## Version 1.2.0

* Added guidance for issues [#698](https://github.com/dolittle/Bifrost/issues/698)
* Including PDB files in NuGet Packages [#744](https://github.com/dolittle/Bifrost/issues/744)
* Migrated remaining NUnit specs to MSpec [#687](https://github.com/dolittle/Bifrost/issues/687)
* Introduced GitVersion for getting version for builds [#758](https://github.com/dolittle/Bifrost/issues/758)
* Changed all files from CRLF to LF [#732](https://github.com/dolittle/Bifrost/issues/732)
* Various changes migrated from ProCoSys fork: [#713(https://github.com/dolittle/Bifrost/issues/713)
  - Use activator to create instances of Commands when generating proxies
  - Limit initialization of AssetsManager once - JavaScript
  - Introduce IConvention as a marker interface for finding things that are governed by conventions
  - Composed validators improvement
  - Security proxies excluded from the application route
* Updated debugging documentation [#734](https://github.com/dolittle/Bifrost/issues/734)
* Documentation for how you could set up Web.config [#735](https://github.com/dolittle/Bifrost/issues/735)
* Documentation for editor config [#731](https://github.com/dolittle/Bifrost/issues/731)
* Added MyGet symbol source [#737](https://github.com/dolittle/Bifrost/issues/737)
* Cleaned up Tabs to Spaces [#733](https://github.com/dolittle/Bifrost/issues/)
* Ensure CommandHandlerInvoker is only called once [#766](https://github.com/dolittle/Bifrost/issues/766)
* Upgraded to release version of FluentValidation [#775](https://github.com/dolittle/Bifrost/issues/775)
* `IExceptionSubscriber` for catching errors that can be logged or surfaced [#773](https://github.com/dolittle/Bifrost/issues/773)
* Updated to the latest Mongo DB driver [#789](https://github.com/dolittle/Bifrost/issues/789) [#594](https://github.com/dolittle/Bifrost/issues/594) [#779](https://github.com/dolittle/Bifrost/issues/779)
* [Breaking Change] Configuration of events for pipeline [#787](https://github.com/dolittle/Bifrost/issues/787)
* [Breaking Change] Changed entire pipeline of events and subscriptions [#596](https://github.com/dolittle/Bifrost/issues/596) [#193](https://github.com/dolittle/Bifrost/issues/193)
* [Breaking Change] Move to OWIN and not be coupled to System.Web [#515](https://github.com/dolittle/Bifrost/issues/515)
* [Breaking Change] Include Application details instead of system [#799](https://github.com/dolittle/Bifrost/issues/799)
* [Breaking Change] Updated ITenant to use a `dynamic` instead of `WriteOnceExpandoObject` [#797](https://github.com/dolittle/Bifrost/issues/797)
* Fixed so that `ICanPopulateTenant` implementations actually gets called [#798](https://github.com/dolittle/Bifrost/issues/798)
* Using ContentRoot as the actual content root for .NET Core Applications [#800](https://github.com/dolittle/Bifrost/issues/800)
* Provide a workaround for .NET standard of Ninject for default bindings [#801](https://github.com/dolittle/Bifrost/issues/801)
* [Breaking Change] Improved configuration of Events [#591](https://github.com/dolittle/Bifrost/issues/591)
* [Breaking Change] Do not expose default lifecycle for containers [#717](https://github.com/dolittle/Bifrost/issues/717)
* EventStore implementation for Azure Tables [#771](https://github.com/dolittle/Bifrost/issues/771)
* Simple support for Rabbit MQ [#741](https://github.com/dolittle/Bifrost/issues/741)
* DocumentDB support moved to Bifrost.Read.DocumentDB [#812](https://github.com/dolittle/Bifrost/issues/812)
* Introduce logging [#816](https://github.com/dolittle/Bifrost/issues/816)
* Fixing an exception that occured in Ninject during parallell loading [#818](https://github.com/dolittle/Bifrost/issues/818)
* Migrate to MSBuild based projects - away from project.json [#815](https://github.com/dolittle/Bifrost/issues/815)
* [Breaking Change] Systemname not part of configuration anymore [#814](https://github.com/dolittle/Bifrost/issues/814)

## Version 1.1.2

* No changes - just fingertrouble when releasing 1.1.1

## Version 1.1.1

* Releasing all the projects - not just the core Bifrost project
* Clean up QuickStart [#736](https://github.com/dolittle/Bifrost/issues/736)
* Create contribution guideline for documentation [#689](https://github.com/dolittle/Bifrost/issues/689)
* Detailed coding guidelines for C# [#688](https://github.com/dolittle/Bifrost/issues/688)
* Issues guideline [#712](https://github.com/dolittle/Bifrost/issues/712)
* Create a travis build [#710](https://github.com/dolittle/Bifrost/issues/) for cross platform - needs more work - it is failing right now
* Put in place editor.config file [#709](https://github.com/dolittle/Bifrost/issues/709)

## Version 1.1.0

* Targetting .NET 4.6.1 as a minimum for desktop framework
* Targetting .NET Standard 1.6 - capable of running on .NET Core 1.x
* Silverlight support removed
* Mimir has been removed as it was not maintained - it will come back in a new form
* License header text updated for all files
* New build mechanism based on FAKE - fully automated
* Removed ASP.NET MVC support - unmaintained
* Documentation is now being generated on build and published
* NullReferenceException in DefaultCallContext ([#665](https://github.com/dolittle/Bifrost/issues/665), [#706](https://github.com/dolittle/Bifrost/issues/706))
* Bifrost.debug.js created using an MSBuild task ([#707](https://github.com/dolittle/Bifrost/issues/707))
* Only call implementations of ICanSpecifyAssemblies once per assembly ([#666](https://github.com/dolittle/Bifrost/issues/666))
* Discover HBM mappings automatically ([#664](https://github.com/dolittle/Bifrost/issues/664))
* Compliance with Newtonsoft.JSON guidelines for caching ([#676](https://github.com/dolittle/Bifrost/issues/676))
* Removed Oracle EventStore support
* Removed Microsoft Unity support
* Removed Yggdrasil support
* Removed Castle Windsor support
* Removed CommonServiceLocator support
* Rmoved WinRT support
* Including [Code of Conduct](CODE_OF_CONDUCT.md)

## Version 1.0.0.31

* LocalPath used for assemblies during loading from the URI ([#662](https://github.com/dolittle/Bifrost/issues/662)) - from PR #663

## Version 1.0.0.30

* Fixed type discovery performance, both for startup and runtime ([#657](https://github.com/dolittle/Bifrost/issues/657))

## Version 1.0.0.28 & 1.0.0.29

* Adding in support for assemblies without a location during assembly discovery

## Version 1.0.0.27

* areAllParametersSet() on query now supports observables ([#655](https://github.com/dolittle/Bifrost/issues/655))
* Fixed NuGet package reference for FluentValidation to the signed version ([#654](https://github.com/dolittle/Bifrost/issues/654))
* Added a package for the MVC project support (PS: this is MVC3, so good old)

## Version 1.0.0.26

* Removing IExecutionEnvironment - introducing ICanProvideAssemblies and rewriting how things work with assemblies. Fixes [#650](https://github.com/dolittle/Bifrost/issues/650).

## Version 1.0.0.24 & 1.0.0.25

* Making it possible to override IExecutionEnvironment at startup - as a temporary solution till we get to [#650](https://github.com/dolittle/Bifrost/issues/650) & [#651](https://github.com/dolittle/Bifrost/issues/651)

## Version 1.0.0.22 & 1.0.0.23

* Improved weak delegates for Messenger
* Change to WeakDelegate for the CommandFor pipeline
* Ignoring dynamic assemblies for type discovery and assembly loading
* Started work on support for EntityFramework
* Making Proxy generation a part of core as it is needed for more than just clients
* Upgraded NHibernate support to the latest and greatest ([#640](https://github.com/dolittle/Bifrost/issues/640))
* Fixing how assemblies are loaded and making configuration use the same mechanism. It is now possible to specify assemblies in a fluent way + a new interface that can be implemented for specifying assemblies to include or ignore in discovery mechanisms (ICanSpecifyAssemblies)
* JavaScript dependency resolver now only resolves dependency instances when needed ([#646](https://github.com/dolittle/Bifrost/issues/646))
* Upgaded to .NET 4.5.1 for all projects

## Version 1.0.0.21

* Interface that you can implement to get notified when configuration is done - IWantToKnowWhenConfigurationIsDone ([#598](https://github.com/dolittle/Bifrost/issues/598))
* ICommandFor callbacks are called per instance ([#626](https://github.com/dolittle/Bifrost/issues/626))
* Explicit registration of IMessenger -> Messenger removed - marked with singleton
* Making designtime work properly without errors in Visual Studio for the ViewModel markup extension
* Making messenger use WeakReference ([#601](https://github.com/dolittle/Bifrost/issues/601))

## Version 1.0.0.19 & 1.0.0.20

Due to some strange behavior of the NuGet servers yielding error messages claiming errors in the Package metadata and also that versions where missing. We ended up releasing a couple of versions thinking our packages were corrupt. Therefor, these versions are exactly the same as 1.0.0.18.

## Version 1.0.0.18

* Added process callbacks for ICommandFor<> ([#614](https://github.com/dolittle/Bifrost/issues/614))
* Configuring messenger for desktop when configuring for desktop (Configure.Frontend.Desktop()) - as a singleton ([#617](https://github.com/dolittle/Bifrost/issues/617))

### Process Callbacks (#614)

```csharp
public class ViewModel
{
    public ViewModel(ICommandFor<MyCommand> commandFor)
    {
        commandFor.Succeeded((cmd, result) => {
            // Things to do when successful
        });

        commandFor.Failed((cmd, result) => {
            // Things to do when failed
        });

        commandFor.Handled((cmd, result) => {
            // Things to do when handled
        });
    }
}
```

## Version 1.0.0.17

* FromMethod extension in WPF/XAML client can now take an IValueConverter to use for converting parameters ([#616](https://github.com/dolittle/Bifrost/issues/616))

### ParameterConverter ([#616](https://github.com/dolittle/Bifrost/issues/616))

```xaml
<Button Command="{interaction:FromMethod NameOfMethod, CanExecuteWhen=NameOfMethodOrProperty, ParameterConverter={StaticResource AValueConverter}}"/>
````


## Version 1.0.0.16

* FromMethod extension in WPF/XAML client now supports CanExecute ([#612](https://github.com/dolittle/Bifrost/issues/612))
* ICommandFor<> can now be generated from a concrete instance of a command ([#611](https://github.com/dolittle/Bifrost/issues/611))
* Fix for assembly filtering to filter assemblies delay loaded after initial initialization ([#613](https://github.com/dolittle/Bifrost/issues/613))

### CanExecute for FromMethod

In the Interaction namespace of Bifrost when using the client, you'll find a markup extension called FromMethod. This extension now supports the capability of taking

```xaml
<Button Command="{interaction:FromMethod NameOfMethod, CanExecuteWhen=NameOfMethodOrProperty}"/>
```

The *CanExecuteWhen* property of the extension is optional and can take either a method or a property. If it is a method, the method can be a method without any parameters or with one parameter and the command parameter will then be passed in. The return type needs to be of type *bool*, same goes for if it is a property. If the declaring type implements *INotifyPropertyChanged* and you're specifying a property - it will trigger a *CanExecuteChanged* event for the returning command when the property changed so it can be evaluated by the WPF infrastructure again.


## Version 1.0.0.15

* Configure startup now uses same AssemblyProvider as the rest of the system

## Version 1.0.0.14

* ICommandFor<> support for client in place with full proxy generation of the frontend concerns. Fully bindable in XAML.

## Version 1.0.0.13

* AssemblyFiltering for startup in place. For now accessible from the DiscoverAndConfigure() method on the configure object

### Usage

For instance, lets say you have a WPF application, the way you would use this is would be in your App.xaml.cs file:

```csharp
using Bifrost.Configuration;
using Bifrost.Configuration.Assemblies;

namespace YourApp
{
    public partial class App : Application
    {
        static App()
        {
            Configure.DiscoverAndConfigure(a=>
                a.IncludeAll()
                 .ExceptAssembliesStartingWith("System","Microsoft"));
        }
    }
}
```

In the future there will be other mechanisms as well which would apply better for Web scenarios where one does not call DiscoverAndConfigure manually. Also, the sample above with "System" and "Microsoft" will eventually be something Bifrost itself will filter, this is linked to the future mechanism coming.


## Version 1.0.0.12

* XAML Visual Tree extensions
* Adding better check for wether or not an assembly is a .NET assembly during location instead of relying on BadImageException
* Temporarily disabling Windows Phone version of Client - will return in a future release
* DocumentDB strategies for collection storage - default implementation puts all entity types in same collection called entities. Query support for getting correct entities when asking for a specific type.
* Added FromMethod MarkupExtension for XAML clients for pointing directly to a method on your ViewModel for the Command property of things like Button.
* Enabling strong name signing - for same reason this was not enabled
* Introducing a ViewModel MarkupExtension for XAML clients that takes the type of ViewModel and it will use the container to instantiate it. Used for the DataContext property typically.
* Started work on a better way to filter assemblies you don't want to discover types for. Early days.
* Putting in place DesktopConfiguration for the WPF client
* Changing one of the overloads for IOC containers for Bind when binding to a Func. It now takes the type being bound to.

## Version 1.0.0.11

* Support for derived query types ([#538](https://github.com/dolittle/Bifrost/issues/538))
* Required rule more robust
* Fixed generation of Queryable extensions for .Skip and .Take ([#540](https://github.com/dolittle/Bifrost/issues/540))
* Started work on DocumentDB implementation - EventStore is up and running ([#531](https://github.com/dolittle/Bifrost/issues/531))
* Started work on new validation and business rules engine based on a new underlying rule engine
* FluentValidation moved into its own components - Bifrost.FluentValidation - available as its own NuGet package
* Improving the naive file system based EventStore and EntityContexts
* Improved QuickStart to showcase more features - this still need to be worked on a lot to actually cover all features.
* Started work on query validation support ([#382](https://github.com/dolittle/Bifrost/issues/382))
* ICanResolvePrinicpal implementation automatically discovered ([#549](https://github.com/dolittle/Bifrost/issues/549))
* Added ViewModelService for XAML based clients
* Support for SimpleInjector ([#568](https://github.com/dolittle/Bifrost/issues/568) - pull request)
* Handling of missing properties for mapping in JavaScript fixed ([#585](https://github.com/dolittle/Bifrost/issues/585) - pull request)
* Improving XML comments in general
* Added MethodInfoConverter to JSON component
* Started work on mapping support for C# - needed for DocumentDB support, amongst others
* Heavy work on the new control, binding and object model for JavaScript
* Improved isObject() check when checking "undefined" for JavaScript
* Code quality work with the help of NDepend
* Updated 3rd party references
  * Newtonsoft Json - 6.0.8
  * Ninject - 3.2.2.0
  * SignalR - 2.2.0

## Version 1.0.0.10

* Making everything compile against .net 4.5
* Updated 3rd party references
  * Newtonsoft Json - 6.0.5
  * Ninject - 3.2.0.0
  * RavenDB - 2.5.2916
  * MongoDB - 1.9.2
  * SignalR - 2.1.2
  * Unity - 3.5.1404.0
  * AutoFac - 3.5.2
* Exposing StructureMap IOC support as Nuget package
* Exposing Unity IOC support as Nuget package
* Exposing AutoFac IOC support as Nuget package
* Exposint Windsor IOC support as Nuget package
* Retiring dedicated SignalR component - merged into the Web component. Consequence is SignalR is now front and center of everything moving forward.
* Introducing a simple JSON File based EventStore implementation - mostly for demo purposes
* Introducing a simple JSON File based EntityContext implementation - mostly for demo purposes
* Cleaning up QuickStart
* Easier and more convenient way to import types ([#534](https://github.com/dolittle/Bifrost/issues/534))
* Fixing bugs related to client mapping of objects ([#524](https://github.com/dolittle/Bifrost/issues/524), [#522](https://github.com/dolittle/Bifrost/issues/522))
* Fixed deserialization of Concepts based on Guids ([#513](https://github.com/dolittle/Bifrost/issues/513))
* Fixing IE issues with mapping and type recognition. IE does not hold a property called name for the constructor property. Regex to the rescue.
* Started work on the new rule engine. First implementation targets queries and validation of these. This is the first step in moving away from FluentValidation.
* Fixing a lot of "system" properties to be prefixed with an underscore - avoiding naming conflicts
* Rules namespace holding a specification implementation is now called Specifications both in C# and JavaScript
* BundleTables routes from System.Web.Optimization are now recognized and will be ignored for the SPA HTTP request handling

### More convenient way to import instances of types - [#534](https://github.com/dolittle/Bifrost/issues/534)

In Bifrost we have something called ITypeImporter and ITypeDiscoverer. The importer imports the types as instances, that means the implementation uses the IOC container to resolve an instance when calling the .Import*<>() methods. The discoverer is responsible for just discovering the types when calling the Find*<>() methods and has nothing to do with instances. With this release, we're introducing something that makes this whole thing more explicit, clearer and easier to use. There is now an interface called IInstancesOf<> that you can take a dependency on. The interface inherits IEnumerable<> and as a consequence the implementation InstancesOf<> will do the instantiation when its enumerated. The way you can use this is:

```csharp
using Bifrost.Execution;

namespace MyNamespace
{
    public class MySystem
    {
        public MySystem(IInstancesOf<SomeAbsractOrInterfaceType> instances)
        {
            // The enumerator will now instantiate through the IOC container
            // giving you an instance with correct lifecycle
            foreach( var instance in instances )
            {
            }
        }
    }
}
```

### SignalR support

We are now generating Bifrost proxies for hubs - these are placed in the namespace corresponding to the namespace maps set up for the application. Once you have created a hub like so:

```csharp
using Microsoft.AspNet.SignalR;

namespace Web.BoudedContext.Module
{
    public class MyHub : Hub
    {
        public int DoSomething(int someInteger, string someString)
        {
            return 42;
        }
    }
}
```

From your JavaScript - for instance a ViewModel, you can now do this:

```js
Bifrost.namespace("Web.BoundedContext.Module", {
    myFeature: Bifrost.views.ViewModel.extend(function(myHub) {

        myHub.server.doSomething(43,"fourty three")
            .continueWith(function(result) {
                // Result should be 42...
        });
    });
});
```

The "myHub" dependency will automatically be injected by convention. All hubs are placed in the closest matching namespace. If none is found, there is a global namespace called hubs that will hold it. The last dependency resolver in the chain will be the one that recognizes the global namespace and it will always be found.

With this you don't have to think about starting the SignalR Hub connection like one would have to do with a bare SignalR. Bifrost manages this on the first hub being used.

You might notice that instead of what might be expected when working with SignalR for results coming back from calls to the server:

```js
.done(function() {})
````

We are using the Bifrost promise instead. This is because we want consistency with Bifrost, not with SignalRs proxies. In fact, we are not using the SignalR proxies at all, but our own proxy generation engine as we use for the generation of proxies for Commands, Queries, ReadModels, Security, Validation and more.

If you want to set up client funtions that you want for the particular Hub instances, you can do this by doing the following:

```js
myHub.client(function(client) {
    client.doSomething = function(someString, someNumber) {
        // Do things in the client called from the server
    };
});
```

This will subscribe to any method calls from the server corresponding to the name of the function. The above code can be called anytime, there is no ordering to think of as with vanilla SignalR were you have to do this prior to starting the Hub connection. 
