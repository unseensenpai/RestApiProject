using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Reflection;
using TestProject.BL.Employee;
using TestProject.Core.Middlewares;
using TestProject.DAL;
using TestProject.Dto.Auth;
using TestProject.HttpApi.Core;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
RegisterConfigurations(builder);
RegisterServices(builder);
WebApplication app = builder.Build();
ConfigurePipelineSettings(app);
app.Run();
app.Logger.LogInformation("Application started!");


static void RegisterConfigurations(WebApplicationBuilder builder)
{
    builder.Configuration.AddJsonFile("appsettings.json", true, true);
    builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
    builder.Services.Configure<ServiceConfigs>(builder.Configuration.GetSection("ServiceConfiguration"));
}
static void RegisterServices(WebApplicationBuilder builder)
{    

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(config =>
    {
        config.IncludeXmlComments($"{AppContext.BaseDirectory}docs.xml");
    });
    builder.Services.AddDbContext<MySqlContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MssqlTest"));
    });

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
static void RegisterInjectionServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<MyCustomExceptionMiddleware>();
    builder.Services.AddHttpContextAccessor();
    //builder.Services.AddSingleton<RequestDelegate>();
    //builder.Services.AddScoped<ExceptionMiddleware>();
    builder.Services.AddServiceModules();
    builder.Services.AddHttpClient(builder.Configuration.GetSection("Endpoints:Local:Client").Value, options =>
    {
        options.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Endpoints:Local:Address"));
        options.DefaultRequestHeaders.Accept.Clear();
        options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        options.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", builder.Configuration.GetSection("").Value);
        options.Timeout = TimeSpan.FromSeconds(30);
    });
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Transient);
    //builder.Services.AddAutoMapper(typeof(EmployeeProfile)); //AppDomain.CurrentDomain.GetAssemblies().Where(x=> x.GetType().IsAssignableFrom(typeof(Profile))).ToArray());
}
static void ConfigurePipelineSettings(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(configs =>
        {
            configs.DocumentTitle = $"Test Api - {Assembly.GetExecutingAssembly().GetName().Version} - {app.Environment.EnvironmentName}";
            configs.RoutePrefix = "";
            configs.SwaggerEndpoint("/swagger/v1/swagger.json", "Test Api v1");
            
        });
    }

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();    
    app.MapControllers();

    app.UseMiddleware<MyCustomExceptionMiddleware>();
}

AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    app.Logger.LogCritical("an error occured.");
}
