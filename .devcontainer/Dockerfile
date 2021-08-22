# See here for image contents: https://github.com/microsoft/vscode-dev-containers/tree/v0.191.0/containers/dotnet/.devcontainer/base.Dockerfile

ARG VARIANT="5.0"
FROM mcr.microsoft.com/vscode/devcontainers/dotnet:0-${VARIANT}

# Setup tooling for python
RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
    && apt-get -y install --no-install-recommends python3-pip

# Install 3rd party tools
RUN pip3 install pre-commit

# Environment variables setup for user
ENV PATH="/home/vscode/.dotnet/tools:${PATH}"
