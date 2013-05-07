set nuget=solutions\.nuget\nuget.exe

set version=%1

%nuget% pack Bifrost.Default\Bifrost.Default.nuspec -Version %version%
%nuget% pack Bifrost.QuickStart\Bifrost.QuickStart.nuspec -Version %version%