using LeagueOP.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.Configure<LeagueApiOptions>(options =>
    options.BaseUrl = builder.Configuration["ConnectionStrings:LeagueApiRoute"]);


builder.Services.AddSingleton<ILeagueApiCaller, LeagueApiCaller>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
