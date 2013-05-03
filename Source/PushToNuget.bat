del *.nupkg /f /q
set nuget=solutions\.nuget\nuget.exe
set version=1.0.0.7

%nuget% pack Bifrost\Bifrost.csproj -Symbols -version %version%
%nuget% pack Bifrost.JSON\Bifrost.JSON.csproj -Symbols -version %version%
%nuget% pack Bifrost.Ninject\Bifrost.Ninject.csproj -Symbols -version %version%
%nuget% pack Bifrost.JavaScript\Bifrost.JS.nuspec -version %version%
%nuget% pack Bifrost.Web\Bifrost.Web.csproj -Symbols -version %version%
%nuget% pack Bifrost.SignalR\Bifrost.SignalR.csproj -Symbols -version %version%
%nuget% pack Bifrost.RavenDB\Bifrost.RavenDB.csproj -Symbols -version %version%
%nuget% pack Bifrost.RavenDB.Embedded\Bifrost.RavenDB.Embedded.csproj -Symbols -version %version%
%nuget% pack Bifrost.MongoDB\Bifrost.MongoDB.csproj -Symbols -version %version%
%nuget% pack Bifrost.NHibernate\Bifrost.NHibernate.csproj -Symbols -version %version%
%nuget% pack Bifrost.Mimir.Web\Bifrost.Mimir.Web.csproj -Symbols -version %version%
%nuget% pack Bifrost.Default\Bifrost.Default.nuspec -version %version%
%nuget% pack Bifrost.QuickStart\Bifrost.QuickStart.nuspec -version %version%

for %%f in (*.nupkg) do %nuget% push %%f