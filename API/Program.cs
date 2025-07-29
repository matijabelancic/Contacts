using API.Interfaces;
using API.Migrations;
using API.Repositories;
using API.Utilities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("sqlConnection")!;
builder.Services.AddScoped<IContactsRepo>(_ => new ContactsRepo(connectionString));
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>(); 
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.MapControllers();

var (result, migrationException) = MigrationsWorker.ApplyMigrations(connectionString);
if (result == -1)
{
    throw new Exception("[Migration] Could not run migration scripts. " + migrationException.Message, migrationException);
}

app.Run();

