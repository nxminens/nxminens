#!/bin/bash
dotnet.exe publish -r win-x86
dotnet.exe publish -r win-x64
dotnet.exe publish -r linux-x64
dotnet.exe publish -r osx-x64
