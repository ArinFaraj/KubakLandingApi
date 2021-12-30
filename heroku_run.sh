#!/bin/sh

ASPNETCORE_URLS=http://*:${PORT:-80}
dotnet KubakLandingApi.dll