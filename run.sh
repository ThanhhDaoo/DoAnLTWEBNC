#!/bin/bash

echo "Starting Gara Management System..."
echo

echo "Starting API..."
gnome-terminal -- bash -c "cd GaraAPI && dotnet run; exec bash"

sleep 3

echo "Starting MVC..."
gnome-terminal -- bash -c "cd GaraMVC && dotnet run; exec bash"

echo
echo "Both applications are starting..."
echo "API: https://localhost:7000"
echo "MVC: https://localhost:7001"
echo
echo "Press any key to exit..."
read -n 1

