using System.Reflection;
using IndigoSoftTest.BusinessLogic.DI;
using IndigoSoftTest.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddBusinessLogic(builder.Configuration);
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseHsts();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.UseRouting();


using (var serviceScope = app.Services.CreateScope())
{
    serviceScope.ServiceProvider.GetRequiredService<IndigoSoftTestDbContext>().Database.Migrate();
}

app.Run();