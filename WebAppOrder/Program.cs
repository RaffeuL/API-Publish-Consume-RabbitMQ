using WebAppOrder.Extensions;
using WebAppOrder.Routes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterAPI();
builder.Services.AddDomainConfigs();

var app = builder.Build();

app.RegisterAPI();
app.AddEndPoints(builder.Services.BuildServiceProvider());

app.Run();