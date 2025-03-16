using TelegramBridge.Api;
using TelegramBridge.Application;
using TelegramBridge.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
