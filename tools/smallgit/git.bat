@REM Get path to this file
@for /f %%i in ("%0") do set curpath=%%~dpi
@SET HOME=%curpath%
@SET PATH=%HOME%\bin;%PATH%

@git %1 %2 %3 %4 %5 %6 %7 %8 %9
