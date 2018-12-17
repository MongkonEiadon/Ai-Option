# mssql-server-linux
# Maintainers: Microsoft Corporation (LuisBosquez and twright-msft on GitHub)
# GitRepo: https://github.com/Microsoft/mssql-docker

# Base OS layer: Latest Ubuntu LTS.
FROM microsoft/mssql-server-linux:latest

LABEL maintainer "Mongkon eiadon"

# Default SQL Server TCP/Port.
EXPOSE 1433

# Copy all SQL Server runtime files from build drop into image.
# COPY ./install /

# Run SQL Server process.
CMD [ "sqlcmd", "--version" ]