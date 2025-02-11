using DotNetBooksAPI.Data;
using DotNetBooksAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDBService>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" && context.Request.Headers["User-Agent"].ToString().Contains("Mozilla"))
    {
        context.Response.Redirect("/swagger");
        return;
    }

    await next();
});



app.UseHttpsRedirection();

// Skapa en bok
app.MapPost("/book", async (MongoDBService db, Book book) =>
{
    await db.AddBook(book);
    return Results.Ok(book);
});

// Hämta alla böcker
app.MapGet("/books", async (MongoDBService db) =>
{
    var books = await db.GetAllBooks();
    return Results.Ok(books);
});

// Hämta bok efter ID
app.MapGet("/book/{id}", async (MongoDBService db, string id) =>
{
    var book = await db.GetBookById(id);
    return Results.Ok(book);
});

// Uppdatera en bok
app.MapPut("/book", async (MongoDBService db, Book updatedBook) =>
{
    await db.UpdateBook(updatedBook);
    return Results.Ok(updatedBook);
});

// Ta bort en bok efter ID
app.MapDelete("/book/{id}", async (MongoDBService db, string id) =>
{
    var success = await db.DeleteBook(id);
    return success ? Results.Ok("Deleted") : Results.NotFound();
});

app.Run();







