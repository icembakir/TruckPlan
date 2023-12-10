using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using TruckPlan.API.Services;
using TruckPlan.DataAccess.Repositories;
using Microsoft.OpenApi.Models;
using TruckPlan.ExternalAPI.Services.FindCountryService;
using System.Reflection;
using TruckPlan.ExternalAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);
var externalApiConfig = builder.Configuration.GetSection("ExternalApi").Get<ExternalApiSettings>();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddScoped<ITruckPlanService, TruckPlanService>();
builder.Services.AddScoped<ITruckPlanRepository, TruckPlanRepository>();
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddTransient<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddScoped<IFindCountryService, FindCountryService>();
builder.Services.AddSingleton(externalApiConfig);


builder.Services.AddAutoMapper(new List<Assembly> { Assembly.GetExecutingAssembly() }, ServiceLifetime.Singleton);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TruckPlan API", Version = "v1" });
});

builder.Services.AddHttpClient("FindCountryApi", client =>
{
    client.BaseAddress = new Uri(externalApiConfig.ApiUrl);
});

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DisplayOperationId();
});

app.Run();
