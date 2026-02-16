
# REST API Automation Tests

This project contains automated REST API tests built using .NET and xUnit.

'xunit.runner.json' is setup to execute collections (Device Positive Tests, Device Negative Tests) in parallel.

'appsettings.json, appsettings.json' contain environment specific configurations.
appsettings.json is used by default for execution.

'Tests\DeviceNegativeTests.cs' - contains negative test cases

'Tests\DevicePositiveTests.cs' - contains positive test cases

'Fixtures\ApiFixture.cs' - creates and end REST API session before and after test collection execution.

'Dto' - folder contains DTOs used for communications with REST API endpoint.

'DtoConverter\UnixTimestampConverter.cs' - converts UNIX timestamp in milliseconds to DateTime and vice versa.

'Schemas\device.schema.json' - used to execute JSON schema validation test case.

## Tech Stack
- .NET 10
- xUnit 2.9.3

## Running Tests
```bash
dotnet test --logger "xunit;LogFilePath=TestResults/results.xml"
```



