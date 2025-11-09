@echo off
echo Stopping GaraAPI if running...
taskkill /F /IM GaraAPI.exe 2>nul
timeout /t 2 /nobreak >nul

echo Rebuilding GaraAPI...
cd GaraAPI
dotnet build GaraAPI.csproj
cd ..

echo.
echo Please restart GaraAPI and GaraWeb now.
echo Then refresh the browser with Ctrl+F5 to clear cache.
pause

