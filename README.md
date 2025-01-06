# ClientServerApp  
### Stack  
NET 8; C# ASP NET Minimal Web API; Blazor; EF Core; PostgreSQL;   
### Quick start
from folder `src` use 

`docker compose up -d` 

then go to http://localhost:8081  
  
![Screenshot](screenshots/client.png)
  
### Desc
It is a simple  client-server  application  that  keeps  track of users (CRUD).

### Application structure
Server starts from _src/backend/ClientServerApp.WebAPI_. Default local host is http://localhost:5000. Also, you can use swagger http://localhost:5000/swagger/index.html  
UI client (blazor) use layer _src/backend/ClientServerApp.Client_. Client starts on http://localhost:8081 (in development/production environment)  
### Start without `docker compose`
First. Deploy database (Postgres)
use this command:
`docker run --net=appnet --name db-container -d -p 5431:5432 postgres`    

Second.Deploy backend from dockerfile 
(src/backend/ClientServerApp.WebAPI/dockerfile)  
before you need to use command:
`dotnet publish -o published -c Release`  
```docker
FROM mcr.microsoft.com/dotnet/aspnet:8.0  AS  runtime
WORKDIR  /app/webapi
COPY  published/  ./
CMD  [  "dotnet",  "ClientServerApp.WebAPI.dll"  ]
```  
`docker build . -t webapi-image`
`docker run --net=appnet --name webapi-container -d -p 8080:8080 webapi-image`

Deploy frontend from dockerfile
(src/frontend/ClientServerApp.BlazorUI/dockerfile)
before you need to use command:
`dotnet publish -o published -c Release`  
```docker
FROM mcr.microsoft.com/dotnet/aspnet:8.0  AS  runtime
WORKDIR  /app/client
COPY  published/  ./
ENV  WEB_API_ADDRESS=http://webapi-container:8080
ENV  ASPNETCORE_HTTP_PORTS=8081
CMD  [  "dotnet",  "ClientServerApp.BlazorUI.dll"  ]
```
`docker build . -t client-image`  
`docker run --net=appnet --name client-container -d -p 8081:8081 client-image`
