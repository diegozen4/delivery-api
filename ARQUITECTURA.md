# Arquitectura de la API

La API sigue los principios de **Clean Architecture** (Arquitectura Limpia) para asegurar una separación clara de responsabilidades, alta mantenibilidad y facilidad para las pruebas.

El código está organizado en cuatro proyectos principales:

## 1. `Domain`

- **Propósito:** Es el núcleo de la aplicación. Contiene la lógica y las reglas de negocio más fundamentales.
- **Contenido Típico:**
    - **Entidades:** Las clases que representan los objetos del negocio (`User`, `Product`, `Order`, `Commerce`, etc.). No tienen dependencias de ninguna otra capa.
    - **Enums:** Enumeraciones que definen estados fijos (`OrderStatus`, etc.).
- **Regla Clave:** Este proyecto **no depende de ningún otro proyecto** en la solución. Es completamente independiente de la base de datos, la UI o cualquier framework externo.

## 2. `Application`

- **Propósito:** Orquesta la lógica de negocio. Define los casos de uso de la aplicación.
- **Contenido Típico:**
    - **Interfaces:** Define los "contratos" que la capa de `Infrastructure` debe implementar. Ejemplos: `IProductRepository`, `IAuthService`, `IJwtTokenGenerator`. Esto permite invertir las dependencias.
    - **Servicios de Aplicación:** Clases que contienen la lógica para los casos de uso (ej: `ProductService`). Utilizan las interfaces de los repositorios para interactuar con los datos, pero no saben cómo están implementados.
- **Regla Clave:** Depende de `Domain`, pero no depende de `Infrastructure` ni de `WebAPI`.

## 3. `Infrastructure`

- **Propósito:** Contiene las implementaciones de las interfaces definidas en `Application`. Es el lugar para todo lo que tiene que ver con el mundo exterior: bases de datos, servicios de email, sistemas de archivos, etc.
- **Contenido Típico:**
    - **Persistencia:**
        - `ApplicationDbContext`: La implementación de `DbContext` de Entity Framework Core.
        - `Migrations`: Las migraciones de la base de datos.
    - **Repositorios:** Clases que implementan las interfaces de repositorio (`ProductRepository`, etc.), usando EF Core para consultar la base de datos PostgreSQL.
    - **Servicios Externos:** Implementaciones de servicios como `AuthService` y `JwtTokenGenerator`, que dependen de frameworks como ASP.NET Core Identity.
- **Regla Clave:** Depende de `Application` y `Domain`. Es el puente entre la lógica de negocio y las herramientas externas.

## 4. `WebAPI`

- **Propósito:** Es el punto de entrada a la aplicación. Expone la lógica de negocio al mundo exterior a través de una API REST.
- **Contenido Típico:**
    - **Controladores:** Clases `Controller` que definen los endpoints de la API (`/api/products`, `/api/auth/login`).
    - **`Program.cs`:** El archivo de configuración donde se registran todos los servicios, se configura el middleware (como autenticación y autorización) y se construye la aplicación.
    - **DTOs (en el proyecto `Contracts`):** Aunque es un proyecto separado, `Contracts` está estrechamente ligado a la `WebAPI`. Define la forma de los datos que entran y salen de la API (`RegisterUserRequest`, `ProductDto`, etc.).
- **Regla Clave:** Depende de todos los demás proyectos (`Application`, `Infrastructure`, `Domain`, `Contracts`). Es la capa más externa.

### Flujo de una Petición

1.  Una petición HTTP llega a un **Controlador** en `WebAPI`.
2.  El Controlador llama a un método en un **Servicio de Aplicación** (ej: `IProductService`).
3.  El Servicio de Aplicación ejecuta la lógica de negocio, para lo cual llama a uno o más **Repositorios** (ej: `IProductRepository`).
4.  El Repositorio, implementado en `Infrastructure`, traduce la llamada a una consulta de **Entity Framework Core** contra la base de datos.
5.  Los datos (generalmente **Entidades** del `Domain`) viajan de vuelta a través de las capas.
6.  En la capa de `Application` o `WebAPI`, las Entidades se mapean a **DTOs** antes de ser devueltas como una respuesta JSON al cliente.
