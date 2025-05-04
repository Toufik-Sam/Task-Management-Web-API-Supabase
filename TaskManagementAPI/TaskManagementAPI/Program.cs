using TaskManagementAPI.Middlewares;
using TaskManagementAPI.StartupConfig;

var builder = WebApplication.CreateBuilder(args);


builder.AddStandardServices();
builder.AddAuthServices();
builder.AddCustomServices();
builder.AddValidationServices();


var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
