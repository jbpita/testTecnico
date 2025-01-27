FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=YourStrongPass@2023!

USER root

RUN apt-get update && apt-get install -y curl gnupg2 apt-transport-https netcat-openbsd \
    && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/debian/9/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /scripts

COPY ./scripts/* /scripts/

RUN chmod +x /scripts/init.sh

EXPOSE 1433

CMD /scripts//start.sh