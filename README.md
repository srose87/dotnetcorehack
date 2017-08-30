# dotnetcorehack
[![Build Status](https://travis-ci.org/dlogankeenan/dotnetcorehack.svg?branch=master)](https://travis-ci.org/dlogankeenan/dotnetcorehack)

A very basic API to get back into things with C#

**I'm abandoning this project because it takes 6 seconds on my fully loaded MBP to build and run 84 tests. That is not acceptable and I do not see C# as a viable option for building testable APIs.**

## Prerequisites for Running and Writing Code
1. Install [.Net Core](https://www.microsoft.com/net/download/core)
2. Install [Install .NET Core SDK](https://www.microsoft.com/net/core#macos)
3. Install [Visual Studio Code](https://code.visualstudio.com/download)
4. Install the csharp extension for Visual Studio code within the IDE.

## Run the app
1. `dotnet restore` - this will download all the dependnecies
2. `dotnet run` - within `/dotnetcorehack` dir will start the app
3. JSON request using Postman can be made to `http://localhost:5000/contacts`
4. Run tests with `dotnet test dotnetcorehack.Test/dotnetcorehack.Test.csproj`

## Code Linting
This project utilizes [StyleCop](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) to ensure code is correct and consistent between developers.  All linting rules are disabled while tests run or the project is being built in debug mode (`dotnet build` defaults to debug).  Linting will occur during a release build `dotnet build -c release`.
