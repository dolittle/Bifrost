@echo off
del *.nupkg /f /q
call CreateBifrostPackages.bat %1

for %%f in (*.nupkg) do %nuget% push %%f
