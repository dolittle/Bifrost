del *.nupkg /s /y

set version=1.0.0.1

nuget pack Bifrost\Bifrost.nuspec -Symbols -version %version%
nuget pack Bifrost.JSON\Bifrost.JSON.nuspec -Symbols -version %version%
nuget pack Bifrost.Ninject\Bifrost.Ninject.nuspec -Symbols -version %version%
nuget pack Bifrost.JavaScript\Bifrost.JS.nuspec -version %version%
nuget pack Bifrost.Web\Bifrost.Web.nuspec -Symbols -version %version%
nuget pack Bifrost.SignalR\Bifrost.SignalR.nuspec -Symbols -Properties version=%version%
nuget pack Bifrost.RavenDB\Bifrost.RavenDB.nuspec -Symbols -version %version%
nuget pack Bifrost.RavenDB.Embedded\Bifrost.RavenDB.Embedded.nuspec -Symbols -version %version%
nuget pack Bifrost.MongoDB\Bifrost.MongoDB.nuspec -Symbols -version %version%
nuget pack Bifrost.NHibernate\Bifrost.NHibernate.nuspec -Symbols -version %version%
nuget pack Bifrost.Mimir.Web\Bifrost.Mimir.nuspec -Symbols -Properties version=%version%

for %%f in (*.nupkg) do nuget push %%f