var builder = WebApplication.CreateBuilder(args);

// Read from appsettings.json
string mongoConnection = builder.Configuration["MongoDBSettings:ConnectionURI"]!;
string mongoDbName = builder.Configuration["MongoDbSettings:DatabaseName"]!;
string sessionsCollectionName = builder.Configuration["MongoDBSettings:SessionsCollectionName"]!;

// Add services to the container.
builder.Services.AddControllers();

#region remove or comment when uploading to github

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowLocalhost",
//        policy =>
//        {
//            policy.SetIsOriginAllowed(origin =>
//            {
//                if (origin == null) return false;

//                var uri = new Uri(origin);
//                return uri.Host == "localhost" || uri.Host == "127.0.0.1"; // allows ANY port on localhost/127.0.0.1
//            })
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials(); // only if you need cookies/auth
//        });
//});

#endregion

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


#region remove / comment when uploading to github

//app.UseCors("AllowLocalhost"); // Enables CORS (before authorization & controllers)

#endregion

// Adds session validation middleware BEFORE controllers
app.UseMiddleware<EncantoWebAPI.Middlewares.SessionValidationMiddleware>(
    mongoConnection,
    mongoDbName,
    sessionsCollectionName
);

app.UseAuthorization();

app.MapControllers();

app.Run();
