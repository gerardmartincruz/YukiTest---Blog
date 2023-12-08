using Blog.Context;
using Blog.Entities;
using Blog.Filters;
using Blog.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var blogDbConnectionString = builder.Configuration.GetConnectionString("BlogDatabase");

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddLogging();

        if (builder.Environment.IsDevelopment())
            builder.Services.AddDbContext<BlogContext>(options =>
                options.UseInMemoryDatabase("BlogInMemoryDatabase"));
        else
            builder.Services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(blogDbConnectionString));

        builder.Services.AddScoped<IPostReadService, PostReadService>();
        builder.Services.AddScoped<IPostWriteService, PostWriteService>();


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            //add data during app startup
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            dbContext.InitializeData();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGet("/post/{id:int}", (int id, bool? includeAuthor, IPostReadService readService, ILogger<IPostWriteService> logger) =>
        {
            try
            {
                if ((includeAuthor != null) && includeAuthor == true)
                    return readService.GetPostWithAuthor(id);
                else
                    return readService.GetPostWithoutAuthor(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                throw new Exception("Somethig wen wrong");
            }
            
            
        }).AddEndpointFilter<ContentFilter>();

        app.MapPost("/post", async (Post post, IPostWriteService writeService, HttpContext context, ILogger<IPostWriteService> logger) =>
        {
            try
            {
                if (post.Author is not null)
                    await writeService.CreatePostWithAuthor(post);
                else
                    await writeService.CreatePostWithoutAuthor(post);

                context.Response.StatusCode = 201; // Created
                await context.Response.WriteAsync( "inserted succcessfully");
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
            {
                // Handle specific database update exception (e.g., unique constraint violation)
                logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("Error saving to the database: Duplicate key violation.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency exception
                logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 409; // Conflict
                await context.Response.WriteAsync("Concurrency error. The resource has been modified by another user.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync("Internal server error.");
            }
        });

        app.Run();
    }
}
