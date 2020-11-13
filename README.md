# Samples.RocketLaunches
Sample api for displaying simple aggregate metric data over simple rocket launch/company/location data

## [Build Test Run](#build-test-run)

* Download and install dotnet core 3.1 LTS
  * Mac: <https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.100-macos-x64-installer>

  * Linux instructions: <https://docs.microsoft.com/dotnet/core/install/linux-package-managers>

  * Windows: <https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.100-windows-x64-installer>

* Clone code and build:
```bash
git clone https://github.com/boydc7/Samples.RocketLaunches
cd Samples.RocketLaunches
dotnet publish -c Release -o publish Samples.RocketLaunches.Api/Samples.RocketLaunches.Api.csproj
```
* Run (from Samples.RocketLaunches folder from above)
```bash
cd publish
dotnet rlapi.dll
```

The api will start and by default listen for requests on localhost:8084.

Once started, you can view the Swagger for the API at <http://localhost:8084 />

## [Authentication](#authentication)

No authentication implemented, public/open api

## [Endpoints](#endpoints)

See swagger at <http://localhost:8084 /> after starting the API, from where you can view endpoints and execute requests/get results inline.
