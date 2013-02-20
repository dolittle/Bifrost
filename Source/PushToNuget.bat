nuget pack Bifrost\Bifrost.nuspec -Symbols
nuget pack Bifrost.JSON\Bifrost.JSON.nuspec -Symbols
nuget pack Bifrost.Ninject\Bifrost.Ninject.nuspec -Symbols
nuget pack Bifrost.Web\Bifrost.Web.nuspec -Symbols
nuget pack Bifrost.SignalR\Bifrost.SignalR.nuspec -Symbols
nuget pack Bifrost.RavenDB\Bifrost.RavenDB.nuspec -Symbols
nuget pack Bifrost.RavenDB.Embedded\Bifrost.RavenDB.Embedded.nuspec -Symbols
nuget pack Bifrost.MongoDB\Bifrost.MongoDB.nuspec -Symbols
nuget pack Bifrost.NHibernate\Bifrost.NHibernate.nuspec -Symbols
nuget pack Bifrost.Mimir.Web\Bifrost.Mimir.nuspec -Symbols

for %%f in (*.nupkg) do nuget push %%f

