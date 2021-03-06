REM Called from AfterBuild Step within csproj
@ECHO === === === === === === === ===
SET EnableNuGetPackageRestore=true
SET NUGET=..\..\..\..\InstallSupport\NuGet.exe
SET OUTDIR=C:\SourceCode\
SET VER=%1

IF NOT EXIST %OUTDIR%\NUL SET OUTDIR=%TEMP%

IF NOT EXIST %OUTDIR%\NUL OUTDIR=%TEMP%

@ECHO ===NUGET Publishing Version %VER% to %OUTDIR%
%NUGET% pack -Symbols -Version %VER% TestCop.nuspec
%NUGET% push Resharper.TestCop.R9.%VER%.symbols.nupkg -ApiKey XXX -Source %OUTDIR%
@ECHO === === === === === === === ===
