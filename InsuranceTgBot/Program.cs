using InsuranceTgBot.Data;
using InsuranceTgBot.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// adding Serilog
var logger = new LoggerConfiguration().WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
//builder.Services.AddSerilog(logger);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// adding DbContext
builder.Services.AddDbContext<AppDbContext>(opts
    => opts.UseSqlServer(builder.Configuration["ConnectionString"]));

// adding TelegramBotClient
builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient<ITelegramBotClient>(
    httpClient => new TelegramBotClient(builder.Configuration["BotToken"], httpClient));

// adding services
builder.Services.AddScoped<UpdateHandler>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
