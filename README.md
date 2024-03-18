Proyecto de Booking Agency - Arquitectura DDD con Patrón Repository
Este repositorio contiene el código fuente de un proyecto de una agencia de reservas (Booking Agency), desarrollado utilizando la arquitectura Domain-Driven Design (DDD) junto con el patrón de diseño Repository.

Arquitectura DDD (Domain-Driven Design)
Domain-Driven Design es una metodología de diseño de software que se centra en la comprensión profunda del dominio de negocio para guiar el diseño del software. En este proyecto, hemos adoptado la arquitectura DDD para:

Mejorar la comprensión del dominio: Utilizando conceptos como Entidades, Valor de Objetos, Servicios de Dominio, etc., hemos modelado el dominio de la agencia de reservas de manera clara y precisa.

Fomentar la colaboración entre equipos: La arquitectura DDD promueve un lenguaje común entre los equipos de desarrollo y los expertos del dominio, lo que facilita la comunicación y colaboración durante todo el ciclo de vida del proyecto.

Facilitar la evolución del software: Al modelar el dominio de manera sólida y separar las preocupaciones relacionadas con la infraestructura, podemos realizar cambios en el software de manera más ágil y segura, sin comprometer la integridad del dominio.

Patrón de Diseño Repository
El patrón de diseño Repository se utiliza para abstraer el acceso a los datos, proporcionando una capa intermedia entre el código de la aplicación y la capa de persistencia de datos. En este proyecto, hemos utilizado el patrón Repository por las siguientes razones:

Separación de preocupaciones: Al utilizar un repositorio, podemos separar la lógica de acceso a los datos de la lógica de negocio de la aplicación, lo que facilita la prueba unitaria y el mantenimiento del código.

Flexibilidad en el almacenamiento de datos: El repositorio proporciona una interfaz única para acceder a los datos, lo que nos permite cambiar fácilmente el proveedor de la base de datos o el mecanismo de almacenamiento sin afectar al resto de la aplicación.

Mejora del rendimiento: El repositorio puede implementar estrategias de almacenamiento en caché y optimización de consultas para mejorar el rendimiento de acceso a los datos.

Para poder iniciar , Es necesario iniciar Sesion para obtener un token y este token enviarlo por lo Headers para poder usar los demas servicios
