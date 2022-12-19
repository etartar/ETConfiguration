using ETConfiguration.Consul.Sample.Configs;
using ETConfiguration.Core.Consul.Extensions;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddConsulConfiguration(builder.Configuration["ConsulKey"], options =>
{
    options.ConsulConfigurationOptions = cco =>
    {
        cco.Address = new Uri(builder.Configuration["ConsulURL"]);
    };
    options.ReloadOnChange = true;
    options.ReloadOnChangeWaitTime = TimeSpan.FromSeconds(10);
    options.OnLoadException = (consulLoadException) =>
    {
        var message = $"Error onload exception {consulLoadException.Exception.Message}";
        Console.WriteLine(message);
        Debug.WriteLine(message);
    };
    options.OnWatchException = (consulWatchExceptionContext) =>
    {
        var message = $"Unable to watchChanges in Consul due to {consulWatchExceptionContext.Exception.Message}";
        Console.WriteLine(message);
        Debug.WriteLine(message); 
        return TimeSpan.FromSeconds(2);
    };
});

var section = builder.Configuration.GetSection("ConsulConfig");
builder.Services.Configure<ConsulConfig>(section);

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
