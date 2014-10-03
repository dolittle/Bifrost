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
- Unity
- Windsor

All available from Nuget:

install-package Bifrost.AutoFac
install-package Bifrost.StructureMap
install-package Bifrost.Unity
install-package Bifrost.Windsor

If you want to implement support for your own, you need to implement the 
interface called IContainer sitting in the Bifrost.Execution namespace.