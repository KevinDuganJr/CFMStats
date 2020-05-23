@echo off

REM This script is only for local development; it runs the Pre-Publish scripts on the local machine
REM Otherwise, this is handled within Azure DevOps by using SQLCMD directly
REM To run, navigate to the folder that this file is stored in and run the file directly

SET DATABASE_ENVIRONMENT=local
SET DATABASE_PUBLISH_PROFILE=CFMStats.Database.publish.xml
SET DATABASE_SERVER=(local)\CFMStats
SET DATABASE_CATALOG=CFMStats-Dev

REM Checking for SQL Package locations (starting with lowest version first, as we prefer to use latest)

SET SQLCMD_130=C:\Program Files (x86)\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\SQLCMD.exe
SET SQLCMD_170=C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.exe

if exist "%SQLCMD_130%" (
    SET SQLCMD="%SQLCMD_130%"
)

if exist "%SQLCMD_170%" (
    SET SQLCMD="%SQLCMD_170%"
)

REM Executing script

SET PREPUBLISH_SCRIPT_PATH=\Scripts\PrePublish
SET SHARED_SCRIPT_PATH=\Scripts\Shared

%SQLCMD% -S "%DATABASE_SERVER%" -d "%DATABASE_CATALOG%" -i ".%PREPUBLISH_SCRIPT_PATH%\Index.sql" -v Environment ="%DATABASE_ENVIRONMENT%" -v path ="%cd%%PREPUBLISH_SCRIPT_PATH%" -v shared ="%cd%%SHARED_SCRIPT_PATH%" -v overwritedataloads =0

pause