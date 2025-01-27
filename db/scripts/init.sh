sleep 20s



/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P YourStrongPass@2023! -i /scripts/init.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P YourStrongPass@2023! -i /scripts/sp_getPersonas.sql
