using LocalAI.Components;
using LocalAI.Configuration;
using System.ClientModel;
using OpenAI;
using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var lmStudio = builder.Configuration
    .GetRequiredSection(LmStudioOptions.SectionName)
    .Get<LmStudioOptions>()!;

var lmStudioClient = new OpenAIClient(
    new ApiKeyCredential(lmStudio.ApiKey),
    new OpenAIClientOptions { Endpoint = new Uri(lmStudio.Endpoint) }
);

builder.Services.AddChatClient(lmStudioClient.GetChatClient(lmStudio.ModelName).AsIChatClient());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();