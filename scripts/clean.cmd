@echo off
setlocal
cd /d "%~dp0.."

set SOLUTION=HttpStatus.sln
set CONFIG=Release
set PLATFORM=Any CPU

rem Prefer MSBuild clean for correctness
set VSWHERE="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if exist %VSWHERE% (
  for /f "usebackq delims=" %%i in (`%VSWHERE% -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do set MSBUILD="%%i"
)

if defined MSBUILD (
  echo Cleaning via MSBuild...
  %MSBUILD% %SOLUTION% /t:Clean /p:Configuration=%CONFIG% /p:Platform="%PLATFORM%" /nologo /v:m
) else (
  echo MSBuild not found via vswhere.
)

rem Ensure directories are removed regardless of MSBuild availability
if exist HttpStatus\bin rmdir /s /q HttpStatus\bin
if exist HttpStatus\obj rmdir /s /q HttpStatus\obj

exit /b %errorlevel%


