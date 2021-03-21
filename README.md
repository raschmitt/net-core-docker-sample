# .Net Core - Docker Sample

[![Azure DevOps builds (stage)](https://img.shields.io/azure-devops/build/raschmitt/7618d927-8467-43e2-b5e9-1aeddc1fbfdc/26?label=Continuous%20Integration&stage=CI)](https://dev.azure.com/raschmitt/raschmitt/_build?definitionId=26)
[![Azure DevOps builds (stage)](https://img.shields.io/azure-devops/build/raschmitt/7618d927-8467-43e2-b5e9-1aeddc1fbfdc/26?label=Continuous%20Delivery&stage=CD)](https://dev.azure.com/raschmitt/raschmitt/_build?definitionId=26)
[![Sonar Coverage](https://img.shields.io/sonar/coverage/raschmitt_net-core-docker-sample?label=Code%20coverage&server=https%3A%2F%2Fsonarcloud.io&style=flat-square)](https://sonarcloud.io/dashboard?id=raschmitt_net-core-docker-sample)
[![Mutation testing badge](https://img.shields.io/endpoint?style=flat-square&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2Fraschmitt%2Fnet-core-docker-sample%2Fmaster)](https://dashboard.stryker-mutator.io/reports/github.com/raschmitt/net-core-docker-sample/master)
[![Docker Image Version (tag latest semver)](https://img.shields.io/docker/v/raschmitt/net-core-docker-sample/latest?label=Latest%20Image&style=flat-square)](https://hub.docker.com/repository/docker/raschmitt/net-core-docker-sample)



Sample .Net Core API, with SQL Server Database, containerized with Docker and ready to use.

## Dependencies 

- [Docker](https://docs.docker.com/get-docker/)

## Live preview [![Website](https://img.shields.io/website?down_message=Unhealthy&label=Health%20Check&up_message=Healthy&url=https%3A%2F%2Fnet-core-docker-sample-service-raschmitt.cloud.okteto.net%2Fhealth)](https://net-core-docker-sample-service-raschmitt.cloud.okteto.net/health)

A live preview of this API can be found [here](https://net-core-docker-sample-service-raschmitt.cloud.okteto.net/swagger/index.html).

## How to run locally

- ### Running the API 

1. After cloning this repository go into the `NetCoreDockerSample` directory and run `docker-compose up`.

2. Access [http://localhost:8080/swagger](http://localhost/swagger) and you are good to start playing with this API.

- ### Connecting to the container's database

If you wish to connect to the container's database you can do so with the following credentials:

| Parameter | Value |
| :---: | :---: |
| Server name | 127.0.0.1,1433 |
| Login | sa |
| Paswword | sa@@2020 |

## How to debug 

- [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/containers/edit-and-refresh?view=vs-2019)
- [Visual Studio Code](https://code.visualstudio.com/docs/containers/debug-netcore)
- [Rider](https://blog.jetbrains.com/dotnet/2018/07/18/debugging-asp-net-core-apps-local-docker-container/)
 
## Contributions

  Contributions and feature requests are always welcome.
