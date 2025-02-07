using Microsoft.AspNetCore.Connections;
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

        app.MapGet("/birds/{id}", (int id) =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdRespondModel>(jsonStr);
            var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return Results.BadRequest("No Data Found.");
            }
            return Results.Ok(item);
        })
       .WithName("BirdGetByID")
       .WithOpenApi();

        app.MapPost("/birds", (BirdModel requestModel) =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdRespondModel>(jsonStr);

            requestModel.Id = result.Tbl_Bird.Count == 0 ? 1 : result.Tbl_Bird.Max(x => x.Id) + 1;
            result.Tbl_Bird.Add(requestModel);

            var Ctojson = JsonConvert.SerializeObject(result);
            File.WriteAllText(folderPath, Ctojson);

            return Results.Ok(requestModel);
        })
      .WithName("BirdCreate")
      .WithOpenApi();

        app.MapPut("/birds/{id}", (int id, BirdModel requestModel) =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdRespondModel>(jsonStr);

            var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return Results.BadRequest("No Data Found.");
            }
            var index = result.Tbl_Bird.FindIndex(x => x.Id == id);
            if (index != -1)
            {
                result.Tbl_Bird[index] = new BirdModel
                {
                    Id = id,
                    BirdEnglishName = requestModel.BirdEnglishName,
                    BirdMyanmarName = requestModel.BirdMyanmarName,
                    Description = requestModel.Description,
                    ImagePath = requestModel.ImagePath,
                };

            }

            var Ctojson = JsonConvert.SerializeObject(result);
            File.WriteAllText(folderPath, Ctojson);

            return Results.Ok(result.Tbl_Bird[index]);
        })
      .WithName("BirdUpdate")
      .WithOpenApi();

        app.MapDelete("/birds/{id}", (int id) =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdRespondModel>(jsonStr);

            var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return Results.BadRequest("No Data Found.");
            }
            result.Tbl_Bird.Remove(item);

            var Ctojson = JsonConvert.SerializeObject(result);
            File.WriteAllText(folderPath, Ctojson);

            return Results.Ok("Your Data already deleted.");
        })
      .WithName("BirdDelete")
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
    public List<BirdModel> Tbl_Bird { get; set; }//json file name (can't change)
}

public class BirdModel//c# class name
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}











