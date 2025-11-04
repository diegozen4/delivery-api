# 2. Reglas de Negocio

Este documento describe las reglas y la lógica de negocio principales que rigen la aplicación, centradas en el modelo de marketplace de 3 vías y la gestión de sus entidades.

---

## 2.1. Modelo de Marketplace de 3 Vías

La aplicación funciona como un mercado que conecta a tres tipos de actores:

-   **Clientes:** Usuarios que compran productos.
-   **Negocios (Commerces):** Tiendas, restaurantes u otros vendedores que ofrecen sus productos en la plataforma.
-   **Repartidores (Delivery):** Usuarios que realizan las entregas de los pedidos.

---

## 2.2. Gestión de Usuarios y Roles

El sistema de usuarios está construido sobre **ASP.NET Core Identity** y define los siguientes roles, cada uno con responsabilidades específicas:

-   **`Cliente` (Cliente):**
    -   Es el rol por defecto para cualquier usuario nuevo que se registra en la plataforma.
    -   Puede navegar por los negocios, ver productos y realizar pedidos.
    -   Tiene acceso a su historial de pedidos y puede gestionar sus direcciones.
    -   Puede solicitar convertirse en Repartidor a través de la aplicación.

-   **`Negocio` (Business):**
    -   Rol asignado por un `Administrador` a los dueños de los comercios.
    -   Un usuario con este rol está asociado a una o más entidades `Commerce`.
    -   Puede gestionar la información de su `Commerce` (nombre, dirección, horario, etc.).
    -   Puede crear, actualizar, ocultar y eliminar los `Products` que pertenecen a su `Commerce`.
    -   Puede ver y gestionar los `Orders` que reciben, actualizando su estado (ej. de `Pendiente` a `EnPreparacion`).

-   **`Repartidor` (Delivery):**
    -   Un `Cliente` puede solicitar este rol, que debe ser aprobado por un `Administrador`.
    -   Un usuario puede ser `Cliente` y `Repartidor` al mismo tiempo con la misma cuenta.
    -   Puede ver una lista de pedidos disponibles para entrega, según su ubicación y disponibilidad.
    -   Puede aceptar pedidos para realizar la entrega, actualizando el estado del pedido (`EnCamino`, `Entregado`).
    -   Tiene un perfil de repartidor con información adicional (ej: estatus, vehículo, calificación).

-   **`Administrador` (Admin):**
    -   Rol con los máximos privilegios en el sistema.
    -   Gestiona las categorías globales de la plataforma (`Pizzas`, `Bebidas`, etc.).
    -   Supervisa y puede gestionar todos los `Commerces`, `Users`, y `Orders`.
    -   Aprueba las solicitudes de nuevos `Repartidores` y asigna el rol `Negocio`.
    -   Gestiona los `DeliveryGroup` y asigna a sus administradores (`DeliveryGroupAdmin`).

---

## 2.3. Lógica de Entidades Principales

-   **`Commerce` (Negocio):**
    -   Cada `Commerce` es una entidad única gestionada por uno o más usuarios con el rol `Negocio`.
    -   Agrupa sus propios `Products`.
    -   Puede afiliarse a uno o más `DeliveryGroup` para gestionar sus entregas.

-   **`Category` (Categoría):**
    -   Las categorías son globales y gestionadas únicamente por los `Administradores`.
    -   Un `Commerce` puede asociarse con múltiples categorías para que sus productos sean fácilmente descubiertos por los clientes.
    -   Esta es una relación de **Muchos-a-Muchos** entre `Commerce` y `Category`.

-   **`Product` (Producto):**
    -   Cada `Product` pertenece a **un único** `Commerce`.
    -   Tiene su propio precio, descripción, imagen y stock, definidos por el `Negocio`.
    -   Esta es una relación de **Uno-a-Muchos** (un `Commerce` tiene muchos `Products`).

-   **`Order` (Pedido):**
    -   Un `Cliente` crea un `Order` para un `Commerce` específico.
    -   Un `Order` contiene uno o más `Products` a través de la entidad de detalle `OrderDetail`.
    -   Pasa por varios estados (`OrderStatus`), que son actualizados por el `Negocio` y el `Repartidor`:
        1.  `Pendiente`: El cliente ha realizado el pedido.
        2.  `Confirmado`: El negocio ha aceptado el pedido.
        3.  `EnPreparacion`: El negocio está preparando el pedido.
        4.  `ListoParaRecoger`: El pedido está listo para ser recogido por un repartidor.
        5.  `EnCamino`: El repartidor ha recogido el pedido y se dirige al cliente.
        6.  `Entregado`: El repartidor ha entregado el pedido al cliente.
        7.  `Cancelado`: El pedido ha sido cancelado por el cliente o el negocio.

-   **`DeliveryGroup` (Grupo de Repartidores):**
    -   Entidad que agrupa a varios `DeliveryUser`.
    -   Es gestionada por un `DeliveryGroupAdmin`.
    -   Permite a los `Commerce` externalizar o gestionar su propia flota de repartidores de forma organizada.
    -   Puede tener diferentes modos de asignación de pedidos (manual, broadcast, etc.).
