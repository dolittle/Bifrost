@echo off
set nuget=solutions\.nuget\nuget.exe

set version=%1

%nuget% pack Bifrost\Bifrost.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.FluentValidation\Bifrost.FluentValidation.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.CommonServiceLocator\Bifrost.CommonServiceLocator.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.Client.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.JSON\Bifrost.JSON.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.AutoFac\Bifrost.AutoFac.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.Ninject\Bifrost.Ninject.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.SimpleInjector\Bifrost.SimpleInjector.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.StructureMap\Bifrost.StructureMap.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.Unity\Bifrost.Unity.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.Windsor\Bifrost.Windsor.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.JavaScript\Bifrost.JS.nuspec -Version %version%
%nuget% pack Bifrost.Web\Bifrost.Web.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.Web.Mvc\Bifrost.Web.Mvc.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.RavenDB\Bifrost.RavenDB.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.MongoDB\Bifrost.MongoDB.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.DocumentDB\Bifrost.DocumentDB.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.NHibernate\Bifrost.NHibernate.nuspec -Symbols -Version %version%
%nuget% pack Bifrost.Mimir.Web\Bifrost.Mimir.nuspec -Version %version%
%nuget% pack Bifrost.Default\Bifrost.Default.nuspec -Version %version%
%nuget% pack Bifrost.QuickStart\Bifrost.QuickStart.nuspec -Version %version%
%nuget% pack Bifrost.MSpec\Bifrost.MSpec.nuspec -Version %version%