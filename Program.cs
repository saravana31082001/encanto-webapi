var builder = WebApplication.CreateBuilder(args);

// Read from appsettings.json
string mongoConnection = builder.Configuration["MongoDBSettings:ConnectionURI"]!;
string mongoDbName = builder.Configuration["MongoDbSettings:DatabaseName"]!;
string sessionsCollectionName = builder.Configuration["MongoDBSettings:SessionsCollectionName"]!;

// Add services to the container.
builder.Services.AddControllers();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend",
//        policy =>
//        {
//            policy.WithOrigins(
//                "http://localhost:5173", // local dev
//                "https://red-mushroom-0c80b7710.1.azurestaticapps.net" // deployed frontend
//            )
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials();
//        });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
            {
                var uri = new Uri(origin);
                return uri.Host == "localhost"
                       || uri.Host == "127.0.0.1"
                       || origin == "https://red-mushroom-0c80b7710.1.azurestaticapps.net";
            })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});



// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Enable Swagger in ALL environments (not just Development)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend"); // Enables CORS (before authorization & controllers)

// Adds session validation middleware BEFORE controllers
app.UseMiddleware<EncantoWebAPI.Middlewares.SessionValidationMiddleware>(
    mongoConnection,
    mongoDbName,
    sessionsCollectionName
);

app.UseAuthorization();

app.MapControllers();

app.Run();
