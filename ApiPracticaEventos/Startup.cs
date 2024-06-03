using ApiPracticaEventos.Data;
using ApiPracticaEventos.Helpers;
using ApiPracticaEventos.Models;
using ApiPracticaEventos.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ApiPracticaEventos;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    private async Task<string> GetSecretAsync()
    {
        return await HelperSecretManager.GetSecretAsync();
    }
    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        string secret = GetSecretAsync().GetAwaiter().GetResult();

        KeysModel model = JsonConvert.DeserializeObject<KeysModel>(secret);
        string connectionString = model.ConnectionString;

        services.AddControllers();
        services.AddTransient<EventosRepository>();
        services.AddDbContext<EventosContext>(options =>
        {
            options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
        });
        services.AddSingleton<KeysModel>(x => model);
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api EVENTOS AWS",
                Version = "v1"
            });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(url: "swagger/v1/swagger.json",
                "Api TEMPLATE AWS");
            options.RoutePrefix = "";
        });

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors(options => options.AllowAnyOrigin());

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}