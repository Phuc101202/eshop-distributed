
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using ServiceDefaults.Messaging;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogDb");
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductAIService>();
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

builder.AddOllamaSharpChatClient("ollama-llama3-2");
builder.AddOllamaSharpEmbeddingGenerator("ollama-all-minilm");

builder.Services.AddInMemoryVectorStoreRecordCollection<int, ProductVector>("products");


// Add services to the container.

var app = builder.Build();


app.MapDefaultEndpoints();

app.UseMigration();

app.MapProductEndpoints();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.Run();
