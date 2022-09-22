﻿using Blazorise;
using Blazorise.RichTextEdit;
using Blazored.Modal;
using Blazored.LocalStorage;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

using EviCRM.Server.Core;
using EviCRM.Server.Data;
using EviCRM.Server.Areas.Identity;
using EviCRM.Server.Alexandra;

using Majorsoft.Blazor.Extensions.BrowserStorage;
using Majorsoft.Blazor.Components.Notifications;
using Majorsoft.Blazor.Server.Logging.Console;
using Majorsoft.Blazor.Components.GdprConsent;

using Havit.Blazor.Components.Web;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using EviCRM.Server.Core.EntityFramework;
using BlazorCalendar.Services;
using BlazorCalendar.Core;

var builder = WebApplication.CreateBuilder(args);

//Entity Framework
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var ef_connectionString = builder.Configuration.GetConnectionString("DefaultConnection_EF");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<Context>(options => options.UseMySql(ef_connectionString, ServerVersion.AutoDetect(ef_connectionString)).LogTo(Console.WriteLine), ServiceLifetime.Transient);

//Razor Pages Middleware
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//EF Core Auth
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

//Alexandra Backend Middleware
builder.Services.AddScoped<AlexandraContext>();

builder.Services.AddSingleton<BackendController>();
builder.Services.AddSingleton<Sentinel>();
builder.Services.AddSingleton<SystemCore>();
builder.Services.AddSingleton<InTouchProvider>();

builder.Services.AddSingleton<LiveNature>();

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

builder.Services.AddLocalization();
builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();
builder.Services.AddBrowserStorage();
builder.Services.AddGdprConsent();
builder.Logging.AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Debug);
builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddSingleton<Microsoft.Extensions.Hosting.IHostingEnvironment>(new HostingEnvironment());

builder.Services.AddBlazoredModal();
builder.Services.AddNotifications();

//Local Api Middleware Context
builder.Services.AddHttpClient("LocalApi", client => client.BaseAddress = new Uri("https://localhost:7083/"));

builder.Services.AddBlazoriseRichTextEdit(
                options =>
                {
                    options.UseShowTheme = true;
                }
            );
builder.Services
  .AddBlazorise(options =>
  {
      options.Immediate = true;
  })
  .AddBootstrapProviders().AddFontAwesomeIcons();


builder.Services.AddTransient<ICalendarEventsProvider, MicrosoftCalendarEventsProvider>();
builder.Services.AddTransient<CalendarController>();

//Havit UX UI Nuget Middleware DI
builder.Services.AddHxServices();

//SignalR DI
builder.Services.AddSignalR(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
    options.EnableDetailedErrors = true;
});

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});


//builder.Services.AddLettuceEncrypt();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    //HTTPS SSL Production Middleware
 
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseWebSockets();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization("ru-RU");

app.MapControllers();

//SignalR MapHub
app.MapHub<signalR_chat>("/signalr_chat");
app.MapHub<signalR_general>("/signalr_general");
app.MapHub<signalR_alexandra>("/signalr_alexandra");
app.MapHub<signalR_debug>("/signalr_debug");
app.MapHub<signalR_general>("/signalr_general");

app.MapHub<ALive>("/alive");

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();
