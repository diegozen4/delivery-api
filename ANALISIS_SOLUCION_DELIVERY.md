# Análisis y Propuesta de Solución para `delivery-api`

Este documento resume el análisis de la solución `delivery-api` y propone un modelo para la gestión de grupos de repartidores, integrando las necesidades de agilidad, escalabilidad y flexibilidad para todos los actores.

---

## 1. Análisis de la Solución `delivery-api`

La solución `delivery-api` está implementada siguiendo los principios de **Clean Architecture** en .NET Core 8, lo que garantiza una clara separación de responsabilidades, alta mantenibilidad y facilidad para las pruebas.

### 1.1. Estructura de Proyectos

*   **`Domain`**: Contiene las entidades de negocio puras (`User`, `Commerce`, `Product`, `Order`, `DeliveryUser`, `DeliveryGroup`, etc.) y las reglas de negocio fundamentales, sin dependencias externas.
*   **`Application`**: Define los casos de uso y las interfaces (`IAuthService`, `ICategoryRepository`, `IJwtTokenGenerator`, etc.). Orquesta la lógica de negocio y depende de `Domain`.
*   **`Infrastructure`**: Implementa las interfaces de `Application`. Maneja la persistencia de datos (Entity Framework Core con PostgreSQL), migraciones, autenticación (ASP.NET Core Identity, JWT) y servicios externos. Depende de `Application` y `Domain`.
*   **`WebAPI`**: Es el punto de entrada de la aplicación, exponiendo la funcionalidad a través de controladores REST. Depende de todos los demás proyectos.
*   **`Contracts`**: Contiene los Data Transfer Objects (DTOs) para las solicitudes y respuestas de la API.

### 1.2. Tecnologías Clave

*   **Backend**: .NET Core 8.
*   **Base de Datos**: PostgreSQL con Entity Framework Core.
*   **Autenticación**: ASP.NET Core Identity para gestión de usuarios y roles, y JSON Web Tokens (JWT) para la autenticación de la API.
*   **Inyección de Dependencias**: Configurada en `Program.cs` para todos los servicios y repositorios.

### 1.3. Flujo de Autenticación

*   **`AuthController` (WebAPI)**: Expone endpoints para `register`, `login` y `change-password`.
*   **`IAuthService` (Application)**: Define el contrato para la lógica de autenticación.
*   **`AuthService` (Infrastructure)**: Implementa `IAuthService` utilizando `UserManager<User>` y `RoleManager<Role>` de ASP.NET Core Identity. Asigna el rol "Cliente" por defecto al registrarse y utiliza `IJwtTokenGenerator` para crear tokens.
*   **`IJwtTokenGenerator` (Application)**: Define el contrato para la generación de JWT.
*   **`JwtTokenGenerator` (Infrastructure)**: Genera tokens JWT con claims de usuario (ID, email, roles) y configuración de expiración.

### 1.4. Base de Datos y Entidades

*   **`ApplicationDbContext` (Infrastructure)**: Hereda de `IdentityDbContext<User, Role, Guid>`. Define `DbSet` para todas las entidades principales (Commerce, Category, Product, Order, DeliveryUser, DeliveryGroup, etc.).
*   **Configuración de Modelo**: Utiliza `OnModelCreating` para mapear entidades a esquemas específicos (ej. `auth`, `commerce`, `order`), configurar relaciones (uno-a-muchos, muchos-a-muchos) y convertir enumeraciones a strings para almacenamiento.
*   **Entidad `User` (Domain)**: Extiende `IdentityUser<Guid>`, incluyendo propiedades personalizadas como `FirebaseId` y una colección de `RefreshTokens`.

### 1.5. Patrón Repositorio

*   Implementado con interfaces en la capa `Application` (ej. `ICategoryRepository`) y sus implementaciones concretas en la capa `Infrastructure` (ej. `CategoryRepository`), utilizando Entity Framework Core para el acceso a datos.

---

## 2. Modelo Propuesto para la Gestión de Grupos de Repartidores

El modelo se basa en la descentralización de la asignación de pedidos a los `DeliveryGroup`s, ofreciendo flexibilidad a los `Commerce` y a los `DeliveryUser`s, y promoviendo la agilidad y escalabilidad.

### 2.0. Impacto en el Modelo de Marketplace de 3 Vías

La introducción de los `DeliveryGroup`s y el rol `DeliveryGroupAdmin` **no modifica fundamentalmente el modelo de marketplace de 3 vías** (Cliente -> Comercio -> Delivery), sino que lo **refina y enriquece la pata de "Delivery"** de ese modelo.

*   **La Interacción Central Permanece:** El flujo fundamental de un marketplace de 3 vías sigue siendo el mismo: el Cliente solicita un servicio/producto, el Comercio lo provee, y el Delivery lo transporta.
*   **`DeliveryGroup` como Capa Organizativa:** El `DeliveryGroup` actúa como una abstracción o una capa organizativa *dentro* del componente "Delivery". Permite que el Comercio interactúe con una entidad organizada (el grupo) en lugar de directamente con repartidores individuales, y que el grupo gestione a sus `DeliveryUser`s internos.
*   **`DeliveryGroupAdmin` como Rol de Gestión Interna:** El `DeliveryGroupAdmin` es un rol especializado cuya responsabilidad es gestionar los recursos (los `DeliveryUser`s) y las operaciones *dentro* de un `DeliveryGroup`. No es un nuevo actor primario en el marketplace, sino un rol de gestión de los recursos de delivery.

En esencia, esta adición hace que el componente "Delivery" del modelo de 3 vías sea más robusto, flexible y capaz de manejar diversos escenarios operativos, desde repartidores individuales hasta empresas de delivery completas.

### 2.1. Roles Clave y sus Responsabilidades

*   **`Administrador` (Plataforma):**
    *   Crea y gestiona `DeliveryGroup`s en el sistema.
    *   Asigna el rol `DeliveryGroupAdmin` a usuarios específicos.
    *   Supervisa las asociaciones entre `Commerces` y `DeliveryGroup`s.
    *   Monitorea el rendimiento general de los grupos.
*   **`DeliveryGroupAdmin` (Administrador de Grupo):**
    *   Gestiona un `DeliveryGroup` específico.
    *   Añade y elimina `DeliveryUser`s de su grupo.
    *   Configura el `ModoAsignacion` de su grupo (Manual, Broadcast, etc.).
    *   Monitorea el rendimiento de los repartidores de su grupo.
*   **`Commerce` (Negocio):**
    *   Se afilia a uno o más `DeliveryGroup`s (selección múltiple).
    *   Decide si sus pedidos se muestran solo a grupos afiliados o también a "repartidores libres".
    *   Despacha pedidos a los grupos afiliados o al "mercado libre".
*   **`DeliveryUser` (Repartidor):**
    *   Puede ser `Cliente` y `Repartidor` simultáneamente.
    *   Elige su "Modo Repartidor" en la app.
    *   Se marca como "Disponible" para un `DeliveryGroup` específico al que pertenece, o como "Disponible" para pedidos libres.
    *   Acepta pedidos según el modo de asignación del grupo o del mercado libre.

### 2.2. Flujo de Operación del Repartidor

1.  **Inicio de Sesión:** El usuario inicia sesión en la app (`delivery-app`).
2.  **Selección de Modo:** Elige "Modo Repartidor".
3.  **Selección de Disponibilidad:**
    *   **Si pertenece a un grupo:** Selecciona el `DeliveryGroup` para el cual desea estar "Disponible".
    *   **Si no pertenece a ningún grupo:** Puede marcarse como "Disponible para pedidos libres".
4.  **"En Línea":** El repartidor se marca como "Disponible" en el modo elegido.
5.  **Recepción de Pedidos:**
    *   **Pedidos de Grupo:** Si está disponible para un grupo, verá los pedidos despachados a ese grupo (ya sea por `Broadcast` o asignación directa).
    *   **Pedidos Libres:** Si está disponible para pedidos libres, verá los pedidos que los `Commerces` han decidido mostrar a "repartidores libres" y que no han sido tomados por ningún grupo.
6.  **Aceptar/Rechazar Pedido:** El repartidor acepta o rechaza el pedido dentro de un límite de tiempo.
7.  **Proceso de Entrega:** Realiza la entrega, actualizando el estado en la app.
8.  **"Fuera de Línea":** El repartidor se marca como "No Disponible" cuando termina.

### 2.3. Opciones de Despacho para el `Commerce`

El `Commerce` tendrá flexibilidad para configurar cómo se gestionan sus pedidos de delivery:

1.  **Afiliación a `DeliveryGroup`s (Multi-selección):**
    *   Un `Commerce` puede afiliarse a uno o más `DeliveryGroup`s (ej. su propio grupo interno, más dos empresas de delivery externas).
    *   Esto se gestionaría a través de una relación muchos-a-muchos entre `Commerce` y `DeliveryGroup`.

2.  **Prioridad de Despacho:**
    *   El `Commerce` podría configurar una prioridad entre sus grupos afiliados (ej. primero mi grupo interno, luego Grupo A, luego Grupo B).
    *   O bien, el sistema podría intentar despachar a todos los grupos afiliados simultáneamente (si su `ModoAsignacion` es `Broadcast`).

3.  **Opción "Mostrar a Repartidores Libres":**
    *   El `Commerce` puede activar una opción para que, si un pedido no es tomado por sus grupos afiliados en un tiempo determinado, o si simplemente desea ampliar la visibilidad, el pedido se muestre también a los `DeliveryUser`s que están "Disponibles para pedidos libres" (es decir, que no pertenecen a ningún grupo o no están activos para un grupo en ese momento).

### 2.4. Agilidad y Escalabilidad

*   **Flexibilidad del Repartidor:** El repartidor tiene control sobre su disponibilidad y a qué tipo de pedidos responde, maximizando sus oportunidades.
*   **Control del Comercio:** El comercio decide sus afiliaciones y la visibilidad de sus pedidos, adaptándose a sus necesidades operativas.
*   **Descentralización:** La asignación interna de pedidos recae en el `DeliveryGroup`, reduciendo la carga del sistema central y permitiendo que cada grupo optimice sus operaciones.
*   **"Mercado Abierto" (Pedidos Libres):** La opción de "repartidores libres" crea un mercado dinámico que asegura que los pedidos siempre encuentren un repartidor, incluso si los grupos afiliados están ocupados.
*   **Tecnología Robusta:** La base de .NET Core 8, EF Core y Clean Architecture proporciona una plataforma sólida para implementar estas funcionalidades de manera eficiente y escalable.

---

Este modelo busca equilibrar la estructura y el control con la flexibilidad y las oportunidades, crucial para un servicio de delivery ágil y escalable.