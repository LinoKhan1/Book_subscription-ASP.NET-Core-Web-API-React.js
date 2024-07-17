using Book_subscription.Server.API.Mappings;
using Book_subscription.Server.Core.Configurations;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("https://localhost:5173")  // Allow requests from Vite development server
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddLogging();


// Auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add ASP.NET Core Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));

// Jwt Authentication
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
if (jwtSettings != null)
{
    var key = Encoding.ASCII.GetBytes(jwtSettings.Key);


    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
}

// API Key settings
builder.Services.Configure<ApiKeySettings>(builder.Configuration.GetSection("ApiKeySettings"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories in the DI Container
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();  
builder.Services.AddScoped<IResellerRepository, ResellerRepository>();


// Register services in the DI Container
builder.Services.AddScoped<IJwtAuthService, JwtAuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();    
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IResellerService, ResellerService>();


// Register Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
public partial class Program { }