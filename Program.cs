var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => LoremIpsumNet.LoremIpsumGenerator.Generate());

app.Run();
