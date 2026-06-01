using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Middleware;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using firstDoorBackEnd.Services;
using firstDoorBackEnd.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FirstDoorContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddHttpClient();

builder.Services.AddScoped<ICareerJetService, CareerJetService>();

builder.Services.AddHttpClient<ICareerJetRepository, CareerJetRepository>(client =>
{
    client.BaseAddress = new Uri("https://search.api.careerjet.net/");

    string apiKey = builder.Configuration["CareerJet:ApiKey"] ?? string.Empty;
    string authenticationString = $"{apiKey}:";

    byte[] binaryData = System.Text.Encoding.UTF8.GetBytes(authenticationString);
    string base64Credentials = Convert.ToBase64String(binaryData);

    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);

    string referer = builder.Configuration["CareerJet:Referer"] ?? string.Empty;

    if (referer != string.Empty)
    {
        client.DefaultRequestHeaders.Add("Referer", referer);
    }
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<IReedService, ReedService>();
builder.Services.AddScoped<IReedService, ReedService>();
builder.Services.AddScoped<IReedRepository, ReedRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = true;
});

builder.Services.AddHealthChecks()
    .AddUrlGroup(
        uri: new Uri($"https://search.api.careerjet.net/v4/query?user_ip={Uri.EscapeDataString("82.165.195.101")}&user_agent={Uri.EscapeDataString("Mozilla")}"),
        name: "careerjet-api",
        failureStatus: HealthStatus.Degraded,
        tags: ["external"],
        configureClient: (_, client) =>
        {
            string apiKey = builder.Configuration["CareerJet:ApiKey"] ?? string.Empty;
            string authenticationString = $"{apiKey}:";

            byte[] binaryData = System.Text.Encoding.UTF8.GetBytes(authenticationString);
            string base64Credentials = Convert.ToBase64String(binaryData);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);

            string referer = builder.Configuration["CareerJet:Referer"] ?? string.Empty;
            client.DefaultRequestHeaders.Add("Referer", referer);
        });

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program {}