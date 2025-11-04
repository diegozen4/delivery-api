# 1. Arquitectura de la Solución

La API sigue los principios de **Clean Architecture** para asegurar una separación clara de responsabilidades, alta mantenibilidad y facilidad para las pruebas. Esta arquitectura organiza el código en capas concéntricas, donde las dependencias siempre apuntan hacia adentro, protegiendo el núcleo del negocio de los detalles de implementación externos.

El código está organizado en cuatro proyectos principales:

![Clean Architecture Diagram](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)
*Fuente: Uncle Bob Martin*

---

## 1.1. `Domain`

- **Propósito:** Es el **núcleo** de la aplicación. Contiene la lógica y las reglas de negocio más fundamentales, sin dependencias de ninguna otra capa.
- **Contenido Típico:**
    - **Entidades:** Clases que representan los objetos del negocio (`User`, `Product`, `Order`, `Commerce`, `DeliveryGroup`). No tienen dependencias de frameworks o librerías externas.
    - **Enums:** Enumeraciones que definen estados fijos (`OrderStatus`, `AssignmentMode`).
    - **Value Objects:** Objetos inmutables que representan atributos descriptivos, como `Price` o `Address`.
- **Regla Clave:** Este proyecto **no depende de ningún otro proyecto** en la solución. Es completamente independiente de la base de datos, la UI o cualquier framework.

---

## 1.2. `Application`

- **Propósito:** Orquesta los casos de uso de la aplicación. Define qué hace el sistema, pero no cómo lo hace.
- **Contenido Típico:**
    - **Interfaces (Contratos):** Define los "contratos" que la capa de `Infrastructure` debe implementar. Ejemplos: `IProductRepository`, `IAuthService`, `IJwtTokenGenerator`. Esto permite aplicar el Principio de Inversión de Dependencias.
    - **Servicios de Aplicación / Casos de Uso:** Clases que contienen la lógica para los casos de uso (ej: `ProductService`, `OrderService`). Utilizan las interfaces de los repositorios para interactuar con los datos, pero no conocen la implementación concreta (ej. no saben que se usa Entity Framework).
    - **DTOs (Data Transfer Objects):** Aunque residen en el proyecto `Contracts`, son utilizados por la capa de aplicación para definir la forma de los datos que fluyen hacia y desde las capas externas.
- **Regla Clave:** Depende de `Domain`, pero no depende de `Infrastructure` ni de `WebAPI`.

---

## 1.3. `Infrastructure`

- **Propósito:** Contiene las implementaciones de las interfaces definidas en `Application`. Es el lugar para todo lo que tiene que ver con el mundo exterior: bases de datos, servicios de autenticación, sistemas de archivos, APIs de terceros, etc.
- **Contenido Típico:**
    - **Persistencia:**
        - `ApplicationDbContext`: La implementación de `DbContext` de Entity Framework Core, que define los `DbSet` y la configuración del modelo.
        - `Migrations`: Las migraciones de la base de datos generadas por EF Core.
    - **Repositorios:** Clases que implementan las interfaces de repositorio (`ProductRepository`, `OrderRepository`), usando EF Core para consultar la base de datos PostgreSQL.
    - **Servicios Externos:** Implementaciones de servicios como `AuthService` (usando ASP.NET Core Identity) y `JwtTokenGenerator` (usando librerías de JWT).
- **Regla Clave:** Depende de `Application` y `Domain`. Actúa como el puente entre la lógica de negocio y las herramientas externas.

---

## 1.4. `WebAPI`

- **Propósito:** Es el punto de entrada a la aplicación. Expone la lógica de negocio al mundo exterior a través de una API REST.
- **Contenido Típico:**
    - **Controladores:** Clases `Controller` que definen los endpoints de la API (`/api/products`, `/api/auth/login`). Reciben peticiones HTTP, las validan y las pasan a los servicios de la capa `Application`.
    - **`Program.cs`:** El archivo de arranque de la aplicación donde se configura el pipeline de HTTP, se registran todos los servicios para la inyección de dependencias (DI) y se configura el middleware (autenticación, autorización, logging, CORS).
    - **Middleware:** Componentes personalizados para manejar tareas transversales, como la gestión global de errores (`ErrorHandlingMiddleware`).
- **Regla Clave:** Depende de todos los demás proyectos (`Application`, `Infrastructure`, `Domain`, `Contracts`). Es la capa más externa y volátil.

---

## 1.5. Flujo de una Petición

Un caso de uso típico, como "crear un producto", sigue este flujo:

1.  Una petición `POST /api/products` llega a `ProductsController` en **`WebAPI`**.
2.  El controlador valida el DTO de entrada y llama al método `CreateProductAsync` en el servicio `IProductService` de la capa **`Application`**.
3.  El `ProductService` ejecuta la lógica de negocio (validaciones, etc.) y utiliza la interfaz `IProductRepository` para persistir la nueva entidad `Product`.
4.  La implementación de `ProductRepository` en **`Infrastructure`** recibe la entidad, la añade al `ApplicationDbContext` y guarda los cambios en la base de datos PostgreSQL.
5.  Los datos (si es necesario) viajan de vuelta a través de las capas. La entidad del `Domain` se mapea a un DTO en la capa de `Application` o `WebAPI` antes de ser devuelta como una respuesta JSON al cliente.
