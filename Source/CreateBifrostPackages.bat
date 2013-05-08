set nuget=solutions\.nuget\nuget.exe

%nuget% pack Bifrost\Bifrost.csproj -Symbols 
%nuget% pack Bifrost.JSON\Bifrost.JSON.csproj -Symbols 
%nuget% pack Bifrost.Ninject\Bifrost.Ninject.csproj -Symbols 
%nuget% pack Bifrost.JavaScript\Bifrost.JavaScript.csproj 
%nuget% pack Bifrost.Web\Bifrost.Web.csproj -Symbols 
%nuget% pack Bifrost.SignalR\Bifrost.SignalR.csproj -Symbols 
%nuget% pack Bifrost.RavenDB\Bifrost.RavenDB.csproj -Symbols 
%nuget% pack Bifrost.RavenDB.Embedded\Bifrost.RavenDB.Embedded.csproj -Symbols 
%nuget% pack Bifrost.MongoDB\Bifrost.MongoDB.csproj -Symbols 
%nuget% pack Bifrost.NHibernate\Bifrost.NHibernate.csproj -Symbols 
%nuget% pack Bifrost.Mimir.Web\Bifrost.Mimir.Web.csproj -Symbols 