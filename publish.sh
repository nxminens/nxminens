#!/bin/bash
dotnet.exe publish -c Release -r win-x86
dotnet.exe publish -c Release -r win-x64
dotnet.exe publish -c Release -r linux-x64
dotnet.exe publish -c Release -r osx-x64
