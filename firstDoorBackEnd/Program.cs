using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

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
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();