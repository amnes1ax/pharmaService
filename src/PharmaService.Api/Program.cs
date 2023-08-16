using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using PharmaService.DataAccess.PostgresSql;
using Hellang.Middleware.ProblemDetails;
using PharmaService.Api;
using PharmaService.Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var connectionString = builder.Configuration.GetConnectionString("main")!;

builder.Services.AddRepositories(connectionString);
builder.Services.AddPharmaServices();

builder.Services.AddProblemDetails(options =>
{
    options.ValidationProblemStatusCode = 400;
    options.IncludeExceptionDetails = (_, _) => builder.Environment.IsDevelopment();
    options.MapFluentValidationException();
    options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
    options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AnyOrigins", policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AnyOrigins");

app.MapControllers();

app.Run();