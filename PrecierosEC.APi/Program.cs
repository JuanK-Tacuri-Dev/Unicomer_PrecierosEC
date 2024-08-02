using PrecierosEC.Core.Extensions;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCorsProgram();
builder.Services.SetAppsetings(builder.Configuration);


//builder.Services.AddDbContext<Context>(options => options.UseSqlServer(AppConfiguration.ConnectionString));
builder.Services.AddScoped<IPrecierosService, PrecierosService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerGenAndSecurityToken();

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

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
