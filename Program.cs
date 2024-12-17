using smr.Core.Repositories;
using smr.Core.Services;
using smr.Data.Repositories;
using smr;
using smr.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IrenterService, renterService>();
builder.Services.AddScoped<IrenterRepository, renterRepository>();


//builder.Services.AddDbContext<DataContext>();


builder.Services.AddScoped<ItouirstService, touirstService>();
builder.Services.AddScoped<ItouirstRepository, touirstRepository>();


builder.Services.AddScoped<IturnService, turnService>();
builder.Services.AddScoped<IturnRepository, turnRepository>();

builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
