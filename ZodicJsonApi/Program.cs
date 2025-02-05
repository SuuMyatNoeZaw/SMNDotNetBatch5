using Newtonsoft.Json;

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

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/zodiac", () =>
{
    string folderPath = "Data/Zodiac.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<ZodiacRespondModel>(jsonStr);
    return Results.Ok(result.ZodiacSignsDetail);
})
    .WithName("GetAllZodiacSign")
.WithOpenApi();

app.MapGet("/zodiac/{id}", (int id) =>
{
    string folderPath = "Data/Zodiac.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<ZodiacRespondModel>(jsonStr);
    var item=result.ZodiacSignsDetail.FirstOrDefault(x => x.Id == id);
    if(item is null)
    {
        return Results.BadRequest("No Data Found.");
    }

    return Results.Ok(item);
})
    .WithName("GetZodiacSignByID")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}



public class ZodiacRespondModel
{
    public ZodiacModel[] ZodiacSignsDetail { get; set; }
}

public class ZodiacModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MyanmarMonth { get; set; }
    public string ZodiacSignImageUrl { get; set; }
    public string ZodiacSign2ImageUrl { get; set; }
    public string Dates { get; set; }
    public string Element { get; set; }
    public string ElementImageUrl { get; set; }
    public string LifePurpose { get; set; }
    public string Loyal { get; set; }
    public string RepresentativeFlower { get; set; }
    public string Angry { get; set; }
    public string Character { get; set; }
    public string PrettyFeatures { get; set; }
    public Trait[] Traits { get; set; }
}

public class Trait
{
    public string name { get; set; }
    public int percentage { get; set; }
}
