@echo off
setlocal enabledelayedexpansion

REM Check if there are any JSON files in the current directory
dir /b *.json >nul 2>&1
if errorlevel 1 (
    echo No JSON files found in the current directory.
    pause
    exit /b 1
)

REM Find an available filename
set "base_name=new"
set "extension=.eaapi"
set "counter=0"
set "filename=%base_name%%extension%"

REM Check if base filename exists, increment counter if needed
if exist "%filename%" (
    set /a counter=1
    set "filename=%base_name%!counter!%extension%"
    
    :check_loop
    if exist "!filename!" (
        set /a counter+=1
        set "filename=%base_name%!counter!%extension%"
        goto check_loop
    )
)

echo Creating archive: %filename%
echo.

REM Create a temporary zip file first
set "temp_zip=%temp%\temp_archive_%random%.zip"
powershell -NoProfile -Command "Compress-Archive -Path '*.json' -DestinationPath '%temp_zip%' -CompressionLevel Optimal"

if errorlevel 1 (
    echo Error: Failed to create archive.
    pause
    exit /b 1
)

REM Move and rename the zip to .eaapi
move "%temp_zip%" "%filename%" >nul

if errorlevel 1 (
    echo Error: Failed to rename archive to .eaapi format.
    del "%temp_zip%" 2>nul
    pause
    exit /b 1
)

echo.
echo Success! Created %filename%
echo.

REM Count and display the number of JSON files archived
for /f %%A in ('dir /b *.json ^| find /c /v ""') do set json_count=%%A
echo Archived %json_count% JSON file(s).

pause