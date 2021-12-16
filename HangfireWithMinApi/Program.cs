using System;
using CronEspresso.NETCore;
using Hangfire;
using Hangfire.SQLite;
using Hangfire.Storage.SQLite;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<SendEmailsJob>();
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage());

builder.Services.AddHangfireServer();
builder.Services.AddRazorPages();
var app = builder.Build();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapHangfireDashboard();
});

//fire and forget job
BackgroundJob.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

//var strCronExpression = CronGenerator.GenerateMinutesCronExpression(6);

RecurringJob.AddOrUpdate<SendEmailsJob>(job => job.Execute(10), cronExpression: "*/5 * * * *");


app.MapGet("/", () => "To see hangfire Dashboard use the url /hangfire");

app.Run();
