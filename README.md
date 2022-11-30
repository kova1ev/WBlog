# WBlog
[![build & test](https://github.com/kova1ev/WBlog/actions/workflows/build_&_test.yml/badge.svg?branch=master)](https://github.com/kova1ev/WBlog/actions/workflows/build_&_test.yml)

This pet project is a personal blog site for publishing your articles.

## Images
<img src="https://github.com/kova1ev/WBlog/blob/master/docs/main-2.jpg" alt="main"  />

<details>
   <summary>More images</summary>
  <img src="https://github.com/kova1ev/WBlog/blob/master/docs/index.jpg" alt="main" width="1000px" />
  <hr>
  <img src="https://github.com/kova1ev/WBlog/blob/master/docs/admin_edit-2.jpg" alt="main" width="1000px" />
</details>
  
## How to use
***Required .Net 6 or above***

 ### Database

The default settings in the project use the local Ms SqL Server. You can change the connection string in src\Admin\appsettings.json file.


Then, in the \WBog directory, run cmd and update the database using `dotnet ef` tools https://learn.microsoft.com/en-us/ef/core/cli/dotnet:
```
   dotnet ef database update -p .\src\Infrastructure.Data\ -s .\src\Admin\ 
```
:point_up: If you don't want or can't use Ms Sql Server, then you can use Database In Memory. Change the value of "DataBase Provider" in the src\Admin\appsettings.json file. ( *supported 2 value "mssql" and "inmemory"* )

```
  "DataBaseProvider": "mssql"   =>   "DataBaseProvider": "inmemory"
 ```
### Visual Studio 
In the solution properties, select the two projects to run "Admin" and "WebUI".
And Start projects.

### Terminal 
In the \WBlog directory, run cmd and restore required packages:
```
dotnet restore
```
Then run projects 
```
dotnet run --project .\src\Admin\
```
```
dotnet run --project .\src\WebUI\
```
### Url
Admin -  https://localhost:7184/

Swagger - https://localhost:7184/Swagger/index.html

UserInterface - https://localhost:7232/

**login for admin**

```
login : admin@mail.com
password : !Aa12345
```

## Using Technologies :
<ul>
  <li>Asp.Net Core 6</li>
  <li>EntityFrameworkCore 6</li>
  <li>Razor Pages</li>
  <li>Blazor Server</li>
  <li>Bootstrap 5</li>
  <li>Swagger</li>
</ul>

***The project is still under development***
