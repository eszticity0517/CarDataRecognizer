# README #
Setup, coding guidelines, deployment ...

## Set up local environment
.NET Core is cross-platform and no restrictions were included into the code, I myself developed it on Win 10 OS.

1. Download Visual Studio Community 2019 along with .NET 5.0
2. When installing, select .NET cross platform addition
3. Type "dotnet --version" in the terminal to check if the version matches the above

## Database
Since MySQL is the integrated one, please use XAMPP: https://www.apachefriends.org/index.html

## Migrations
1. Change field types / names in entities
2. dotnet ef migrations add [InitialCreate or other name you choose]
3. Start the app, if no changes, run in root folder: dotnet ef database update
4. In the Migrations folder, now you can see your newest changes, and the __EFMigrationsHistory database table now contains a new entity as well

### Deployment
dotnet publish -c Release -r win-x64 --output ./bin CarDataRecognizer.sln

Now the /bin folder is filled with new content.

### Developer guideline ###

* Start method names with capital letter
* Avoid using var, use direct types instead (p.es. string)
