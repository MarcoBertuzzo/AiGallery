using AiGallery.Client.Pages;
using AiGallery.Components;
using AiGallery.Data;
using AiGallery.Resources;
using AiGallery.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

//Enable Serilog
var logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.CreateLogger();
builder.Logging.AddSerilog(logger);



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
    

builder.Services.AddDbContext<MyDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Scoped);  //see appsettings.json

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<TitleService>();
builder.Services.AddScoped<LanguageService>();

builder.Services.AddScoped<EmailService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseRouting();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);


app.UseRequestLocalization(options =>
{
    var supportedCultures = new string[] { "en-US", "en-GB", "en-CA", "it-IT" };

    options.AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures)
        .SetDefaultCulture("en-US");
});

app.Run();
