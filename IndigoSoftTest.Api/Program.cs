using IndigoSoftTest.BusinessLogic.DI;
using IndigoSoftTest.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddBusinessLogic();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.UseRouting();

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseAuthorization();

using (var serviceScope = app.Services.CreateScope())
{
    serviceScope.ServiceProvider.GetRequiredService<IndigoSoftTestDbContext>().Database.Migrate();
}

app.Run();