using Microsoft.EntityFrameworkCore;
using SMNDotNetBatch5.Database.Models;

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

app.MapGet("/blogs", () =>
{
    AppDbContext db = new AppDbContext();
    var list=db.TblBlogs.AsNoTracking().ToList();
    return Results.Ok(list);
})
.WithName("GetBlogList")
.WithOpenApi();

app.MapPost("/blogs", (TblBlog blog) =>
{
    AppDbContext db = new AppDbContext();
    db.TblBlogs.Add(blog);
    db.SaveChanges();
    return Results.Ok(blog);
})
.WithName("CreateBlog")
.WithOpenApi();

app.MapPut("/blogs/{id}", (int id,TblBlog blog) =>
{
    AppDbContext db = new AppDbContext();
    var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId==id);
    if(item is null)
    {
        return Results.BadRequest("No data found.");
    }
    item.BlogTitle = blog.BlogTitle;
    item.BlogAuthor= blog.BlogAuthor;
    item.BlogContent= blog.BlogContent;
    db.Entry(item).State = EntityState.Modified;
    db.SaveChanges();
    return Results.Ok(item);
})
.WithName("UpdateBlog")
.WithOpenApi();

app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
{
    AppDbContext db = new AppDbContext();
    var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
    if (item is null)
    {
        return Results.BadRequest("No data found.");
    }
    if (!String.IsNullOrEmpty(blog.BlogTitle))
    {
        item.BlogTitle = blog.BlogTitle;
    }
    if (!String.IsNullOrEmpty(blog.BlogAuthor))
    {
        item.BlogAuthor = blog.BlogAuthor;
    }
    if (!String.IsNullOrEmpty(blog.BlogContent))
    {
        item.BlogContent = blog.BlogContent;
    } 
    db.Entry(item).State = EntityState.Modified;
    db.SaveChanges();
    return Results.Ok(item);
})
.WithName("UpdateBlogByOne")
.WithOpenApi();

app.MapDelete("/blogs/{id}", (int id) =>
{
    AppDbContext db = new AppDbContext();
    var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
    if (item is null)
    {
        return Results.BadRequest("No data found.");
    }
    db.Entry(item).State = EntityState.Deleted;
    db.SaveChanges();
    return Results.Ok(item);
})
.WithName("DeleteBlog")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
