using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Tutorly.Application.Authorization;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.Params;
using Tutorly.Application.Handlers;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Application.Services;
using Tutorly.Application.Validators;
using Tutorly.Domain.Models;
using Tutorly.Infrastructure;
using Tutorly.Infrastructure.Repos;
using Tutorly.WebAPI.Authentication;
using Tutorly.WebAPI.Middlewares;
using Tutorly.WebAPI.Services;

/*
 
 @TO DO:

- Logging

-Azure

-address controller
=> adding address to post
 
-tests for getting posts by remote or address *
-tests for address controller

 */

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
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
builder.Services.AddScoped<IRepository<Address>, AddressRepository>();


builder.Services.AddScoped<IHandler<PostApply>, PostApplyHandler>();
builder.Services.AddScoped<IHandler<CreatePost>, CreatePostHandler>();
builder.Services.AddScoped<IHandler<RegisterUser>, RegisterUserHandler>();
builder.Services.AddScoped<IQueryHandler<GetAllPosts, IEnumerable<Post>>, GetAllPostsHandler>();
builder.Services.AddScoped<IHandler<DeleteUser>, DeleteUserHandler>();
builder.Services.AddScoped<IHandler<LoginUser>, LoginUserHandler>();
builder.Services.AddScoped<IQueryHandler<GetUserBy, User>, GetUserByHandler>();
builder.Services.AddScoped<IQueryHandler<GetAllTutors, IEnumerable<Tutor>>, GetAllTutorsHandler>();
builder.Services.AddScoped<IQueryHandler<GetTutorById, Tutor>, GetTutorByIdHandler>();
builder.Services.AddScoped<IQueryHandler<GetPostById, Post>, GetPostByIdHandler>();
builder.Services.AddScoped<IQueryHandler<GetAllCategories, IEnumerable<Category>>, GetAllCategoriesHandler>();
builder.Services.AddScoped<IHandler<DeletePost>, DeletePostHandler>();
builder.Services.AddScoped<IQueryHandler<GetUserData, User>, GetUserDataHandler>();
builder.Services.AddScoped<IHandler<UpdateUserData>, UpdateUserDataHandler>();
builder.Services.AddScoped<IHandler<CreateCategory>, CreateCategoryHandler>();
builder.Services.AddScoped<IHandler<DecideStudentApplication>, DecideStudentApplicationHandler>();
builder.Services.AddScoped<IQueryHandler<GetAddress, Address>, GetAddressHandler>();
builder.Services.AddScoped<IHandler<CreateAddress>, CreateAddressHandler>();


builder.Services.AddScoped<IAuthorizationHandler, ResourceAvaibilityRequirementHandler>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<IAddressService, AddressService>();
    
builder.Services.AddScoped<IValidator<DecideStudentApplicationDto>, DecideStudentApplicationValidator>();   
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();
builder.Services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();
builder.Services.AddScoped<IValidator<CreatePostDto>, CreatePostDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ExceptionHandlingMiddleware>();


var jwtOptionSection = builder.Configuration.GetRequiredSection("JwtOptions");
builder.Services.Configure<JwtOptions>(jwtOptionSection);

var jwtOptions = new JwtOptions();
jwtOptionSection.Bind(jwtOptions);
builder.Services.AddSingleton(jwtOptions);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidIssuer = jwtOptionSection["Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionSection["Key"])),
            RoleClaimType = ClaimTypes.Role,
        };
    });

builder.Services.AddScoped<DataGenerator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", builder =>
            builder.AllowAnyHeader()
            .AllowAnyMethod()
             .AllowAnyHeader()
            .WithExposedHeaders("Location")
            .WithOrigins("http://localhost:5173")

    );
});

var app = builder.Build();


app.UseCors("FrontEndClient");

/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dataGenerator = services.GetRequiredService<DataGenerator>();

    dataGenerator.Seed();
}
*/

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }