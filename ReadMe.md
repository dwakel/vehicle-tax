### Vehicle Tax

Provides basic tax/duty information about types and categories of vehicles you can import into the country. Also calculates a vehicles import duty/tax. This is based on real information provided by the GRA Customs Ghana

#### 🚀 Preriquisits
- Docker
- Internet connection
- Docker Compose


#### 🚀 How To Test
Extract zip into Default bash directory.
In a separate terminal window (2 terminals) RUN the following commads:

#### Terminal 1: Human resource application
``` Bash
cd vehicle-tax

sh scripts/create.sh

```
Open Visual studio and run the project or run:

``` Bash
dotnet run --project VehicleTracker/VehicleTracker.csproj

```
Create an account and link the account to your existing account tracker accounts



#### Relevant commands (must cd into project directory)
Run Migrations
``` Bash
sh scripts/migrate.sh apply

```

RoleBack Migrations
``` Bash
sh scripts/migrate.sh rollback

```

Destroy Database
``` Bash
sh scripts/destroy.sh

```

Start Database
``` Bash
sh scripts/start.sh

```

-Migrations (database scripts) can be found in the migration directory within project directory
- Migration are written in raw sql



#### Point to note
Make sure application are run in the specified ports in the launch.json file
- The application should be run using kestrel

