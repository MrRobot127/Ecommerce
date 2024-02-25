using Ecommerce.Api.Services;
using Ecommerce.Application;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Identity;
using Ecommerce.Infrastructure;
using Ecommerce.Persistence;
using Ecommerce.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistanceServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("all", builder => builder.AllowAnyOrigin()
//    .AllowAnyHeader()
//    .AllowAnyMethod());
//});


var app = builder.Build();

// Initialise and seed the database
using (var scope = app.Services.CreateScope())
{
    try
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<UserIdentityDbContextInitialiser>();
        await initialiser.InitialiseAsync();

        var persistenceInitialiser = scope.ServiceProvider.GetRequiredService<PersistenceDbContextInitialiser>();
        await persistenceInitialiser.InitialiseAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database initialisation.");

        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
