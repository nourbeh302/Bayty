using EFModeling;
using EFModeling.DataStore;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.DataStoreContract;
using Models.Entities;
using Serilog;
using Serilog.Events;
using System.Text;
using BaytyAPIs.Validators;
using BaytyAPIs.Middlewares;
using BaytyAPIs.Services.Authentication;
using BaytyAPIs.Services.EmailSending;
using BaytyAPIs.Services.RealTime;
using BaytyAPIs.Services.SMS;
using Microsoft.AspNetCore.Http.Features;
using System.Net.WebSockets;

//Brain tree Payment Service Handler
var builder = WebApplication.CreateBuilder(args);

//builder.Logging.ClearProviders();

//var logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .WriteTo.File($"./Logs/LogDay{Convert.ToString(DateTime.Now)}/Test.txt", LogEventLevel.Debug, rollingInterval: RollingInterval.Day)
//    .CreateLogger();

// Add services to the container.


builder.Services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblies(ValidatorsAssembliesList.validatorsAssembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.Configure<SMSSettings>(builder.Configuration.GetSection("SMSSettings"));

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddIdentity<User, IdentityRole>(opts => opts.User.AllowedUserNameCharacters += " ")
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

// LifeTime => notfound
builder.Services.AddDbContextPool<ApplicationDbContext>(opts =>
{
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    optionsBuilder.UseSqlServer(connectionString,
        a => a.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.AddSingleton<PhoneNumberValidatorTokens>();
builder.Services.AddHttpClient<ISMSService, SMSService>();
builder.Services.AddSingleton<IWebSocketService, WebSocketService>();
builder.Services.AddScoped<IDataStore, DataStore>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<ISMSService, SMSService>();

builder.Services.AddAuthorization(config =>
                                  config.AddPolicy("EmailVerifiedPolicy",
                                    policy => policy.RequireClaim("EmailVerified", "True")));

//JWT => Json Web Tokens
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opts =>
    {
        opts.SaveToken = false;
        opts.RequireHttpsMetadata = false; // In Production will be true
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Http Request

app.UseWebSockets();

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();

app.UseCors(config =>
{
    config.AllowAnyHeader();
    config.AllowAnyMethod();
    config.AllowAnyOrigin();
});

app.UseRefreshTokenHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();