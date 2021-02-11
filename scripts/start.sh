
echo ":: starting containers"
docker-compose start

echo ":: waiting for containers to start..."
secs=5
while [ $secs -gt 0 ]; do
   echo -ne "$secs\033[0K\r"
   sleep 1
   : $((secs--))
done

echo ":: applying migrations"
export CONNSTR="Host=localhost;Username=gra;Port=5409;Password=1;Database=gra"
./scripts/migrator.sh apply