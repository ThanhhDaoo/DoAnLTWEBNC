@echo off
echo Starting Gara Management System...
echo.

echo Starting API...
start "GaraAPI" cmd /k "cd GaraAPI && dotnet run"

timeout /t 3 /nobreak > nul

echo Starting MVC...
start "GaraMVC" cmd /k "cd GaraMVC && dotnet run"

echo.
echo Both applications are starting...
echo API: https://localhost:7000
echo MVC: https://localhost:7001
echo.
echo Press any key to exit...
pause > nul

