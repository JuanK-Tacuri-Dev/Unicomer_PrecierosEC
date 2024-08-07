using PrecierosEC.APi.Extensions;
using PrecierosEC.Core.Extensions;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCorsProgram();
builder.Services.SetAppsetings(builder.Configuration);
builder.Services.AddDependecyInjections(builder.Configuration);
builder.Services.AddScoped<IPrecierosService, PrecierosService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerGen();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();       
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
    });
}

string logFilePath = "audit-log.txt";

app.UseMiddleware<AuditMiddleware>(logFilePath);
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
