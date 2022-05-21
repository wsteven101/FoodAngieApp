# FoodApp
Angular

dotnet add FoodApp.csproj package Swashbuckle.AspNetCore -v 5.6.3
dotnet add Food.Data package Microsoft.EntityFrameworkCore.SqlServer -v 5.0.0
dotnet add Food.Data package Microsoft.EntityFrameworkCore.SqlServer -v 5.0.0
dotnet add FoodApp package  Microsoft.EntityFrameworkCore.Design -v 5.0.0
dotnet add FoodApp package  Microsoft.EntityFrameworkCore.Design -v 5.0.0
dotnet add FoodApp/FoodApp.csproj package Azure.Identity
dotnet add FoodApp/FoodApp.csproj package Azure.Extensions.AspNetCore.Configuration.Secrets


dotnet ef migrations add CreateDB --project Food.Data --startup-project FoodApp


For Integration Tests
dotnet add Food.Data.IntTest package Microsoft.Extensions.Configuration.FileExtensions --version 5.0.0
dotnet add Food.Data.IntTest package Microsoft.Extensions.Configuration.Json --version 5.0.0
dotnet add Food.Data.IntTest package Microsoft.Extensions.Configuration.EnvironmentVariables --version 5.0.0
dotnet add Test.Integration/Food.Data.IntTest package  Azure.Identity

testy1
