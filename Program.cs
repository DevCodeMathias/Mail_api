using mail_api.Data;
using mail_api.Domain.Interfaces;
using mail_api.Application.Service;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();


builder.Services.AddScoped<ICepService, CepService>();
builder.Services.AddScoped<ICepRepository, CepRepository>();
builder.Services.AddScoped<IViaCepService, ViaCepService>();


builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();  
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

