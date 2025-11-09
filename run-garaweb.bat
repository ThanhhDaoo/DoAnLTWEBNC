@echo off
echo Starting GaraAPI, GaraMVC and GaraWeb...

echo.
echo Starting GaraAPI on port 7002...
start "GaraAPI" cmd /k "cd GaraAPI && dotnet run --urls=https://localhost:7002"

timeout /t 3 /nobreak > nul

echo.
echo Starting GaraMVC on port 5001...
start "GaraMVC" cmd /k "cd GaraMVC && dotnet run --urls=https://localhost:5001"

timeout /t 3 /nobreak > nul

echo.
echo Starting GaraWeb on port 5005...
start "GaraWeb" cmd /k "cd GaraWeb && dotnet run --urls=https://localhost:5005"

echo.
echo All applications are starting...
echo GaraAPI: https://localhost:7002
echo GaraMVC (Admin): https://localhost:5001
echo GaraWeb (User): https://localhost:5005
echo.
echo Press any key to exit...
pause > nul
