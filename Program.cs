using WebApplicationPOE1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container from Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline using Startup class
startup.Configure(app, app.Environment);

app.Run();