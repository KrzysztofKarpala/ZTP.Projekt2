
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Products.Shared;
using Microsoft.AspNetCore.Mvc;
using Products.Infrastructure.Mongo.DatabaseSettings;
using Products.Core.Repositories;
using Products.Infrastructure.Mongo.Repositories;
using Products.Application.Queries;
using Microsoft.OpenApi.Models;

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

var builder = WebApplication.CreateBuilder(args);

#pragma warning disable CS0618
BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
#pragma warning restore CS0618
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.Services.AddApiVersioning(config =>
{
    // Specify the default API Version as 1.0
    config.DefaultApiVersion = new ApiVersion(1, 0);
    // If the client hasn't specified the API version in the request, use the default API version number 
    config.AssumeDefaultVersionWhenUnspecified = true;
    // Advertise the API versions supported for the particular endpoint
    config.ReportApiVersions = true;
});


builder.Services.Configure<ProductDatabaseSettings>(builder.Configuration.GetSection("Mongo"));
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetProductsQuery>());
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = $"products/swagger/{{documentName}}/swagger.json";
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
        {
            swaggerDoc.Servers.Add(new OpenApiServer { Url = $"http://{httpReq.Host.Value}" });
        });
    });
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "products/swagger";
        c.SwaggerEndpoint("v1/swagger.json", "Products API v1");
    });
}



app.UseAuthorization();

app.MapControllers();

app.Run();
