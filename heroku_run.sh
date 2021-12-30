#!/bin/sh

ASPNETCORE_URLS=http://*:${PORT:-80}

echo "Running on URL: $ASPNETCORE_URLS"
dotnet KubakLandingApi.dll