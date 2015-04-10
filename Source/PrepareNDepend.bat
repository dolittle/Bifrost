@echo off
if not exist NDependOut md NDependOut
if not exist NDependOut\bin md NDependOut\bin
del NDependOut\bin\*.* /q
for /f "eol=: delims=" %%d in ('dir /b Bifrost.* ^| find /v /i "Specs"') do (
	if exist %%d\bin\debug\%%d.dll (
		echo Prepared %%d 
		copy %%d\bin\debug\%%d.dll NDependOut\bin /y >nul
		copy %%d\bin\debug\%%d.pdb NDependOut\bin /y >nul
	)
)
