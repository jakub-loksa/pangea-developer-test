# Pangea BE Developer Test

## Solution

This solution contains an API project `DiffChecker.API` which should be the startup project. After opening, you can see the entire OpenAPI definition.

Since there is a need for persistency, I added Entity Framework with an InMemory database for easy testing. Note, the InMemory database clears after each shutdown.

The architecture is spread out across an API, multiple class libraries handling different parts of the code and a test project.

## Tests

If you use Visual Studio, just open 'Test Explorer' and run the tests. They should work as-is.

There are 3 types of tests:
1. Unit tests of the diff checking algorithm
2. Integration tests of the APIs
3. AutoMapper tests to validate configuration