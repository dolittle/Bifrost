@echo off
call CreateTestResultsDir.cmd
@echo on
..\Tools\Forseti\Forseti.Output.exe "..\Source\Forseti.yaml" "..\TestResults\forseti.testresults.trx" "BUILD-CI"
@echo off