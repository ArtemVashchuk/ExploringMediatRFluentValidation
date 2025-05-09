using FluentValidation;
using Gatherly.App.Extensions;
using Gatherly.Application.Behaviors;
using Gatherly.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .Scan(selector => selector
        .FromAssemblies(
            Gatherly.Infrastructure.AssemblyReference.Assembly,
            Gatherly.Persistence.AssemblyReference.Assembly)
        .AddClasses(false)
        .AsImplementedInterfaces()
        .WithScopedLifetime());

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Gatherly.Application.AssemblyReference.Assembly));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(Gatherly.Application.AssemblyReference.Assembly, includeInternalTypes: true);

string connectionString = builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddDbContext<ApplicationDbContext>((_, optionsBuilder) =>
{
    optionsBuilder.UseSqlServer(connectionString)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

builder
    .Services
    .AddControllers()
    .AddApplicationPart(Gatherly.Presentation.AssemblyReference.Assembly);

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();