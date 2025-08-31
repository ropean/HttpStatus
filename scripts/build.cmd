@echo off
setlocal enabledelayedexpansion
cd /d "%~dp0.."

set SOLUTION=HttpStatus.sln
set CONFIG=Release
set PLATFORM=Any CPU

rem Determine version from latest Git tag (fallback to 1.0.0)
set GIT_TAG=
where git >nul 2>nul
if %errorlevel% equ 0 (
  for /f "usebackq delims=" %%t in (`git describe --tags --abbrev=0 2^>nul`) do set GIT_TAG=%%t
)
if not defined GIT_TAG set GIT_TAG=1.0.0

echo Using version: %GIT_TAG%

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
%MSBUILD% %SOLUTION% /m /t:Rebuild /p:Configuration=%CONFIG% /p:Platform="%PLATFORM%" /p:AssemblyVersion=%GIT_TAG% /p:FileVersion=%GIT_TAG% /p:InformationalVersion=%GIT_TAG% /nologo /v:m
exit /b %errorlevel%


