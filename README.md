# Taller Avanzado: Plataforma Educativa con Arquitectura Limpia y Docker

---

## Objetivo General

Desarrollar una **API REST completa** aplicando arquitectura por capas (API, Application, Domain, Infrastructure), **Entity Framework Core**, **MySQL** y **Docker**, que permita administrar estudiantes, profesores, cursos, secciones, inscripciones y calificaciones.

El trabajo se desarrollará por equipos (4–6 integrantes) y será dockerizable (API + DB + herramienta de administración).

---

## Objetivos de Aprendizaje

Los estudiantes deben demostrar dominio de:

- Arquitectura limpia con separación de capas.
- EF Core con migraciones y seed data.
- Inyección de dependencias.
- Relaciones entre entidades (1–N, N–N).
- Pruebas unitarias básicas en la capa de aplicación.
- Consumo de endpoints desde Postman.
- Dockerización y despliegue local con docker-compose.

---

## Contexto del Problema

Una institución educativa necesita una plataforma para gestionar estudiantes, profesores, cursos, secciones, inscripciones y calificaciones.

Actualmente toda la información se maneja en hojas de cálculo, y la institución quiere migrar a un sistema moderno con base de datos y API REST para futuras integraciones.

---

## Requerimientos Técnicos

### 1. Estructura del Proyecto

Crear una solución con los siguientes proyectos:

- **webEscuela.Api** → Controladores, configuración de dependencias.
- **webEscuela.Application** → Servicios, lógica de negocio y pruebas unitarias.
- **webEscuela.Domain** → Entidades, interfaces y reglas de negocio.
- **webEscuela.Infrastructure** → DbContext, repositorios e implementación con EF Core.

### 2. Base de Datos

- **MySQL**.
- **EF Core** con migraciones.
- Datos iniciales (profesores, cursos de ejemplo, etc.).
- Las relaciones deben crearse correctamente (clave foránea o tabla intermedia).

### 3. Dockerización

- Un **Dockerfile** para la API.
- Un **docker-compose.yml** que levante:
  - API .NET
  - Base de datos MySQL
  - Adminer o phpMyAdmin para administración
- Variables de entorno para conexión a la base de datos.
- Persistencia de datos con volúmenes.

### 4. Pruebas

- Al menos **2 pruebas unitarias** en la capa Application (por ejemplo: validaciones de negocio o lógica de inscripción).
- Se pueden usar **xUnit** o **NUnit**.

### 5. Documentación

- **README** con pasos para ejecutar el proyecto (local y Docker).
- Archivo **.env.example** con variables requeridas.
- Colección de **Postman** con los endpoints principales.
- Breve descripción del trabajo en equipo (quién hizo qué).

---

## Requerimientos Funcionales

### 1. Estudiantes

- Registrar nuevo estudiante.
- Listar todos los estudiantes.
- Obtener estudiante por ID.
- Actualizar información del estudiante.
- Eliminar estudiante.

### 2. Profesores

- Registrar nuevo profesor.
- Listar todos los profesores.
- Obtener profesor por ID.
- Actualizar información del profesor.
- Eliminar profesor.

### 3. Cursos

- Crear nuevo curso.
- Listar todos los cursos.
- Obtener curso por ID.
- Actualizar curso.
- Eliminar curso.
- Asignar un profesor responsable (relación 1–N).

### 4. Secciones / Horarios

- Crear secciones para cada curso (día, hora, aula, cupo máximo).
- Listar secciones por curso.
- Actualizar una sección.
- Validar que los horarios no se solapen.

### 5. Inscripciones

- Registrar inscripción de un estudiante en una sección.
- Listar todas las inscripciones.
- Consultar cursos en los que está inscrito un estudiante.
- Consultar estudiantes inscritos en un curso.
- Evitar inscribir un estudiante en dos secciones con el mismo horario.

### 6. Calificaciones

- Registrar o actualizar la nota de un estudiante en una sección.
- Consultar las calificaciones de un estudiante.
- Consultar promedio de calificaciones por curso.

---

## Relaciones entre Entidades

- **Profesor** 1–N **Curso**
- **Curso** 1–N **Sección**
- **Estudiante** N–N **Sección** (mediante Inscripción)
- **Inscripción** 1–1 **Calificación**

---

## Reglas de Negocio Sugeridas

- Un estudiante no puede inscribirse en una sección si el cupo máximo fue alcanzado.
- Un estudiante no puede inscribirse en dos secciones con horario superpuesto.
- No se puede eliminar un curso que tenga secciones activas.
- Si se elimina una inscripción, también se deben eliminar sus calificaciones asociadas.

---

## Casos Mínimos de Prueba Unitaria

### 1. InscripciónService

- No permitir inscripción si el cupo está lleno.
- No permitir inscripción si hay conflicto de horario.

### 2. CalificaciónService

- Calcular correctamente el promedio de notas por curso.

---

## Entregables

### 1. Repositorio en GitHub (uno por equipo) con:

- Solución completa en C#.
- docker-compose.yml funcional.
- Migraciones creadas.
- .env.example con variables de entorno.
- Pruebas unitarias funcionando.
- README detallado con pasos de ejecución.
- Colección Postman exportada (.json).

### 2. Carpeta docs/ (opcional):

- Diagrama de relaciones o UML.
- Documentación del modelo de dominio.
- Archivo EVALUACION.md con roles del equipo.

---

**Fin del Documento**
