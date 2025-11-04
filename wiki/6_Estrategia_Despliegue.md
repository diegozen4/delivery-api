# 6. Estrategia de Despliegue

Esta sección describe la infraestructura de nube y el proceso de CI/CD (Integración y Despliegue Continuo) para la `delivery-api`.

---

## 6.1. Infraestructura en la Nube (Azure)

La aplicación está diseñada para ser desplegada en **Microsoft Azure**, utilizando los siguientes servicios:

-   **Azure App Service:**
    -   **Propósito:** Hostea la aplicación `WebAPI`.
    -   **Plataforma:** Se configura un App Service Plan sobre Linux para ejecutar el contenedor de .NET.
    -   **Escalabilidad:** Permite escalar verticalmente (aumentando la potencia de la instancia) y horizontalmente (aumentando el número de instancias) según la demanda.

-   **Azure Database for PostgreSQL:**
    -   **Propósito:** Proporciona una instancia gestionada de PostgreSQL en la nube.
    -   **Ventajas:** Azure se encarga del mantenimiento, las copias de seguridad, la alta disponibilidad y la seguridad de la base de datos.

-   **Azure Key Vault:**
    -   **Propósito:** Almacena de forma segura todos los secretos de la aplicación, como la cadena de conexión a la base de datos, la clave secreta de JWT y otras claves de API.
    -   **Integración:** La `WebAPI` se configura con Managed Identity para acceder al Key Vault sin necesidad de almacenar ninguna credencial en la configuración de la aplicación.

-   **Azure Blob Storage:**
    -   **Propósito:** Almacena archivos estáticos, principalmente las imágenes de los productos y los logos de los comercios.
    -   **Acceso:** Se configura un contenedor con acceso público de lectura para que las imágenes puedan ser consumidas por las aplicaciones cliente.

-   **Azure Application Insights:**
    -   **Propósito:** Es la herramienta de APM (Application Performance Management) para monitorear la salud de la API, registrar telemetría, detectar anomalías y analizar el rendimiento y los errores.

---

## 6.2. Pipeline de CI/CD con GitHub Actions

El proceso de integración y despliegue continuo se automatiza mediante **GitHub Actions**. El workflow se define en un archivo YAML ubicado en `.github/workflows/`.

### Workflow de Integración Continua (CI)

Este workflow se dispara en cada `push` a las ramas `main` y `develop`, y en cada `pull request` dirigido a `main`.

1.  **Checkout:** Descarga el código fuente del repositorio.
2.  **Setup .NET:** Instala la versión correcta del SDK de .NET.
3.  **Restore Dependencies:** Restaura los paquetes NuGet del proyecto.
4.  **Build:** Compila la solución en modo `Release`.
5.  **Test:** Ejecuta todas las pruebas unitarias y de integración. Si alguna prueba falla, el pipeline se detiene y reporta el error.
6.  **SonarCloud Scan:** (Opcional pero recomendado) Ejecuta un análisis estático del código para detectar bugs, vulnerabilidades y code smells.

### Workflow de Despliegue Continuo (CD)

Este workflow se dispara automáticamente después de que el pipeline de CI se completa con éxito en la rama `main`.

1.  **Build & Publish:**
    -   Compila la aplicación y la publica como un artefacto de compilación, optimizado para el despliegue.

2.  **Login to Azure:**
    -   Se autentica en Azure utilizando un Service Principal previamente configurado y almacenado como un secreto en GitHub.

3.  **Deploy to Azure App Service:**
    -   Toma el artefacto de compilación y lo despliega en la instancia de Azure App Service correspondiente (ej. `delivery-api-staging` o `delivery-api-prod`).

4.  **Run Database Migrations:**
    -   **Importante:** Después del despliegue de la aplicación, se debe ejecutar un paso para aplicar las migraciones de Entity Framework Core a la base de datos de Azure. Esto se puede hacer de varias maneras:
        -   Usando `dotnet ef database update` en un script dentro del pipeline.
        -   Como una tarea de liberación (Release Task) en Azure DevOps si se usara esa herramienta.
        -   La estrategia preferida es ejecutar las migraciones como parte del arranque de la aplicación, pero esto debe hacerse con cuidado para evitar problemas en entornos con múltiples instancias.
