# .Net Core - Docker Sample

[![Azure DevOps builds](https://img.shields.io/azure-devops/build/raschmitt/7618d927-8467-43e2-b5e9-1aeddc1fbfdc/22?label=Build%20%26%20Test&style=flat-square)](https://dev.azure.com/raschmitt/raschmitt/_build?definitionId=22)
[![Sonar Coverage](https://img.shields.io/sonar/coverage/raschmitt_net-core-docker-sample?label=Code%20Coverage&server=https%3A%2F%2Fsonarcloud.io&style=flat-square)](https://sonarcloud.io/dashboard?id=raschmitt_net-core-docker-sample)

Sample .Net Core API, with SQL Server Database, containerized with Docker and ready to use.

## Project Dependencies 

- [Docker](https://docs.docker.com/get-docker/)

## How to run

- ### Running the API 

1. After cloning this repository go into the `NetCoreDockerSample` directory and run `docker-compose up`.

2. Access [http://localhost/swagger](http://localhost/swagger) and you are good to start playing with this API.

- ### Connecting to the container's database

If you wish to connect to the container's database with [mssql-cli](https://docs.microsoft.com/en-us/sql/tools/mssql-cli?view=sql-server-ver15), [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/sql-server-management-studio-ssms?view=sql-server-ver15), or any othe database management tools you can do so with the following credentials:

| Parameter | Value |
| :---: | :---: |
| Server name | 127.0.0.1,1433 |
| Login | sa |
| Paswword | sa@2020 |
 
## Contributions

  Contributions and feature requests are always welcome.
