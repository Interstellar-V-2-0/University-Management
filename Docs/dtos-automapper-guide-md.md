# Taller Célula – Sistema de Gestión Universitaria

---

## 1. Introducción y Objetivo

Esta guía explica la correcta implementación de **Data Transfer Objects (DTOs)** y el uso de **AutoMapper** dentro de una arquitectura basada en **Domain Driven Design (DDD)**. Se detalla su propósito, su posición dentro del sistema y las mejores prácticas para garantizar un código limpio, mantenible y alineado con los principios de arquitectura limpia.

El objetivo es establecer un estándar para la creación, mapeo e implementación de DTOs en un sistema educativo basado en DDD. Los DTOs permiten transportar datos entre capas sin exponer las entidades de dominio, y AutoMapper facilita la conversión entre objetos de forma automática y segura.

### Contexto del Negocio

El sistema debe gestionar estudiantes, cursos y matrículas (enrollments), garantizando separación de responsabilidades y consistencia de datos entre capas.

---

## 2. Conceptos de DTOs y AutoMapper en DDD

### ¿Qué son los DTOs?

Un **DTO (Data Transfer Object)** es una clase que transporta datos entre la capa de aplicación o dominio y la capa de presentación (API). No contiene lógica de negocio, solo propiedades simples.

### ¿Qué es AutoMapper?

**AutoMapper** es una librería que automatiza la conversión entre DTOs y entidades, reduciendo código repetitivo.

### Analogía

Los DTOs son como **formularios administrativos**: solo muestran o recogen la información necesaria. AutoMapper es el **asistente que traduce** la información del lenguaje interno del sistema al formato que espera el cliente.

---

## 3. Arquitectura DDD y Ubicación de DTOs

En una arquitectura DDD, los DTOs deben ubicarse en la capa de **API**, en una carpeta `Dtos/Entidad/`. Esto mantiene el dominio libre de dependencias de transporte.

### Capas Principales

**Domain**: Entidades, reglas de negocio y Value Objects.

**Application**: Casos de uso, servicios y lógica de aplicación.

**Infrastructure**: Acceso a datos y servicios externos.

**API**: Controladores, DTOs, perfiles AutoMapper.

### Analogía

- **Domain** → Reglamento académico
- **Application** → Dirección académica
- **Infrastructure** → Secretaría técnica
- **API** → Ventanilla estudiantil

---

## 4. Estructura de Carpetas del Proyecto

```
UniversityMgmt/
│
├─ UniversityMgmt.API/                     # Capa de presentación (endpoints REST)
│   ├─ Controllers/
│   │   ├─ StudentsController.cs
│   │   ├─ CoursesController.cs
│   │   └─ EnrollmentsController.cs
│   │
│   ├─ Dtos/
│   │   ├─ Student/
│   │   │   ├─ CreateStudentDto.cs        # Para crear
│   │   │   ├─ UpdateStudentDto.cs        # Para actualizar
│   │   │   └─ StudentDto.cs              # Para listar/ver
│   │   │
│   │   ├─ Course/
│   │   │   ├─ CreateCourseDto.cs
│   │   │   ├─ UpdateCourseDto.cs
│   │   │   └─ CourseDto.cs
│   │   │
│   │   └─ Enrollment/
│   │       ├─ CreateEnrollmentDto.cs
│   │       ├─ EnrollmentDto.cs
│   │       └─ EnrollmentDetailDto.cs
│   │
│   └─ Profiles/
│       ├─ StudentProfile.cs
│       ├─ CourseProfile.cs
│       └─ EnrollmentProfile.cs
│
├─ UniversityMgmt.Application/             # Lógica de aplicación (servicios y casos de uso)
│   ├─ Interfaces/
│   ├─ Services/
│   └─ Validators/
│
├─ UniversityMgmt.Domain/                  # Reglas de negocio puras
│   ├─ Entities/
│   ├─ ValueObjects/
│   ├─ Interfaces/
│   └─ Exceptions/
│
└─ UniversityMgmt.Infrastructure/          # Implementación técnica (repositorios, EF Core)
    ├─ Persistence/
    ├─ Repositories/
    └─ DependencyInjection/
```

---

## 5. Ejemplos de Perfiles AutoMapper

Cada agregado principal (Student, Course, Enrollment) debe tener su propio perfil AutoMapper. Esto mantiene el código organizado y facilita la trazabilidad.

### StudentProfile

```csharp
public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, CreateStudentDto>().ReverseMap();
        CreateMap<Student, UpdateStudentDto>().ReverseMap();
    }
}
```

### CourseProfile

```csharp
public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CreateCourseDto>().ReverseMap();
        CreateMap<Course, UpdateCourseDto>().ReverseMap();
    }
}
```

### EnrollmentProfile

```csharp
public class EnrollmentProfile : Profile
{
    public EnrollmentProfile()
    {
        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        CreateMap<Enrollment, CreateEnrollmentDto>().ReverseMap();
        CreateMap<Enrollment, EnrollmentDetailDto>().ReverseMap();
    }
}
```

---

## 6. Detalle del Campo Calculado FullName

En el perfil de Student, se utiliza la función `ForMember` para generar una propiedad derivada `FullName`, combinando `FirstName` y `LastName` del dominio.

```csharp
CreateMap<Student, StudentDto>()
    .ForMember(dest => dest.FullName, 
               opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
```

Esto permite que el dominio se mantenga puro (sin lógica de presentación), mientras que la API entrega un dato legible al cliente.

---

## 7. Tipos de Mapeos y Buenas Prácticas

### Tipos de Mapeos en AutoMapper

**CreateMap<>()**: Mapea propiedades con el mismo nombre.

**ReverseMap()**: Permite mapear en ambos sentidos (DTO ↔ Entidad).

**ForMember()**: Personaliza el mapeo o crea campos calculados.

**MapFrom()**: Define de dónde proviene un valor cuando los nombres no coinciden.

### Reglas Clave

✓ Un perfil por agregado raíz.

✓ No mezclar entidades distintas en un mismo perfil.

✓ Mantener DTOs en la capa API.

✓ Controladores delgados, lógica en Application.

✓ Evitar duplicar mapeos con nombres coherentes.

---

## 8. Flujo de Datos

El flujo de datos entre capas sigue un patrón claro y desacoplado:

```
[Domain Entity] → [AutoMapper Profile] → [DTO] → [Controller Response]
```

### Ejemplo

```
Student (Entidad)
    ↓
StudentProfile (Mapeo con FullName)
    ↓
StudentDto
    ↓
Controlador devuelve JSON al cliente
```

---

## 9. Buenas Prácticas Finales

✓ Un perfil AutoMapper por agregado raíz.

✓ DTOs solo en la capa API.

✓ Validaciones en Application, no en controladores.

✓ AutoMapper reduce errores humanos al mapear manualmente.

✓ Documentar todos los DTOs para mantener trazabilidad del API.

---

## 10. Analogía Final: Universidad

- **Domain** → Reglamento académico (define las reglas y restricciones del sistema).
- **Application** → Dirección académica (coordina la aplicación de las reglas).
- **Infrastructure** → Secretaría técnica (maneja la información técnica y persistencia).
- **API** → Ventanilla estudiantil (interactúa con estudiantes y personal).

Así, cada capa cumple un rol claro y trabaja de manera coordinada, manteniendo independencia y orden.

---

**Fin del Documento**