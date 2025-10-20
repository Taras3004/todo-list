# TodoList ASP.NET Core MVC + Web API
Project implements a simple TodoList with a **Web API** for managing lists and tasks and an **MVC web application** for displaying and authorizing users.

## Technologies
- **Backend / API:** ASP.NET Core Web API, Entity Framework Core, SQL Server (LocalDB)
- **Frontend:** ASP.NET Core MVC, Razor, CSS
- **Other:** Dependency Injection, Asynchronous CRUD operations, Razor ViewModels

## Functionality
1. **Web API**
   - CRUD for Todo-Lists and tasks
   - CRUD for tags and ability to add them to tasks
2. **MVC WebApp**
   - Users authorization
   - Page for all todo-lists
   - Page for todo-list with tasks
   - Page for every task with detailed description and ability to add tags to task
   - Search, sort and filter tasks
   - Interaction with Web API through `TodoApiClient`
3. **Database**
   - Saving TodoList, tasks and tags
   - Using EF Core migrations

## How to launch project
1. **Clone repository:**
     ```bash
     git clone https://github.com/Taras3004/todo-list.git
     cd todo-list

2. **Adjust connection to the databases.** Go to WebApi/appsettings.json
    ```json
    "ConnectionStrings": {
      "TodoDbConnection": "ENTER CONNECTION STRING FOR TODO DATABASE HERE",
      "UsersDbConnection": "ENTER CONNECTION STRING FOR USERS DATABASE HERE",
      ...
    }
3. **Create startup project** that runs WebApi and WebApp or **run these commands**:
    ```bash
    cd todo-list/WebApi
    dotnet run
    ...
    cd todo-list/WebApp
    dotnet run
