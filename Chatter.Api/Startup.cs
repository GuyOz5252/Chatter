using Chatter.Api.Middlewares;
using Chatter.Domain.Entities;
using Chatter.Domain.Interfaces;
using Chatter.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SharedKernel.Interfaces;

namespace Chatter.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("Chatter"));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                _configuration.GetConnectionString("ChatterDb"),
                optionsBuilder => optionsBuilder.MigrationsAssembly("Chatter.Api"));
            options.UseSeeding((dbContext, _) =>
            {
                // TODO: fix
                dbContext.Set<User>().AddRange(
                    new User("626guyo", "Password1")
                );
            });
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
        
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
