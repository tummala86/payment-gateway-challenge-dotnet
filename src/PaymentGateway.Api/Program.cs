using Hellang.Middleware.ProblemDetails;
using PaymentGateway.Api.Constants;
using PaymentGateway.Api.Extensions;
using PaymentGateway.Api.Middleware;

using ProblemDetailsExtensions = PaymentGateway.Api.Extensions.ProblemDetailsExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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


namespace PaymentGateway.Api
{
    public partial class Program { }
}