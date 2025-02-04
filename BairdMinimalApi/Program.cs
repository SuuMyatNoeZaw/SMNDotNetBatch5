using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        var summaries = new[]
        {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

       app.MapGet("/birds", () =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdRespondModel>(jsonStr);
            return Results.Ok(result.Tbl_Bird);
        })
        .WithName("BirdGet")
        .WithOpenApi();

        app.Run();
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}



public class BirdRespondModel
{
    public BirdModel[] Tbl_Bird { get; set; }//json file name (can't change)
}

public class BirdModel//c# class name
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
