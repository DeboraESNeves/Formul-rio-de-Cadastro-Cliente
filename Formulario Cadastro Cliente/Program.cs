using Formulario_Cadastro_Cliente;
using Formulario_Cadastro_Cliente.Data;
using Formulario_Cadastro_Cliente.Models;
using Formulario_Cadastro_Cliente.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<IClienteService, ClienteService>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var outputTemplate = "{Timestamp} [{level}] {Message} {NewLine} {Exception}";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(outputTemplate: outputTemplate)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


TesteConexao.Testar();
Console.WriteLine("Aplicação iniciada. Pressione CTRL+C para sair.");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clientes}/{action=Add}/{id?}");

app.Run();
