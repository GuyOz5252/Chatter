using Chatter.Api.Middlewares;
using Chatter.Domain.Interfaces;
using Chatter.Domain.Services;
using Microsoft.OpenApi.Models;

namespace Chatter.Api;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddProblemDetails();
        services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
        services.AddControllers();
        services.AddOpenApi();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Chatter API" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors();

        app.UseAuthorization();

        app.UseExceptionHandler();
        
        app.UseSwagger();
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chatter API");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
