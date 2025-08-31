@echo off
setlocal enabledelayedexpansion
cd /d "%~dp0.."

set SOLUTION=HttpStatus.sln
set CONFIG=Release
set PLATFORM=Any CPU

rem Find vswhere
set VSWHERE="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if not exist %VSWHERE% (
  echo vswhere not found at %VSWHERE%
  echo Ensure Visual Studio Build Tools are installed.
  exit /b 1
)

for /f "usebackq delims=" %%i in (`%VSWHERE% -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do set MSBUILD="%%i"
if not defined MSBUILD (
  echo MSBuild not found via vswhere.
  exit /b 1
)

echo Using MSBuild: %MSBUILD%

rem Restore packages (packages.config style)
where nuget >nul 2>nul
if %errorlevel% neq 0 (
  echo nuget.exe not found in PATH. Skipping explicit restore.
) else (
  echo Restoring NuGet packages...
  nuget restore %SOLUTION% -NonInteractive
  if %errorlevel% neq 0 exit /b %errorlevel%
)

echo Building %SOLUTION% (%CONFIG% ^| %PLATFORM%)...
%MSBUILD% %SOLUTION% /m /t:Rebuild /p:Configuration=%CONFIG% /p:Platform="%PLATFORM%" /nologo /v:m
exit /b %errorlevel%


