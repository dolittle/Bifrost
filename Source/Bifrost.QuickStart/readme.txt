Configurator
============
All configuration is done through implementors of the ICanConfigure interface. 
You can have multiple implementations of these. The Configurator.cs contains 
the configuration for the application and is setup for you to just run as is.

ContainerCreator
================
The ContainerCreator is responsible for setting up the IOC container. It 
implements the ICanCreateContainer interface which is automatically discovered 
by Bifrost during startup.

The default configuration for an IOC container in this package is Ninject.

There is support for the following IOC containers in addition to Ninject:

- AutoFac
- StructureMap

All available from Nuget:

install-package Bifrost.AutoFac
install-package Bifrost.StructureMap

If you want to implement support for your own, you need to implement the 
interface called IContainer sitting in the Bifrost.Execution namespace.

Web
===
As you'll notice in this project there already is a full sample implemented.
It all starts with the index.html file. Notice the <div/> with the 
data-navigation-frame attribute on it, it points to a page composing the
feature in this sample. 