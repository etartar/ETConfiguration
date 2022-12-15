using ETConfiguration.Core.Database.Extensions;
using ETConfiguration.Core.Database.Repositories;
using ETConfiguration.Database.Sample.Configs;
using ETConfiguration.Database.Sample.Persistence.Contexts;
using ETConfiguration.Database.Sample.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ConfigurationContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConfigDb"));
});

builder.Services.AddScoped<IConfigurationReadRepository, ConfigurationReadRepository>();
builder.Services.AddScoped<IConfigurationWriteRepository, ConfigurationWriteRepository>();

builder.Configuration.AddDatabaseConfiguration(
    serviceProvider: builder.Services.BuildServiceProvider(),
    reloadOnChange: true,
    reloadDelay: 5000,
    configurationExpression: x => x.IsActive
    );


var section = builder.Configuration.GetSection("ConfigSection");
builder.Services.Configure<ConfigSection>(section);

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
