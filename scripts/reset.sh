

echo ":: reset database"

export CONNSTR="Host=localhost;Username=gra;Port=5409;Password=1;Database=gra"
./scripts/migrator.sh rollback
./scripts/migrator.sh apply
./scripts/runsql.sh "./sql/seed.sql"

