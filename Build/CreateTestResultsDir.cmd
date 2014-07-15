@Echo off
if not exist "..\TestResults\" (
	@echo Creating TestResults directory
	mkdir "..\TestResults\"
)