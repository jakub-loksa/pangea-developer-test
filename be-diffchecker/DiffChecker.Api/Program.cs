using DiffChecker.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// Exposing the Program class for integration testing.
/// </summary>
public partial class Program { }