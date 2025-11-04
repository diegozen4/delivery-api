# Wiki del Proyecto: Delivery API

Bienvenido a la documentación centralizada de `delivery-api`. Esta wiki sirve como la fuente principal de verdad para la arquitectura, las reglas de negocio, y las guías técnicas del proyecto.

## Visión General

El proyecto es una plataforma de delivery local diseñada para funcionar como un marketplace de 3 vías, conectando a los siguientes actores:

1.  **Clientes:** Usuarios finales que solicitan productos.
2.  **Negocios (Commerces):** Tiendas y restaurantes que ofertan sus productos.
3.  **Repartidores (Delivery):** Encargados de realizar la entrega de los pedidos.

El objetivo es crear una solución robusta, escalable y fácil de mantener, siguiendo los principios de Clean Architecture con .NET y PostgreSQL.

## Secciones de la Wiki

A continuación se detallan las principales secciones de esta documentación:

*   **[1. Arquitectura de la Solución](1_Arquitectura.md):** Explica en detalle la implementación de Clean Architecture, la responsabilidad de cada capa (`Domain`, `Application`, `Infrastructure`, `WebAPI`) y el flujo de una petición a través del sistema.

*   **[2. Reglas de Negocio](2_Reglas_Negocio.md):** Describe la lógica fundamental que gobierna el comportamiento de la aplicación, incluyendo la gestión de roles, el ciclo de vida de los pedidos y las interacciones entre las entidades principales.

*   **[3. Esquema de la Base de Datos](3_Esquema_BD.md):** Detalla el diseño de la base de datos PostgreSQL, incluyendo las tablas principales, sus columnas y las relaciones entre ellas.

*   **[4. Endpoints de la API](4_Endpoints_API.md):** Proporciona una guía de los endpoints RESTful expuestos por la `WebAPI`, sus parámetros y las respuestas esperadas.

*   **[5. Guía de Instalación y Desarrollo](5_Guia_Desarrollo.md):** Contiene las instrucciones paso a paso para que un nuevo desarrollador pueda clonar, configurar y ejecutar el proyecto en su entorno local.

*   **[6. Estrategia de Despliegue](6_Estrategia_Despliegue.md):** Describe el proceso de CI/CD (Integración y Despliegue Continuo) y la infraestructura de nube en Azure sobre la que corre la aplicación.
