using Newtonsoft.Json.Serialization;
using TestProject.Concrete;
using TestProject.Contracts;
using TestProject.HttpApi.Core;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder);
var app = builder.Build();
ConfigurePipelineSettings(app);
app.Run();

static void RegisterInjectionServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IEmployeeService, EmployeeService>();
}

static void RegisterServices(WebApplicationBuilder builder)
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddControllers(options =>
    {
        options.RespectBrowserAcceptHeader = true;
    }).AddApplicationPart(typeof(CoreController).Assembly)
        .AddXmlSerializerFormatters()
        .AddJsonOptions(jsonOptions =>
        {
            jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });

    RegisterInjectionServices(builder);
}

static void ConfigurePipelineSettings(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}