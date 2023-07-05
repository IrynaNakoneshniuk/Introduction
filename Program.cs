using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (context) =>
{
    if (context.Request.Query.Count == 0)
    {
        context.Response.ContentType= "text/plain; charset=utf-8";
        await context.Response.WriteAsync("Привіт користувач NET 6.0");
    }
});

app.MapGet("/info", async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    var tableResp = new System.Text.StringBuilder("<table>").
    Append($"<tr><td>Host:{context.Request.Host}</td>" +
    $"<tr><td>Path: {context.Request.Path}</td></tr>");

    if (context.Request.Query["name"] == "ivan" && context.Request.Query["age"] == "33")
    {
        tableResp.Append($"<tr><td>QueryString= {context.Request.QueryString}</td></tr>");
    }

    await context.Response.WriteAsync(tableResp.ToString());

});


app.MapGet("/time", async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    await context.Response.WriteAsync($"Date: {DateTime.UtcNow}");

});


app.MapGet("/key", async (context) =>
{
    IConfigurationSection section = app.Configuration.GetSection("Key");

    if (section != null)
    {
        context.Response.ContentType = "text/html; charset=utf-8";

        await context.Response.WriteAsync($"Keys: {section["kFirst"]}, {section["kSecond"]}");
    }
});

app.Run();
