using TaskManagementAPI.Middlewares;
using TaskManagementAPI.StartupConfig;
using TaskManagementBusinessLayer.ScheduledJobs;

var builder = WebApplication.CreateBuilder(args);


builder.AddStandardServices();
builder.AddAuthServices();
builder.AddCustomServices();
builder.AddValidationServices();
//builder.Services.AddHttpClient();

var app = builder.Build();
//Insert Error Handling Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapGet("/DailyReport", (Report report) => report.ReportData.Order());

app.MapControllers();


app.Run();
