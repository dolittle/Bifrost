rem @echo off
call CreateTestResultsDir.cmd
rem @echo on
..\Tools\Forseti\Forseti.Output.exe "..\Source\Forseti.yaml" "..\TestResults\forseti.testresults.trx" "BUILD-CI"
rem @echo off