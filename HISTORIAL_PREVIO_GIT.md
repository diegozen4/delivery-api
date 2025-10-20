# Historial de Desarrollo (Previo a la Implementación de Git)

Este documento sirve como un registro del trabajo de desarrollo realizado en el proyecto `delivery-api` antes de la inicialización del control de versiones con Git.

## 1. Configuración Inicial del Proyecto

- **Estructura del Proyecto:** Se estableció una solución de .NET siguiendo los principios de **Clean Architecture**, separando las responsabilidades en cuatro proyectos principales:
    - `Domain`: Contiene las entidades del negocio y la lógica de dominio más pura.
    - `Application`: Orquesta la lógica de negocio, definiendo interfaces para los repositorios y servicios.
    - `Infrastructure`: Implementa la lógica de acceso a datos (repositorios, Entity Framework Core) y servicios externos.
    - `WebAPI`: Expone la funcionalidad de la aplicación a través de una API REST.
- **Base de Datos:** Se configuró el proyecto para usar **PostgreSQL** como motor de base de datos a través de Entity Framework Core.
- **Entidades Iniciales:** Se crearon las entidades de dominio básicas como `Product`, `Commerce`, `Category`, `User`, `Order`, entre otras.

## 2. Implementación de CRUDs Base

Se implementaron las operaciones CRUD (Crear, Leer, Actualizar, Eliminar) completas para las entidades principales:

- **CategoriesController:** Endpoints para gestionar las categorías de productos.
- **CommercesController:** Endpoints para gestionar los comercios/negocios.
- **ProductsController:** Endpoints para gestionar los productos.

Para cada entidad, se siguió el flujo de Clean Architecture, creando Repositorios, Servicios, Interfaces y DTOs (Data Transfer Objects) en sus respectivos proyectos.

## 3. Refactorización del Modelo de Datos

- **Relación `Category` y `Commerce`:** Se identificó que la relación inicial de "uno a muchos" (un comercio tiene muchas categorías) no era escalable.
- **Cambio a Muchos-a-Muchos:** Se refactorizó el modelo a una relación de "muchos a muchos", permitiendo la creación de categorías globales que pueden ser utilizadas por múltiples comercios. Esto se logró modificando las entidades y aplicando una nueva migración a la base de datos.

## 4. Implementación del Sistema de Autenticación y Autorización

Esta fue una fase de trabajo mayor que incluyó:

- **Adopción de ASP.NET Core Identity:** Se decidió usar el sistema de identidad de Microsoft por su robustez y seguridad.
- **Refactorización de Entidades:** Las entidades `User` y `Role` fueron modificadas para heredar de `IdentityUser<Guid>` y `IdentityRole<Guid>`, integrándose con el framework.
- **Actualización de `DbContext`:** `ApplicationDbContext` fue actualizado para heredar de `IdentityDbContext`, permitiendo a EF Core gestionar las tablas de Identity.
- **Configuración de Servicios:** En `Program.cs`, se configuraron los servicios de Identity y la autenticación basada en **JSON Web Tokens (JWT)**.
- **Implementación de `AuthService`:**
    - Se creó la lógica para el registro de nuevos usuarios, asignándoles el rol por defecto "Cliente".
    - Se implementó la lógica de login, que verifica las credenciales y genera un JWT.
- **Creación de `AuthController`:** Se expusieron los endpoints públicos `POST /api/auth/register` y `POST /api/auth/login`.

## 5. Corrección de Bugs y Mejoras

- **Error `FirebaseId` No Nulo:** Durante las pruebas de registro, se detectó un error de base de datos porque la columna `FirebaseId` no aceptaba valores nulos.
    - **Solución:** Se modificó la entidad `User` para que `FirebaseId` sea una propiedad que acepte nulos (`string?`) y se aplicó la migración correspondiente a la base de datos.
- **Funcionalidad "Cambiar Contraseña":**
    - Se implementó un nuevo endpoint protegido `POST /api/auth/change-password`.
    - Solo los usuarios autenticados (que presenten un JWT válido) pueden usarlo.
    - La lógica extrae el ID de usuario del token y utiliza el método seguro `UserManager.ChangePasswordAsync` para realizar el cambio.

---
*Fin del historial previo a Git.*
