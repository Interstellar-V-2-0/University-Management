using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;

namespace UniMgmt.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            // Evitar duplicar datos
            if (context.Students.Any()) return;

            // Profesores
            var professors = new List<Professor>
            {
                new Professor { Name = "Laura", LastName = "Gómez", Email = "laura.gomez@uni.edu", PhoneNumber = "3001112233", specialty = "Matemáticas" },
                new Professor { Name = "Carlos", LastName = "Ramírez", Email = "carlos.ramirez@uni.edu", PhoneNumber = "3002223344", specialty = "Programación" },
                new Professor { Name = "María", LastName = "Torres", Email = "maria.torres@uni.edu", PhoneNumber = "3003334455", specialty = "Historia" },
                new Professor { Name = "Sofía", LastName = "Fernández", Email = "sofia.fernandez@uni.edu", PhoneNumber = "3004445566", specialty = "Inglés" },
                new Professor { Name = "Andrés", LastName = "Vega", Email = "andres.vega@uni.edu", PhoneNumber = "3005556677", specialty = "Física" }
            };
            await context.Professors.AddRangeAsync(professors);
            await context.SaveChangesAsync();

            // Cursos
            var courses = new List<Course>
            {
                new Course { Name = "Cálculo I", Description = "Fundamentos de cálculo diferencial", ProfessorId = professors[0].Id },
                new Course { Name = "Programación Avanzada", Description = "Conceptos de POO y estructuras de datos", ProfessorId = professors[1].Id },
                new Course { Name = "Historia Universal", Description = "Evolución de las civilizaciones antiguas", ProfessorId = professors[2].Id },
                new Course { Name = "Inglés Técnico", Description = "Inglés aplicado a la tecnología", ProfessorId = professors[3].Id }
            };
            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();

            // Secciones
            var sections = new List<Section>
            {
                new Section { CourseId = courses[0].Id, DaySection = "Lunes", StarTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Classroom = "A1", CapacityMax = 30 },
                new Section { CourseId = courses[1].Id, DaySection = "Martes", StarTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), Classroom = "B2", CapacityMax = 25 },
                new Section { CourseId = courses[2].Id, DaySection = "Miércoles", StarTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(15, 0, 0), Classroom = "C3", CapacityMax = 20 },
                new Section { CourseId = courses[3].Id, DaySection = "Jueves", StarTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Classroom = "D4", CapacityMax = 25 }
            };
            await context.Sections.AddRangeAsync(sections);
            await context.SaveChangesAsync();

            // Estudiantes
            var students = new List<Student>
            {
                new Student { Name = "Daniel", LastName = "Ariza", Email = "daniel.ariza@uni.edu", PhoneNumber = "3101112233" },
                new Student { Name = "Valentina", LastName = "Suárez", Email = "valentina.suarez@uni.edu", PhoneNumber = "3102223344" },
                new Student { Name = "Camilo", LastName = "Gómez", Email = "camilo.gomez@uni.edu", PhoneNumber = "3103334455" },
                new Student { Name = "Mariana", LastName = "Rodríguez", Email = "mariana.rodriguez@uni.edu", PhoneNumber = "3104445566" },
                new Student { Name = "Santiago", LastName = "Martínez", Email = "santiago.martinez@uni.edu", PhoneNumber = "3105556677" },
                new Student { Name = "Lucía", LastName = "Pérez", Email = "lucia.perez@uni.edu", PhoneNumber = "3106667788" },
                new Student { Name = "Andrés", LastName = "Moreno", Email = "andres.moreno@uni.edu", PhoneNumber = "3107778899" },
                new Student { Name = "Laura", LastName = "Castaño", Email = "laura.castano@uni.edu", PhoneNumber = "3108889900" },
                new Student { Name = "Felipe", LastName = "Rojas", Email = "felipe.rojas@uni.edu", PhoneNumber = "3109990011" },
                new Student { Name = "Isabela", LastName = "Navarro", Email = "isabela.navarro@uni.edu", PhoneNumber = "3111112233" }
            };
            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();

            // Inscripciones
            var inscriptions = new List<Inscription>
            {
                new Inscription { StudentId = students[0].Id, SectionId = sections[0].Id, RegistrationDate = DateTime.Now.AddDays(-10) },
                new Inscription { StudentId = students[1].Id, SectionId = sections[1].Id, RegistrationDate = DateTime.Now.AddDays(-9) },
                new Inscription { StudentId = students[2].Id, SectionId = sections[2].Id, RegistrationDate = DateTime.Now.AddDays(-8) },
                new Inscription { StudentId = students[3].Id, SectionId = sections[3].Id, RegistrationDate = DateTime.Now.AddDays(-7) },
                new Inscription { StudentId = students[4].Id, SectionId = sections[0].Id, RegistrationDate = DateTime.Now.AddDays(-6) },
                new Inscription { StudentId = students[5].Id, SectionId = sections[1].Id, RegistrationDate = DateTime.Now.AddDays(-5) },
                new Inscription { StudentId = students[6].Id, SectionId = sections[2].Id, RegistrationDate = DateTime.Now.AddDays(-4) },
                new Inscription { StudentId = students[7].Id, SectionId = sections[3].Id, RegistrationDate = DateTime.Now.AddDays(-3) }
            };
            await context.Inscriptions.AddRangeAsync(inscriptions);
            await context.SaveChangesAsync();

            // Calificaciones
            var qualifications = new List<Qualification>
            {
                new Qualification { InscriptionId = inscriptions[0].Id, Note = 4.5, Observations = "Excelente rendimiento" },
                new Qualification { InscriptionId = inscriptions[1].Id, Note = 3.8, Observations = "Buen desempeño" },
                new Qualification { InscriptionId = inscriptions[2].Id, Note = 4.2, Observations = "Participativo y constante" },
                new Qualification { InscriptionId = inscriptions[3].Id, Note = 3.5, Observations = "Cumple con lo requerido" },
                new Qualification { InscriptionId = inscriptions[4].Id, Note = 4.7, Observations = "Destacado" }
            };
            await context.Qualifications.AddRangeAsync(qualifications);
            await context.SaveChangesAsync();
        }
    }
}
