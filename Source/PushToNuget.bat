del *.nupkg /f /q

call CreateBifrostPackages.bat
call CreateExamplePackages.bat 1.0.0.7



@ECHO for %%f in (*.nupkg) do %nuget% push %%f