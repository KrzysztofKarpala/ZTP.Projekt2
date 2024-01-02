# Products API

RESTful API written in ASP.NET Core using MediatR for managing products. This API is integrated with other projects, including ZTP_MVC, ZTPMobileApplication, and ZTP.Specs. The implementation follows the principles of Domain-Driven Design (DDD).

## Table of Contents

- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [Features](#features)
- [Classes and Interfaces](#classes-and-interfaces)
- [Integration with Other Projects](#integration-with-other-projects)
- [Domain-Driven Design (DDD)](#domain-driven-design-ddd)
- [Installation and Running Instructions](#installation-and-running-instructions)

## Technologies

- ASP.NET Core
- MediatR
- Swashbuckle
- MongoDB
- Docker Compose

## Project Structure

The project is divided into several folders, each responsible for different functionalities. Here's a brief overview of the directory structure:

- **Controllers**: Contains API controllers handling HTTP requests.
- **Core**: Houses classes and interfaces related to business logic, such as entities, repositories, and more.
- **Application**: Includes classes related to handling MediatR commands and queries.
- **Repositories**: Holds interfaces for repositories responsible for communication with the database or other data sources.

## Features

### ProductsController

Controller handling operations on products.

- `GetAllProducts`: Returns a list of all products.
- `GetProductByProductId`: Returns a product based on the identifier.
- `AddProduct`: Adds a new product.
- `UpdateProduct`: Updates an existing product.
- `DeleteProduct`: Deletes a product based on the identifier.
- `AddProductQuantity`: Adds quantity to an existing product.
- `SubtractProductQuantity`: Subtracts quantity from an existing product.

### ProductHistory

Class storing the history of product changes.

- `Create`: Creates a new `ProductHistory` object.

### IProductRepository

Interface for the product repository.

- `GetProducts`: Retrieves a list of all products.
- `GetProductByProductId`: Retrieves a product based on the identifier.
- `GetProductByProductName`: Retrieves a product based on the name.
- `AddOrReplaceProduct`: Adds or replaces a product.
- `DeleteProductById`: Deletes a product based on the identifier.

## Integration with Other Projects

This API is connected with the following projects:

- **ZTP_MVC**: [Link to ZTP_MVC Repository](https://github.com/KrzysztofKarpala/ZTP_MVC)
- **ZTPMobileApplication**: [Link to ZTPMobileApplication Repository](https://github.com/KrzysztofKarpala/ZTPMobileApplication)
- **ZTP.Specs**: [Link to ZTP.Specs Repository](https://github.com/KrzysztofKarpala/ZTP.Specs)

## Domain-Driven Design (DDD)

The implementation of this API follows the principles of Domain-Driven Design (DDD), emphasizing a domain-centric approach to software design and development.

## Installation and Running Instructions

1. Clone the repository: `git clone https://github.com/KrzysztofKarpala/ZTP.Projekt2.git`
2. Open the project in Visual Studio or another ASP.NET Core-compatible environment.
3. Run the application: `dotnet run`

Enjoy using the API for managing products!
