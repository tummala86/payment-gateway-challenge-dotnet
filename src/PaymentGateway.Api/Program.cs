using System.Text.Json.Serialization;

using Hellang.Middleware.ProblemDetails;

using O9d.Json.Formatting;

using PaymentsGateway.Api.Constants;
using PaymentsGateway.Api.Extensions;
using PaymentsGateway.Api.Middleware;

using ProblemDetailsExtensions = PaymentsGateway.Api.Extensions.ProblemDetailsExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new JsonSnakeCaseNamingPolicy()));
    }); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.SetupServices(builder.Configuration);
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddProblemDetails(ProblemDetailsExtensions.ConfigureProblemDetails);
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TraceIdMiddleware>();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.MapHealthChecks(ApiRoutes.HealthChecks.Internal);

app.Run();


namespace PaymentsGateway.Api
{
    public partial class Program { }
}

