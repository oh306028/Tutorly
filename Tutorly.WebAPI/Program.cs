using Microsoft.EntityFrameworkCore;
using Tutorly.Application.Commands;
using Tutorly.Application.Handlers;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Application.Services;
using Tutorly.Domain.Models;
using Tutorly.Infrastructure;
using Tutorly.Infrastructure.Repos;
using Tutorly.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();  

builder.Services.AddDbContext<TutorlyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IRepository<Post>, PostRepository>();
builder.Services.AddScoped<IRepository<Tutor>, TutorRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();

builder.Services.AddScoped<IHandler<CreatePost>, CreatePostHandler>();
builder.Services.AddScoped<IQueryHandler<GetAllPosts, IEnumerable<Post>>, GetAllPostsHandler>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<ExceptionHandlingMiddleware>();


var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }