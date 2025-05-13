using TaskManagementAPI.Middlewares;
using TaskManagementAPI.StartupConfig;

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

app.MapControllers();

app.Run();
