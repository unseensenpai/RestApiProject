using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using TestProject.Core.Middlewares;
using TestProject.DAL;
using TestProject.Dto.Auth;
using TestProject.HttpApi.Core;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
RegisterConfigurations(builder);
RegisterServices(builder);
WebApplication app = builder.Build();
ConfigurePipelineSettings(app);
AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
app.Logger.LogInformation("--------- Test API Web Application Started! Version: {assembly} ---------", AppDomain.CurrentDomain.GetType().Assembly.GetName().Version);
app.Run();


static void RegisterConfigurations(WebApplicationBuilder builder)
{
    builder.Configuration.AddJsonFile("appsettings.json", true, true);
    builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
    builder.Services.Configure<ServiceConfigs>(builder.Configuration.GetSection("ServiceConfiguration"));
}
static void RegisterServices(WebApplicationBuilder builder)
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Limits.MaxRequestBodySize = int.MaxValue;
        options.Limits.MaxRequestBufferSize = int.MaxValue;
    });
    builder.WebHost.UseKestrelCore();
    builder.WebHost.UseKestrelHttpsConfiguration();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(config =>
    {
        config.SwaggerDoc("v1", new OpenApiInfo()
        {
            Title = $"Test Api - {Assembly.GetExecutingAssembly().GetName().Version} - {builder.Environment.EnvironmentName}",
            Version = "v1",
            Description = "Test API projesidir."
        });
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
    builder.Services.AddHttpClient(builder.Configuration.GetValue<string>("Endpoints:Local:Client"), options =>
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
            configs.DocumentTitle = "Test API v1";
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
void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    var ex = (Exception)e.ExceptionObject;
    var stackTrace = new StackTrace(ex).GetFrame(0)?.GetMethod();
    app.Logger.LogCritical("An error occured. ERROR: {exMessage} - METHOD: {exThrowedClassName}.{methodName}()", ex.Message, stackTrace?.ReflectedType?.Name, stackTrace?.Name);
}
