@echo off
rem Purpose of this is to clean out the bin, obj folders - in fact complete delete them!
rem Clean Build in Visual Studio does not do this, and sometimes it is necessary to do that properly!

for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s/q "%%d"