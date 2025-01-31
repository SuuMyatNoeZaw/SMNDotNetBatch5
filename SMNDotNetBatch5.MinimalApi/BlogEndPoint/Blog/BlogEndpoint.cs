using Microsoft.EntityFrameworkCore;
using SMNDotNetBatch5.Database.Models;

namespace SMNDotNetBatch5.MinimalApi.BlogEndPoint.Blog
{
    public static class BlogEndpoint
    {
        //public static string Test(this int i)
        //{
        //    return i.ToString();
        //}
        public static void MapBlogEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", () =>
            {
                AppDbContext db = new AppDbContext();
                var list = db.TblBlogs.AsNoTracking().ToList();
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

            app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found.");
                }
                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
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


        }
    }
}
