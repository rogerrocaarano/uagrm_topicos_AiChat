#!/bin/bash

# install dotnet sdk version 6.0
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 6.0
# add dotnet to path
export DOTNET_ROOT="$HOME/.dotnet"
export PATH="$PATH:$HOME/.dotnet:$HOME/.dotnet/tools"

# install entity framework tools
dotnet tool install --global dotnet-ef