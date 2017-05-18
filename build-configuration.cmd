@echo off

%FAKE% %NYX% "target=clean" -st
%FAKE% %NYX% "target=RestoreNugetPackages" -st


IF NOT [%1]==[] (set RELEASE_NUGETKEY="%1")

SET SUMMARY="Elders.Cronus.Projections.Replay.Common"
SET DESCRIPTION="Elders.Cronus.Projections.Replay.Common"

%FAKE% %NYX% appName=Elders.Cronus.Projections.Replay.Common          appSummary=%SUMMARY% appDescription=%DESCRIPTION% nugetPackageName=Cronus.Projections.Replay.Common nugetkey=%RELEASE_NUGETKEY%