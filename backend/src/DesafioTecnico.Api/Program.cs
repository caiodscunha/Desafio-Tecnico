using DesafioTecnico.Api.Data;
using DesafioTecnico.Api.Services;
using Microsoft.EntityFrameworkCore;

// Program.cs é o ponto de entrada da aplicação (equivalente ao "main" em outras linguagens).
// A partir do .NET 6, não é mais preciso escrever "static void Main(string[] args)" explicitamente:
// este arquivo inteiro É o Main, em um estilo chamado "top-level statements".
var builder = WebApplication.CreateBuilder(args);

// ---- Registro de serviços (injeção de dependência) ----

// Habilita os Controllers (Controllers/PessoasController.cs, etc.).
builder.Services.AddControllers();

// Configura o EF Core para usar SQLite, lendo a connection string do appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra o serviço de Pessoa: sempre que um Controller pedir IPessoaService,
// o ASP.NET Core vai entregar uma instância de PessoaService.
builder.Services.AddScoped<IPessoaService, PessoaService>();

// Mesma ideia para o serviço de Transação.
builder.Services.AddScoped<ITransacaoService, TransacaoService>();

// Swagger/OpenAPI: gera uma página interativa (/swagger) para testar a API pelo navegador.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Permite que o front-end (rodando em outra porta, ex.: http://localhost:5173) acesse esta API.
const string corsPolicyFrontend = "FrontendPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyFrontend, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Cria o arquivo gastos.db e as tabelas diretamente a partir das classes em Models/,
// caso ainda não existam. Simples e suficiente para este desafio (sem versionamento
// de schema via migrations, que só compensaria em um projeto de longa duração).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// ---- Pipeline HTTP ----

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyFrontend);

app.UseAuthorization();

app.MapControllers();

app.Run();
