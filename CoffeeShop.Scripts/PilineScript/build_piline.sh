#!/bin/bash
# Install Node.js
npm install --quite

# Minify javascripts
gulp

# Install lastest .Net package
dotnet restore

# Build project file
dotnet build --configuration Debug

# Publish
dotnet publish --no-build --configuration Debug --output /tmp/Debug

# Build app under release
dotnet build --configuration Release

# Publish
dotnet publish --no-build --configuration Release --output /tmp/Release
