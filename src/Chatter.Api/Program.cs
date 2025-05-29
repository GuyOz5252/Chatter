using System.Reflection;
using Chatter.Api.Extensions;
using Chatter.Api.Middlewares;
using Chatter.Application.Chats.Create;
using Chatter.Application.Chats.Get;
using Chatter.Application.Chats.SendChatMessage;
using Chatter.Application.Users.Register;
using Chatter.Domain.Abstract;
using Chatter.Domain.Entities;
using Chatter.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using SharedKernel;
using SharedKernel.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    optionsBuilder.UseInMemoryDatabase("ChatterDb");
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ICommandHandler<RegisterUserCommand, Guid>,  RegisterUserCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateChatCommand, Guid>, CreateChatCommandHandler>();
builder.Services.AddScoped<ICommandHandler<SendChatMessageCommand>, SendChatMessageCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetChatsQuery, List<Chat>>, GetChatsQueryHandler>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Chatter API" });
});

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseSwaggerUI(options => options.DocumentTitle = "Chatter API");
app.UseExceptionHandler();
app.MapEndpoints();

await app.RunAsync();
