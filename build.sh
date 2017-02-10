#!/usr/bin/env bash

#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then  
  rm -R $artifactsFolder
fi

cd src/Gameboard_DAL
dotnet restore
dotnet build

cd ../Gameboard
dotnet restore
dotnet build

cd ../../test/Gameboard_Tests
dotnet restore
dotnet build

revision=${TRAVIS_JOB_ID:=1}  
revision=$(printf "%04d" $revision) 