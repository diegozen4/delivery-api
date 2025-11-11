---
Fecha de Creación: martes, 11 de noviembre de 2025
Razón de Creación: Este documento fue generado para proporcionar un análisis de alto nivel del proyecto de API de delivery, su misión, visión y un listado categorizado de funcionalidades faltantes, sirviendo como una hoja de ruta para el desarrollo futuro.
---

# Análisis de la API de Delivery: Misión, Visión y Funcionalidades Faltantes

## Misión y Visión (Inferidas)

*   **Misión:** Ser el motor backend (API) para un marketplace de delivery de tres vías (Clientes, Negocios, Repartidores). La API debe gestionar de forma fiable y eficiente todo el ciclo de vida de un pedido, desde su creación y pago hasta su despacho, entrega y finalización.
*   **Visión:** Convertirse en una plataforma de delivery robusta, escalable y flexible. Aunque es un monolito, su arquitectura limpia (`Clean Architecture`) le permite crecer y adaptarse, con el objetivo de incorporar funcionalidades avanzadas como seguimiento en tiempo real, sistemas de pago, y analíticas, consolidándose como la solución central para el ecosistema de delivery.

## Listado de Funcionalidades Faltantes (Categorizadas)

Basado en la misión y visión, y en el estado actual del proyecto, estas son las funcionalidades clave que aún no están implementadas o están incompletas:

#### A. Flujo Central de Pedidos y Entrega

1.  **Aceptar Pedido de Mercado Abierto:**
    *   Endpoint para que un repartidor acepte un pedido con `DispatchMode.Market`.
2.  **Actualizaciones de Estado de Entrega:**
    *   Endpoints para que el repartidor actualice el estado del pedido (ej. `Recogido`, `EnCamino`, `Entregado`, `Entrega Fallida`).
3.  **Cancelación de Pedidos:**
    *   Lógica y endpoints para que clientes o comercios cancelen pedidos, incluyendo reglas de negocio sobre cuándo se permite la cancelación.
4.  **Historial de Pedidos:**
    *   Endpoints para que clientes, comercios y repartidores consulten sus pedidos pasados con detalles.

#### B. Gestión de Usuarios y Perfiles

5.  **Perfiles de Usuario Detallados:**
    *   Endpoints para que los usuarios (clientes, repartidores, comercios) puedan ver y actualizar su información de perfil (nombre, teléfono, etc.).
6.  **Gestión de Direcciones:**
    *   CRUD completo para las direcciones de los usuarios (`GET`, `POST`, `PUT`, `DELETE /api/users/me/addresses`).
7.  **Proceso de Onboarding/Gestión de Repartidores:**
    *   Flujo formal para que un usuario se postule como repartidor (usando la entidad `DeliveryCandidate`), y para que un administrador apruebe o rechace estas solicitudes.
    *   Gestión de detalles del vehículo del repartidor.
8.  **Gestión de Comercios:**
    *   Endpoints para que los dueños de negocios actualicen la información de su comercio (nombre, dirección, horarios, logo, etc.).

#### C. Finanzas y Pagos

9.  **Integración de Pagos:**
    *   Implementación de la lógica de procesamiento de pagos real (la entidad `Transaction` existe, pero no hay integración con pasarelas de pago como Stripe, PayPal, etc.).
10. **Pagos a Repartidores y Comercios:**
    *   Sistema para calcular y gestionar los pagos a repartidores y comercios.
11. **Motor de Precios y Tarifas:**
    *   Un sistema más dinámico para calcular tarifas de entrega (basado en distancia, tiempo, demanda) y comisiones de la plataforma.

#### D. Calificaciones y Reseñas

12. **Sistema de Calificación:**
    *   Endpoints para que los clientes califiquen al comercio y al repartidor después de un pedido.
13. **Review System:**
    *   Endpoints para que los clientes dejen reseñas escritas.
14. **Visualización de Calificaciones:**
    *   Lógica para calcular y mostrar calificaciones promedio para comercios y repartidores.

#### E. Tiempo Real y Notificaciones

15. **Seguimiento de Ubicación en Tiempo Real:**
    *   Mecanismo (probablemente usando WebSockets/SignalR) para que el cliente pueda rastrear la ubicación del repartidor en tiempo real. (Las coordenadas de latitud/longitud en `User` y `Commerce` son un buen punto de partida).
16. **Notificaciones Push:**
    *   Sistema para enviar notificaciones en tiempo real (ej. "Tu pedido está en camino", "Tienes una nueva oferta") a las aplicaciones móviles/web. (La entidad `Notification` existe, pero el mecanismo de envío no).

#### F. Búsqueda y Descubrimiento

17. **Búsqueda y Filtrado de Comercios/Productos:**
    *   Capacidades avanzadas de búsqueda y filtrado para clientes (ej. buscar por tipo de cocina, nombre de producto, filtrar por calificación, distancia).
18. **Filtrado Basado en Geolocalización:**
    *   Utilizar los datos de latitud/longitud para mostrar a los usuarios comercios cercanos o que puedan entregar en su ubicación.

#### G. Administración

19. **Backend del Panel de Administración:**
    *   Un conjunto completo de endpoints para que los administradores gestionen usuarios, roles, comercios, pedidos y configuraciones de la plataforma.