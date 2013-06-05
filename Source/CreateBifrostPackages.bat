@echo off
set nuget=solutions\.nuget\nuget.exe

set version=%1

%nuget% pack Bifrost\Bifrost.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.JSON\Bifrost.JSON.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.Ninject\Bifrost.Ninject.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.JavaScript\Bifrost.JS.nuspec -Version %version%
%nuget% pack Bifrost.Web\Bifrost.Web.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.SignalR\Bifrost.SignalR.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.RavenDB\Bifrost.RavenDB.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.RavenDB.Embedded\Bifrost.RavenDB.Embedded.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.MongoDB\Bifrost.MongoDB.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.NHibernate\Bifrost.NHibernate.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.Mimir.Web\Bifrost.Mimir.nuspec -Version %version%
%nuget% pack Bifrost.Default\Bifrost.Default.nuspec -Version %version%
%nuget% pack Bifrost.QuickStart\Bifrost.QuickStart.nuspec -Version %version%
