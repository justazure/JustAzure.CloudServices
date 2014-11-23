SET LogPath=%LogFileDirectory%%LogFileName%
ECHO Log Path: %LogPath% >> "%TEMP%\StartupLog.txt" 2>&1

PUSHD ..
ECHO Current Role: %RoleName% >> "%LogPath%" 2>&1
ECHO Current Role Instance: %InstanceId% >> "%LogPath%" 2>&1
ECHO Current Directory: %CD% >> "%LogPath%" 2>&1

ECHO We will first verify if startup has been executed before by checking %RoleRoot%\StartupComplete.txt. >> "%LogPath%" 2>&1

IF EXIST "%RoleRoot%\StartupComplete.txt" (
	ECHO Startup has already run, skipping. >> "%LogPath%" 2>&1
	EXIT /B 0
)

PowerShell -Version 2.0 -ExecutionPolicy Unrestricted .\Startup.ps1 >> "%LogPath%" 2>&1

IF %ERRORLEVEL% EQU -393216 (
   PowerShell -Command "Set-ExecutionPolicy Unrestricted" >> "%LogPath%" 2>&1
   PowerShell .\Startup.ps1 >> "%LogPath%" 2>&1
)

IF ERRORLEVEL EQU 0 (
   ECHO Startup completed. >> "%LogPath%" 2>&1
   ECHO Startup completed. >> "%TEMP%\StartupComplete.txt" 2>&1
   EXIT /B 0
) ELSE (
   ECHO An error occurred. The ERRORLEVEL = %ERRORLEVEL%.  >> "%LogPath%" 2>&1
   EXIT %ERRORLEVEL%
)
