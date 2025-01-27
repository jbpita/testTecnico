#!/bin/bash

# Iniciar SQL Server en segundo plano
/opt/mssql/bin/sqlservr &

# Esperar hasta que SQL Server esté completamente disponible
/scripts/wait-for-it.sh localhost:1433 -- /scripts/init.sh

# Esperar hasta que SQL Server termine de ejecutarse (si es necesario)
wait
