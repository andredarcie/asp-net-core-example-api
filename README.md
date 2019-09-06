# ASP.NET Core example API

## Tutorial to create this project

> **Write tutorial code without copying directly to reinforce learning**

### Initial Project Files
Download the .NET Core SDK in [dotnet.microsoft](https://dotnet.microsoft.com/download)

Choose your O.S, windows, linux or macOS. Click the "Download .NET Core SDK" button, this tutorial follows version 2.2

Once installed, run the command on your terminal:

`dotnet --version`

If all is right, your .NET version will appear.

You can run the command:

`dotnet new --help`

To know what types of project templates can be created, how are we going to create an API, the option would be "ASP.NET Core Web API"

Run the command to create the Web API project named "MyApp"

`dotnet new webapi -o MyApp`


[This commmit has all the files that will be generated](https://github.com/andredarcie/asp-net-core-example-api/commit/2aa400b1cc5b6f6473e5d4639d729bbe814f7295)

Run `cd MyApp` to enter in the MyApp folder


### Entities and Database
Run the command to add the ORM in this case EF Core with SQLite database

`dotnet add package Microsoft.EntityFrameworkCore.Sqlite`

Add folder *Models* and *Movies* inside it, create the entity named [Movie.cs](https://github.com/andredarcie/asp-net-core-example-api/blob/c0832b17470008bc8371219fddfd632f7527f6b5/Models/Movies/Movie.cs), that will be represented as table *Movies* in database

Create file [ApplicationDbContext.cs](https://github.com/andredarcie/asp-net-core-example-api/blob/c0832b17470008bc8371219fddfd632f7527f6b5/Models/ApplicationDbContext.cs) this file contains all entities that will be mapped in the database

In *Startup.cs* file add, the following code after `AddMvc` and `SetCompatibilityVersion` methods

`services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=myapp.db"));`

The `AddDbContext` method is responsible for telling which db context will be used by the project, in this case `ApplicationDbContext` class

### Managers

Managers encapsulates business logic that doesn't naturally fit within a domain object

Add [IMovieManager.cs](https://github.com/andredarcie/asp-net-core-example-api/blob/6409796fa360a5c20b33ea44893c61169a318eae/Models/Movies/IMovieManager.cs) and [MovieManager.cs](https://github.com/andredarcie/asp-net-core-example-api/blob/6409796fa360a5c20b33ea44893c61169a318eae/Models/Movies/MovieManager.cs) files who are responsible for manipulating the movie entity

Add the following code before AddDbContext method in `Startup` class

`services.AddScoped<IMovieManager, MovieManager>();`

This line of code defines which implementation the IMovieManager interface will use

### Controllers

Controllers handles incoming HTTP requests and send response back to the caller.

Remove `ValuesController` that has generated in the `dotnet new` command that in be not used in this tutorial

Create [MoviesController.cs](https://github.com/andredarcie/asp-net-core-example-api/blob/6a5ad2eb9e1d5e6fb0a36f01c1b24c02d020d43f/Controllers/MoviesController.cs) class that have the following methods:

| Verb | URL | Description |
| -------- | -------- | -------- |
| `GET`     | api/movies     |  return all movies |
| `GET`     | api/movies/5     |  get a movie based on its id |
| `POST`     | api/movies     |  creates a movie |
| `PUT`     | api/movies/5     |  update an existing movie |
| `DELETE`     | api/movies/5     |  delete a movie based on its id |

Controllers uses Managers to do business logic operations

### Add Directors

A director can have multiple movies, and a movie can have only one director

Create [Director.cs](https://github.com/andredarcie/asp-net-core-example-api/blob/af71bfc32dee7bb3c6d5703d74b8cca38001a524/Models/Directors/Director.cs), [IDirectorManager.cs](https://github.com/andredarcie/asp-net-core-example-api/blob/af71bfc32dee7bb3c6d5703d74b8cca38001a524/Models/Directors/IDirectorManager.cs) and [DirectorManager.cs](https://github.com/andredarcie/asp-net-core-example-api/blob/af71bfc32dee7bb3c6d5703d74b8cca38001a524/Models/Directors/DirectorManager.cs) files

Add `public DbSet<Director> Directors { get; set; }` in `ApplicationDbContext.cs` file 

And add `services.AddScoped<IDirectorManager, DirectorManager>();` in `Startup.cs` file

Create [DirectorsController.cs](https://github.com/andredarcie/asp-net-core-example-api/blob/af71bfc32dee7bb3c6d5703d74b8cca38001a524/Controllers/DirectorsController.cs) inside `Controllers` folder

In `Models/Movies/Movie.cs` file add

```
public long DirectorId { get; set; }
public Director Director { get; set; }
```

In `Models/Movies/MovieManager.cs` file change `GetAll()` and `GetById()` methods

```
public async Task<List<Movie>> GetAll()
{
    return await _context.Movies.Include(m => m.Director).ToListAsync();
}
```

```
public async Task<Movie> GetById(long id)
{
    return await _context.Movies.Include(m => m.Director)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(m => m.Id == id);
}
```

To include the director in the movie

### Create Database
Run the command, to created the database migrations and snapshot, which is the C# code that represents the database
`dotnet ef migrations add InitialCreate`

Apply the changes
`dotnet ef database update`

### Run

`dotnet build`

`dotnet run`