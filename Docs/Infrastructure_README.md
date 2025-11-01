# 🏫 Proyecto University Management – Capa Infrastructure

Este documento explica de forma simple qué hicimos en la parte de **Interfaces** y **Repositories** del proyecto.  
El objetivo fue organizar la lógica para conectarnos con la base de datos usando buenas prácticas, pero sin hacerlo complicado.

---

## 📘 ¿Qué son las Interfaces?

Una **interfaz** en C# es como un “contrato” que dice **qué métodos debe tener una clase**, pero no cómo los hace.

Por ejemplo, una interfaz puede decir:
> “toda clase que me implemente debe tener los métodos `GetAllAsync`, `GetByIdAsync`, `CreateAsync`, `UpdateAsync` y `DeleteAsync`”.

Esto sirve para que todos los repositorios sigan el mismo formato, aunque cada uno trabaje con entidades diferentes (Person, Student, Course, etc).

En nuestro proyecto hay una interfaz general llamada:

```
IRepository<T>
```

Y cada entidad (por ejemplo, `Person` o `Student`) tiene su propia interfaz específica que hereda de esa interfaz principal.

---

## 🧱 ¿Qué son los Repositories?

Un **repository** es una clase que se encarga de comunicarse con la base de datos.  
Su trabajo es guardar, buscar, actualizar o eliminar datos.

En este caso, los repositorios se encuentran en:

```
UniMgmt.Infrastructure/Repositories/
```

y cada uno maneja una entidad diferente:

| Entidad | Archivo Repository |
|----------|--------------------|
| Person | `PersonRepository.cs` |
| Student | `StudentRepository.cs` |
| Professor | `ProfessorRepository.cs` |
| Course | `CourseRepository.cs` |
| Section | `SectionRepository.cs` |
| Inscription | `InscriptionRepository.cs` |
| Qualification | `QualificationRepository.cs` |

---

## ⚙️ ¿Cómo funcionan?

Cada repositorio usa el **AppDbContext**, que es la clase que conecta con la base de datos.  
Los métodos se hacen con **async/await** para que el programa no se congele mientras espera una respuesta.

Por ejemplo, el `PersonRepository` tiene métodos como:

```csharp
public async Task<IEnumerable<Person>> GetAllAsync()
{
    return await _context.Persons.ToListAsync();
}

public async Task<Person?> GetByIdAsync(int id)
{
    return await _context.Persons
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);
}

public async Task<Person> CreateAsync(Person person)
{
    _context.Persons.Add(person);
    await _context.SaveChangesAsync();
    return person;
}
```

🟢 `GetAllAsync` → trae todos los registros  
🔵 `GetByIdAsync` → busca uno por ID  
🟠 `CreateAsync` → agrega un nuevo registro  
🟣 `UpdateAsync` → actualiza un registro existente  
🔴 `DeleteAsync` → borra un registro

---

## 💡 Beneficios de usar Repositories

✔️ El código queda más ordenado y fácil de mantener.  
✔️ Si se cambia la base de datos, no hay que modificar todo el proyecto.  
✔️ Todos los repositorios trabajan igual, gracias a las interfaces.  
✔️ Es más fácil de probar y entender.

---

## 🧩 En resumen

- Las **Interfaces** definen las reglas.
- Los **Repositories** cumplen esas reglas.
- **AppDbContext** conecta todo con la base de datos.
- Todo el código es **asíncrono**, para que el programa sea más rápido y fluido.

---

### ✨ Ejemplo visual

```
Controlador  →  Repository  →  AppDbContext  →  Base de Datos
```

Así, los controladores no necesitan saber cómo funciona la base de datos, solo llaman a los métodos del repositorio.

---
