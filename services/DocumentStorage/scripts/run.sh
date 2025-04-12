#!/bin/bash

dotnet restore --project /src/DocumentStorage.csproj
dotnet ef database update --project /src/DocumentStorage.csproj --startup-project ../src/DocumentStorage.csproj
dotnet run --project /src/DocumentStorage.csproj