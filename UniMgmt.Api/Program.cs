using Microsoft.EntityFrameworkCore;
using UniMgmt.Application.Interfaces;
using UniMgmt.Application.Services;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;
using UniMgmt.Infrastructure.Repositories;
using UniMgmt.Api.Profiles;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Obtener cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("Default");

// 🔹 Registrar servicios de controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Configurar DbContext con MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 🔹 Registrar servicios de aplicación
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IInscriptionService, InscriptionService>();
builder.Services.AddScoped<IQualificationService, QualificationService>();
builder.Services.AddScoped<ISectionService, SectionService>();

// 🔹 Registrar repositorios
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IInscriptionRepository, InscriptionRepository>();
builder.Services.AddScoped<IQualificationRepository, QualificationRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();

// 🔹 Registrar AutoMapper (forma recomendada en AutoMapper 15.x)
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<StudentProfile>();
    cfg.AddProfile<CourseProfile>();
    cfg.AddProfile<ProfessorProfile>();
    cfg.AddProfile<InscriptionProfile>();
    cfg.AddProfile<QualificationProfile>();
    cfg.AddProfile<SectionProfile>();
});

var app = builder.Build();

// 🔹 Ejecutar el seeder al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    await DatabaseSeeder.SeedAsync(context);
}

// 🔹 Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
