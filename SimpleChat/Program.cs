using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleChat.BLL.Validation;
using SimpleChat.DAL.Persistence;
using SimpleChat.DAL.Repositories.Interfaces;
using SimpleChat.DAL.Repositories.Interfaces.ChatParticipants;
using SimpleChat.DAL.Repositories.Interfaces.Chats;
using SimpleChat.DAL.Repositories.Realizations.ChatParticipants;
using SimpleChat.DAL.Repositories.Realizations.Chats;
using SimpleChat.DAL.Repositories.Realizations.Users;
using SimpleChat.WebApi.Hubs;
using SimpleChat.WebApi.Services.Interfaces;
using SimpleChat.WebApi.Services.Realizations;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7263")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatParticipantRepository, ChatParticipantRepository>();
builder.Services.AddScoped<IChatService,ChatService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(currentAssemblies));
builder.Services.AddAutoMapper(currentAssemblies);
builder.Services.AddValidatorsFromAssembly(Assembly.Load("SimpleChat.BLL"));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");
await app.RunAsync();
