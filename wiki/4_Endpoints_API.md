# 4. Endpoints de la API

La `WebAPI` expone una interfaz RESTful para interactuar con los recursos de la aplicación. La documentación completa y interactiva de los endpoints está disponible a través de **Swagger**, que se puede acceder en la ruta `/swagger` cuando la aplicación se está ejecutando en el entorno de desarrollo.

Esta guía proporciona un resumen de los principales recursos y endpoints disponibles.

---

## 4.1. Autenticación (`/api/auth`)

Gestiona el registro, inicio de sesión y gestión de cuentas de usuario.

-   **`POST /api/auth/register`**
    -   **Descripción:** Registra un nuevo usuario en el sistema con el rol de `Cliente` por defecto.
    -   **Request Body:** `RegisterRequest` (Email, Password).
    -   **Respuesta:** `AuthResponse` (Token, UserDetails).

-   **`POST /api/auth/login`**
    -   **Descripción:** Autentica a un usuario y devuelve un token JWT.
    -   **Request Body:** `LoginRequest` (Email, Password).
    -   **Respuesta:** `AuthResponse` (Token, UserDetails).

-   **`POST /api/auth/change-password`**
    -   **Descripción:** Permite a un usuario autenticado cambiar su contraseña.
    -   **Autorización:** Requiere token JWT.
    -   **Request Body:** `ChangePasswordRequest` (OldPassword, NewPassword).

---

## 4.2. Comercios (`/api/commerces`)

Endpoints para interactuar con los negocios o tiendas.

-   **`GET /api/commerces`**
    -   **Descripción:** Obtiene una lista paginada de todos los comercios disponibles.
    -   **Autorización:** Pública.

-   **`GET /api/commerces/{id}`**
    -   **Descripción:** Obtiene los detalles de un comercio específico, incluyendo sus productos.
    -   **Autorización:** Pública.

-   **`POST /api/commerces`**
    -   **Descripción:** Crea un nuevo comercio. 
    -   **Autorización:** Rol `Admin`.

---

## 4.3. Productos (`/api/products`)

Endpoints para la gestión de productos, generalmente accesibles en el contexto de un comercio.

-   **`GET /api/products/{id}`**
    -   **Descripción:** Obtiene los detalles de un producto específico.
    -   **Autorización:** Pública.

-   **`POST /api/products`**
    -   **Descripción:** Crea un nuevo producto para el comercio del usuario autenticado.
    -   **Autorización:** Rol `Negocio`.
    -   **Request Body:** `CreateProductRequest` (Name, Description, Price, CommerceId).

-   **`PUT /api/products/{id}`**
    -   **Descripción:** Actualiza un producto existente.
    -   **Autorización:** Rol `Negocio` (debe ser dueño del producto).

-   **`DELETE /api/products/{id}`**
    -   **Descripción:** Elimina un producto.
    -   **Autorización:** Rol `Negocio` (debe ser dueño del producto).

---

## 4.4. Pedidos (`/api/orders`)

Endpoints para el ciclo de vida de los pedidos.

-   **`POST /api/orders`**
    -   **Descripción:** Crea un nuevo pedido para un cliente.
    -   **Autorización:** Rol `Cliente`.
    -   **Request Body:** `CreateOrderRequest` (CommerceId, List<OrderItems>).

-   **`GET /api/orders/my-history`**
    -   **Descripción:** Obtiene el historial de pedidos del cliente autenticado.
    -   **Autorización:** Rol `Cliente`.

-   **`GET /api/orders/{id}`**
    -   **Descripción:** Obtiene los detalles de un pedido específico.
    -   **Autorización:** `Cliente` (dueño), `Negocio` (receptor), `Repartidor` (asignado) o `Admin`.

-   **`PUT /api/orders/{id}/status`**
    -   **Descripción:** Actualiza el estado de un pedido.
    -   **Autorización:** `Negocio` o `Repartidor`.
    -   **Request Body:** `UpdateOrderStatusRequest` (NewStatus).

---

## 4.5. Categorías (`/api/categories`)

Endpoints para la gestión de categorías globales.

-   **`GET /api/categories`**
    -   **Descripción:** Obtiene una lista de todas las categorías.
    -   **Autorización:** Pública.

-   **`POST /api/categories`**
    -   **Descripción:** Crea una nueva categoría.
    -   **Autorización:** Rol `Admin`.
