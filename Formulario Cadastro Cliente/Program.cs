using Formulario_Cadastro_Cliente;
using Formulario_Cadastro_Cliente.Data;
using Formulario_Cadastro_Cliente.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();


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

// Paginação
app.MapGet("/clientes/List", async (AppDbContext db, int page = 0, int size = 10) =>
    {
        var clients = await db.Clientes
        .OrderBy(c => c.Id)
        .Skip(page * size)
        .Take(size)
        .ToListAsync();

        var totalRecords = db.Clientes.Count();
        var pagedClients = new PageResult<Cliente>(clients, page, size, totalRecords);

        return Results.Ok(clients);
    });



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clientes}/{action=Add}/{id?}");

app.Run();
