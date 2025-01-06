using ClientServerApp.DAL.Entities;
using ClientServerApp.DAL.Extensions;
using ClientServerApp.DAL.Repositories;

var builder = WebApplication.CreateBuilder();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddUserRepository(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Logger.LogInformation(app.Environment.EnvironmentName);
app.Logger.LogInformation(connectionString);

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/api", () => Results.Ok(new { message = "THE API IS ACTIVE!" }));

app.MapGet("/api/users", async (UserRepository repository) => 
{
    var users = await repository.GetAll();
    return Results.Ok(users);
});

app.MapGet("/api/users/{id}", async (string id, UserRepository repository) => 
{
    var user = await repository.Get(Guid.Parse(id));
    if (user == null)
    {
        return Results.NotFound(new { message = $"Not found user by id {id}" });
    }
    return Results.Ok(user);

});

app.MapDelete("/api/users/{id}", async (string id, UserRepository repository) => 
{
    var user = await repository.Delete(Guid.Parse(id));
    if (user == null)
    {
        return Results.NotFound(new { message = $"Not found user by id {id}" });
    }
    return Results.Ok(user);
});

app.MapPost("/api/users", async (User user, UserRepository repository) => 
{
    var newUser = await repository.Add(user);
    return Results.Ok(user);
});

app.MapPut("/api/users", async (User user, UserRepository repository) => 
{
    var updatedUser = await repository.Update(user);
    if (updatedUser == null)
    {
        return Results.NotFound(new { message = $"Not found user by id {user.Id}"});
    }
    return Results.Ok(updatedUser);
});

app.Run();
