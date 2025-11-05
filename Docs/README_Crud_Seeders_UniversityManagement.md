# Documentación de Desarrollo - University Management API

## Fecha
2025-11-05

## Actividades de Programación Realizadas

# 1. Seeder
- Se implementó un seeder para poblar la base de datos con datos iniciales.
- Entidades incluidas en el seeder:
  - Professors
  - Students
  - Courses
  - Sections
  - Enrollments
  - Qualifications
- Objetivo: asegurar que al arrancar la aplicación haya datos de prueba para probar los endpoints.

**Ejemplo de uso:**
```csharp
await SeedData.InitializeAsync(context);
```
# 2. Controladores
Se crearon controladores RESTful para manejar CRUD de las entidades principales:

CoursesController

StudentsController

ProfessorsController

SectionsController

EnrollmentsController

QualificationsController

Estructura de un controlador típico:

```csharp

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IService<Course> _service;

    public CoursesController(IService<Course> service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
    {
        var course = _mapper.Map<Course>(dto);
        await _service.CreateAsync(course);
        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    // GET, PUT, DELETE implementados de manera similar
}
```

# 3. DTOs (Data Transfer Objects)
Se crearon DTOs para separar los modelos de dominio de la entrada/salida HTTP:

CreateCourseDto, UpdateCourseDto, CourseDto

CreateStudentDto, StudentDto

CreateProfessorDto, ProfessorDto

CreateSectionDto, SectionDto

CreateInscriptionDto, InscriptionDto

CreateQualificationDto, QualificationDto

Objetivo: Validación de datos y encapsulación de la información que viaja entre cliente y API.

# 4. AutoMapper Profiles
Configuración de mapeo entre DTOs y entidades del dominio.

Ejemplo de Profile:

```csharp

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();
        CreateMap<Course, CourseDto>();
    }
}
```

Objetivo: facilitar la conversión automática entre DTOs y entidades.

# 5. Lógica de CRUD
Se completó la lógica de CRUD en los Services y Repositories:

CreateAsync: Crear registros con validación de llaves foráneas.

GetAllAsync / GetByIdAsync: Recuperar registros individuales o listas.

UpdateAsync: Actualizar registros existentes.

DeleteAsync: Eliminar registros por Id.

Validaciones implementadas:

Comprobar existencia de relaciones (por ejemplo, un Course necesita un ProfessorId existente).

Manejo de errores de base de datos con mensajes claros.

Repositorios genéricos y específicos implementados para Inscription, Section y Qualification:

Nuevos métodos en interfaces y servicios para manejar lógica específica (por ejemplo, verificar si un estudiante ya está inscrito en una sección, o agregar calificaciones a inscripciones existentes).

# 6. Cambios en Repos, Services e Interfaces
Inscription:

Repositorio: métodos para crear, obtener y validar inscripciones.

Service: lógica de negocio para prevenir inscripciones duplicadas.

Interface: IInscriptionRepository y IInscriptionService extendidos.

Section:

Repositorio: CRUD de secciones, incluyendo relaciones con cursos y profesores.

Service: lógica para validar capacidad de sección y relaciones.

Interface: ISectionRepository y ISectionService.

Qualification:

Repositorio: agregar/calcular calificaciones de inscripciones.

Service: lógica de asignación de notas y promedio de estudiante.

Interface: IQualificationRepository y IQualificationService.

# 7. Notas y Observaciones
Es importante crear las entidades padre primero (Professors, Students) para evitar errores de foreign key al crear entidades dependientes (Courses, Sections, Enrollments, Qualifications).

Swagger se puede usar para probar todos los endpoints:

```bash
http://localhost:5017/swagger/index.html
```
La API ya permite realizar operaciones CRUD completas para todas las entidades principales.

# 8. Próximos pasos sugeridos
Probar todos los endpoints con datos reales usando Swagger.

Implementar pruebas unitarias para Services y Repositories.

# 9.  Casos de Negocio – Plataforma Educativa

## 1️ Estudiantes

| Caso de negocio       | Descripción                  | Validaciones / Restricciones                                   |
|----------------------|-----------------------------|----------------------------------------------------------------|
| Registrar estudiante  | Permite crear un nuevo estudiante | Nombre, apellido, email y teléfono obligatorios<br>Email único |
| Listar estudiantes    | Recupera todos los estudiantes | N/A                                                            |
| Obtener estudiante por ID | Consulta un estudiante específico | Debe existir el ID                                             |
| Actualizar estudiante | Modifica datos del estudiante | No se permite dejar campos obligatorios vacíos                |
| Eliminar estudiante   | Elimina un estudiante        | No eliminar si tiene inscripciones activas                     |

---

## 2️ Profesores

| Caso de negocio       | Descripción                  | Validaciones / Restricciones                                   |
|----------------------|-----------------------------|----------------------------------------------------------------|
| Registrar profesor    | Crea un nuevo profesor      | Nombre, apellido y especialidad obligatorios                   |
| Listar profesores     | Recupera todos los profesores | N/A                                                            |
| Obtener profesor por ID | Consulta un profesor específico | Debe existir el ID                                             |
| Actualizar profesor   | Modifica datos del profesor | Campos obligatorios no pueden estar vacíos                    |
| Eliminar profesor     | Elimina un profesor         | No eliminar si tiene cursos asignados                          |

---

## 3️ Cursos

| Caso de negocio       | Descripción                  | Validaciones / Restricciones                                   |
|----------------------|-----------------------------|----------------------------------------------------------------|
| Crear curso           | Crea un nuevo curso          | Nombre y descripción obligatorios<br>Debe asignarse un profesor responsable |
| Listar cursos         | Recupera todos los cursos    | N/A                                                            |
| Obtener curso por ID  | Consulta un curso específico | Debe existir el ID                                             |
| Actualizar curso      | Modifica datos del curso     | No dejar campos obligatorios vacíos                            |
| Eliminar curso        | Elimina un curso             | No se puede eliminar si tiene secciones activas                |

---

## 4️ Secciones

| Caso de negocio       | Descripción                  | Validaciones / Restricciones                                   |
|----------------------|-----------------------------|----------------------------------------------------------------|
| Crear sección         | Añade una sección a un curso | Día, hora, aula y cupo máximo obligatorios<br>No se permiten horarios solapados |
| Listar secciones por curso | Recupera todas las secciones de un curso | N/A                                               |
| Actualizar sección    | Modifica información de la sección | Validar que no se solape con otras secciones            |
| Eliminar sección      | Elimina una sección           | N/A                                                            |

---

## 5️ Inscripciones

| Caso de negocio       | Descripción                  | Validaciones / Restricciones                                   |
|----------------------|-----------------------------|----------------------------------------------------------------|
| Registrar inscripción | Inscribe a un estudiante en una sección | No superar cupo máximo<br>No inscribir en horarios que se solapen |
| Listar inscripciones  | Recupera todas las inscripciones | N/A                                                            |
| Consultar cursos de estudiante | Lista cursos donde está inscrito | N/A                                                   |
| Consultar estudiantes en sección | Lista estudiantes inscritos | N/A                                                    |
| Eliminar inscripción  | Elimina inscripción           | También elimina calificación asociada                          |

---

## 6️ Calificaciones

| Caso de negocio       | Descripción                  | Validaciones / Restricciones                                   |
|----------------------|-----------------------------|----------------------------------------------------------------|
| Registrar/Actualizar calificación | Asigna nota a un estudiante en una sección | Nota válida entre 0 y 5 (o según escala)<br>Debe existir inscripción |
| Consultar calificaciones de estudiante | Lista notas de un estudiante | N/A                                                     |
| Calcular promedio por curso | Calcula promedio de notas | Solo incluye calificaciones existentes                         |

---

##  Reglas de negocio generales
- Un estudiante no puede inscribirse en una sección si el cupo máximo está lleno.
- Un estudiante no puede inscribirse en dos secciones con horario superpuesto.
- No se puede eliminar un curso que tenga secciones activas.
- Si se elimina una inscripción, también se eliminan sus calificaciones asociadas.

---