using smr.Core.Repositories;
using smr.Core.Services;
using smr;
using smr.Service;
using smr.Core;
using smr.Middleware;
using smr.Data.Repositories;

using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("userCLinet", options =>
    {
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowAnyOrigin();
    });
   
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

builder.Services.AddScoped<IrenterService, renterService>();
builder.Services.AddScoped<IrenterRepository, renterRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile) , typeof(MappingPosrModel));
//builder.Services.AddDbContext<DataContext>();


builder.Services.AddScoped<ItouirstService, touirstService>();
builder.Services.AddScoped<ItouirstRepository, touirstRepository>();


builder.Services.AddScoped<IturnService, turnService>();
builder.Services.AddScoped<IturnRepository, turnRepository>();

builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IuserRepository, userRepository>();
builder.Services.AddScoped<IuserService, userService>();

var app = builder.Build();
app.UseCors("userCLinet");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

//��� �����
app.UseAuthentication();

//�� ��� ���� ����� - �����
app.UseAuthorization();


app.UseTrack();

app.UseHttpsRedirection();



app.MapControllers();

app.Run();

