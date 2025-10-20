# Reglas de Negocio

Este documento describe las reglas y la lógica de negocio principales que rigen la aplicación.

## 1. Modelo de Marketplace de 3 Vías

La aplicación funciona como un mercado que conecta a tres tipos de actores:

- **Clientes:** Usuarios que compran productos.
- **Negocios (Commerces):** Tiendas, restaurantes u otros vendedores que ofrecen sus productos en la plataforma.
- **Repartidores (Delivery):** Usuarios que realizan las entregas de los pedidos.

## 2. Gestión de Usuarios y Roles

El sistema de usuarios está construido sobre ASP.NET Core Identity y define los siguientes roles:

- **`Cliente` (Cliente):**
    - Es el rol por defecto para cualquier usuario nuevo.
    - Puede navegar por los negocios, ver productos y realizar pedidos.
    - Puede tener su historial de pedidos.
    - Puede solicitar convertirse en Repartidor.

- **`Negocio` (Business):**
    - Rol asignado a los dueños de los comercios.
    - Un usuario con este rol está asociado a una (o más) entidades `Commerce`.
    - Puede gestionar la información de su `Commerce`.
    - Puede crear, actualizar y eliminar los `Products` que pertenecen a su `Commerce`.
    - Puede ver y gestionar los `Orders` que reciben.

- **`Repartidor` (Delivery):**
    - Un `Cliente` puede solicitar y ser aprobado para este rol.
    - Un usuario puede ser `Cliente` y `Repartidor` al mismo tiempo.
    - Puede ver una lista de pedidos disponibles para entrega.
    - Puede aceptar pedidos para realizar la entrega.
    - Tiene un perfil de repartidor con información adicional (ej: estatus, vehículo, calificación).

- **`Administrador` (Admin):**
    - Rol con los máximos privilegios.
    - Gestiona las categorías globales de la plataforma.
    - Supervisa y puede gestionar todos los `Commerces`, `Users`, y `Orders`.
    - Aprueba las solicitudes de nuevos `Repartidores`.

## 3. Lógica de Entidades Principales

- **`Commerce` (Negocio):**
    - Cada `Commerce` es una entidad única gestionada por uno o más usuarios con el rol `Negocio`.
    - Agrupa sus propios `Products`.

- **`Category` (Categoría):**
    - Las categorías son globales y gestionadas por los `Administradores`.
    - Un `Commerce` puede asociarse con múltiples categorías globales para organizar sus productos (ej: "Pizzas", "Bebidas", "Postres").
    - Esta es una relación de **Muchos-a-Muchos**.

- **`Product` (Producto):**
    - Cada `Product` pertenece a **un único** `Commerce`.
    - Tiene su propio precio, descripción e imagen, definidos por el `Negocio`.
    - Esta es una relación de **Uno-a-Muchos** (un `Commerce` tiene muchos `Products`).

- **`Order` (Pedido):**
    - Un `Cliente` crea un `Order` para un `Commerce` específico.
    - Un `Order` contiene uno o más `Products` a través de la entidad `OrderDetail`.
    - Pasa por varios estados (`OrderStatus`), como `Pendiente`, `EnPreparacion`, `EnCamino`, `Entregado`.
