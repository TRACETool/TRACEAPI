using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SecurityWebhook.API.Infrastructure;
using SecurityWebhook.Lib.Models.Constants;
using SecurityWebhoook.Lib.Services.Infrastructure;
using SecurityWebhoook.Lib.Services.SignalRHub;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen()
    .AddHttpContextAccessor()
    .AddHttpClient().AddSignalR();
builder.Services.DependencyConfig(builder.Configuration);
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "SecurityWebhookAPI",
            ValidAudience = "SSCPWeb",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.JWTKey))
        };
    });

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", option =>
{
    option.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHub<CommitHub>("/historicaldatahub");

app.Run();
