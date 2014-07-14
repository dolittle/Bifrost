@echo off
call CreateTestResultsDir.cmd
@echo on
..\Tools\Forseti\Forseti.TRX.exe "..\Source\Forseti.yaml" "..\TestResults\forseti.testresults.trx" "BUILD-CI"
@echo off