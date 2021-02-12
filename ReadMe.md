### Vehicle Tax

Provides basic tax/duty information about types and categories of vehicles you can import into the country. Also calculates a vehicles import duty/tax. This is based on real information provided by the GRA Customs Ghana

ACT 891 CUSTOMS ACT, 2015

#### 🚀 Preriquisits
- Docker
- Internet connection
- Docker Compose


#### 🚀 How To Test
Extract zip into Default bash directory.
In a separate terminal window (2 terminals) RUN the following commads:

#### Terminal
``` Bash
cd vehicle-tax

sh scripts/create.sh

```
Open Visual studio and run the project or run:

``` Bash
dotnet run --project VehicleTracker/VehicleTracker.csproj

```
Create an account and link the account to your existing account tracker accounts


#### How to Use Search And Sort on /api/Vehicle/TaxInformation/SearchSort

Type in your Search or sort parameter into the searchBy{} or sortBy{}
eg. searchBy {"typeName": "ambulance"}
sortBy {"importDuty": "asc"}  (either "asc",ascending or "desc", descending)

### List of Searchable Parametes
- categoryDescription
- typeName
- typeDescription
- categoryName
- importduty"
- vat
- nhil,
- getfundlevy,
- aulevy
- ecowaslevy
- eximlevy
- examlevy
- processingfee
- specialimportlevy

### List of Sortable Parametes
- importduty"
- vat
- nhil,
- getfundlevy,
- aulevy
- ecowaslevy
- eximlevy
- examlevy
- processingfee
- specialimportlevy


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
- To build Main App from doccker compose, uncomment configurations in docker-compose.yml file

