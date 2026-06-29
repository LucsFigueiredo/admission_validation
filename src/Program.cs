using admission_validation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddScoped<FileStorageService>();
builder.Services.AddScoped<DocumentValidationService>();
builder.Services.AddScoped<OcrService>();
builder.Services.AddScoped<RgValidator>();
builder.Services.AddScoped<CpfValidator>();
builder.Services.AddScoped<AdressValidator>();
builder.Services.AddScoped<PisValidator>();
builder.Services.AddScoped<ExtratoValidator>();
builder.Services.AddScoped<NascimentoCasamentoValidator>();
builder.Services.AddScoped<AntecedentesValidator>();
builder.Services.AddScoped<DiplomaValidator>();
builder.Services.AddScoped<HistoricoValidator>();
builder.Services.AddScoped<BensValidator>();
builder.Services.AddScoped<ProventosValidator>();
builder.Services.AddScoped<AcumuloValidator>();
builder.Services.AddScoped<VoterValidator>();
builder.Services.AddScoped<DocumentValidationService>();
builder.Services.AddScoped<TextHelper>();

var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();