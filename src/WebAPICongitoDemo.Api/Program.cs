using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using WebAPICongitoDemo.Api.Config;
using WebAPICongitoDemo.Api.Infra.Authorization;

var builder = WebApplication.CreateBuilder(args);

IConfigurationSection configurationSection = builder.Configuration.GetSection(nameof(AWSCognito));
builder.Services.Configure<AWSCognito>(configurationSection);

AWSCognito cognito = configurationSection.Get<AWSCognito>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCognitoIdentity();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = cognito.AuthorityUrl;
    options.Audience = cognito.ClientId;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(
       options => options.AddPolicy("Managers", policy => policy.Requirements.Add(new CognitoGroupAuthorizationRequirement("Managers")))
   );

builder.Services.AddSingleton<IAuthorizationHandler, CognitoGroupAuthorizationHandler>();

builder.Services.AddHttpClient("CognitoToken", config =>
{
    config.BaseAddress = new Uri(cognito.TokenUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
