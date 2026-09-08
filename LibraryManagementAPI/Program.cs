using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Infrastructure.Repositories;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using IAuthenticationService = Application.Interfaces.IServices.IAuthenticationService;
using Application.Services;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(
    op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IMemberService, MemberService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IBorrowService, BorrowService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationSerice>();
builder.Services.AddTransient<GenerateToken>();

builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IMemberRepository, MemberRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBorrowRepository, BorrowRepository>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();

builder.Services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<AppDbContext>().AddSignInManager();

builder.Services.AddCors(op =>
{
    op.AddPolicy("AllowFrontEnd", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(
    op =>
    {
        op.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter JWT token in the format: Bearer {token}"

        });
        op.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.UseCors("AllowFrontEnd");

app.MapControllers();

app.Run();
