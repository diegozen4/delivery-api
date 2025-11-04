# 5. Guía de Instalación y Desarrollo

Esta guía contiene las instrucciones paso a paso para que un nuevo desarrollador pueda clonar, configurar y ejecutar el proyecto `delivery-api` en su entorno local.

---

## 5.1. Prerrequisitos

Asegúrate de tener instalado el siguiente software:

-   **[.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)** (o la versión especificada en `WebAPI.csproj`).
-   **[Git](https://git-scm.com/)**.
-   Un editor de código o IDE, como **[Visual Studio 2022](https://visualstudio.microsoft.com/)** o **[JetBrains Rider](https://www.jetbrains.com/rider/)**.
-   **[PostgreSQL](https://www.postgresql.org/download/)** (puede ser una instalación local o una instancia en Docker).

---

## 5.2. Clonación del Repositorio

Clona el repositorio en tu máquina local usando el siguiente comando:

```bash
git clone <URL_DEL_REPOSITORIO>
cd delivery-api
```

---

## 5.3. Configuración de la Base de Datos

1.  **Crear la Base de Datos:**
    -   Abre una herramienta de gestión de PostgreSQL (como `psql` o DBeaver).
    -   Crea una nueva base de datos para el proyecto. Por convención, puedes llamarla `delivery_db_dev`.

    ```sql
    CREATE DATABASE delivery_db_dev;
    ```

2.  **Configurar la Cadena de Conexión:**
    -   La configuración para el entorno de desarrollo se gestiona a través de **User Secrets** (secretos de usuario) para evitar exponer información sensible en el control de versiones.
    -   Desde la raíz del proyecto `src/WebAPI`, ejecuta el siguiente comando para inicializar los secretos de usuario:

        ```bash
        dotnet user-secrets init
        ```

    -   Ahora, agrega la cadena de conexión y la configuración de JWT a los secretos. Reemplaza los valores de `USER`, `PASSWORD`, `YOUR_JWT_SECRET_KEY`, etc., con tus propias credenciales.

        ```bash
        dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=delivery_db_dev;Username=USER;Password=PASSWORD;"
        dotnet user-secrets set "Jwt:Key" "YOUR_SUPER_SECRET_AND_LONG_JWT_KEY_HERE"
        dotnet user-secrets set "Jwt:Issuer" "DeliveryAppIssuer"
        dotnet user-secrets set "Jwt:Audience" "DeliveryAppAudience"
        ```

---

## 5.4. Aplicar Migraciones

Una vez configurada la cadena de conexión, necesitas aplicar las migraciones de Entity Framework Core para crear el esquema de la base de datos.

-   Abre una terminal en la raíz del proyecto `src/Infrastructure`.
-   Ejecuta el siguiente comando:

    ```bash
    dotnet ef database update --startup-project ../WebAPI/
    ```
    *Nota: Se usa `--startup-project` para que EF Core pueda obtener la configuración de la cadena de conexión desde el proyecto `WebAPI`.* 

---

## 5.5. Ejecutar la Aplicación

-   Puedes ejecutar la aplicación directamente desde tu IDE (Visual Studio/Rider) seleccionando el proyecto `WebAPI` como proyecto de inicio.
-   Alternativamente, desde una terminal en la raíz del proyecto `src/WebAPI`, ejecuta:

    ```bash
    dotnet run
    ```

-   Una vez que la aplicación esté corriendo, puedes acceder a la API. Por defecto, estará disponible en `https://localhost:7225` (el puerto puede variar).
-   La documentación de Swagger estará disponible en `https://localhost:7225/swagger`.

---

## 5.6. Flujo de Trabajo Básico

1.  **Crear una nueva migración:** Si realizas cambios en las entidades del `Domain` o en la configuración de `ApplicationDbContext`, debes crear una nueva migración. Desde `src/Infrastructure`, ejecuta:

    ```bash
    dotnet ef migrations add NombredelaMigracion --startup-project ../WebAPI/
    ```

2.  **Mantener el código limpio:** Asegúrate de seguir las convenciones de estilo y los patrones de arquitectura establecidos.
