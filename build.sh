#!/bin/bash

# Borrowed and inspired from Akka.net : https://github.com/akkadotnet/akka.net

SCRIPT_PATH="${BASH_SOURCE[0]}";
if ([ -h "${SCRIPT_PATH}" ]) then
  while([ -h "${SCRIPT_PATH}" ]) do SCRIPT_PATH=`readlink "${SCRIPT_PATH}"`; done
fi
pushd . > /dev/null
cd `dirname ${SCRIPT_PATH}` > /dev/null
SCRIPT_PATH=`pwd`;
popd  > /dev/null

NUGET_PATH="$SCRIPT_PATH/Source/Solutions/.nuget";
PACKAGES_PATH="$SCRIPT_PATH/Source/Solutions/packages";

if ! [ -f $NUGET_PATH/.nuget/nuget.exe ] 
    then
        wget "https://www.nuget.org/nuget.exe" -P $$NUGET_PATH/
fi

mono $NUGET_PATH/NuGet.exe update -self
mono $NUGET_PATH/NuGet.exe install FAKE -OutputDirectory $PACKAGES_PATH -ExcludeVersion -Version 4.16.1
mono $NUGET_PATH/NuGet.exe install FSharp.Data -OutputDirectory $PACKAGES_PATH/FAKE -ExcludeVersion -Version 2.3.2

export encoding=utf-8

mono $PACKAGES_PATH/FAKE/tools/FAKE.exe build.fsx "$@"