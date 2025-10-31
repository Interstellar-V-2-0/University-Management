
## Estructura del Proyecto
## Cambios realizados

### Definición de entidades principales
Se crearon las clases en `Domain/Entities/`:
- `Student.cs`
- `Professor.cs`
- `Course.cs`
- `Section.cs`
- `Inscription.cs`
- `Qualification.cs`

Cada clase contiene sus propiedades y claves primarias, además de las relaciones de navegación correspondientes.

---

### Configuración de relaciones
En `AppDbContext.cs` (capa **Infrastructure**) se definieron las relaciones:
- **Profesor 1–N Curso**
- **Curso 1–N Sección**
- **Estudiante N–N Sección** (mediante `Inscription`)
- **Inscripción 1–1 Calificación**

---

### Creación de interfaces base
Se implementaron las interfaces genéricas y específicas en `Domain/Interfaces/`:
- `IRepository<T>` → Define operaciones CRUD comunes.
- Interfaces específicas:  
  `IStudentRepository`, `IProfessorRepository`, `ICourseRepository`,  
  `ISectionRepository`, `IInscriptionRepository`, `IQualificationRepository`.

Estas interfaces serán implementadas en la capa `Infrastructure` con EF Core.

---

## Configuración de entorno local
## Configurar la base de datos

1. **Crear base de datos en MySQL**

```sql
CREATE DATABASE university_db CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
Actualizar la cadena de conexión en UniMgmt.Api/appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=university_db;user=root;password=1234;"
  }
}


Verificar AppDbContextFactory.cs en UniMgmt.Infrastructure/Data:
Esta clase se encarga de construir el contexto en tiempo de diseño para que EF Core pueda ejecutar migraciones correctamente.

Crear y aplicar migraciones

Ejecutar los siguientes comandos desde la raíz del proyecto:

# Crear migración inicial
dotnet ef migrations add InitialCreate --project UniMgmt.Infrastructure --startup-project UniMgmt.Api

# Aplicar migración a la base de datos
dotnet ef database update --project UniMgmt.Infrastructure --startup-project UniMgmt.Api


Esto generará las tablas correspondientes a las entidades definidas.