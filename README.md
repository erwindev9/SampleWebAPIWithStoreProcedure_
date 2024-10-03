# Basic CRUD Products API

## Description

This project is a basic example of a Web API for managing products with CRUD (Create, Read, Update, Delete) operations. The product model includes three properties: `Id`, `Name`, and `Price`. This example specifically uses Stored Procedures to handle database operations, demonstrating that CRUD transactions can be performed without relying on Entity Framework (EF).

## Features

- **Create**: Add a new product to the database.
- **Read**: Retrieve all products or a specific product by ID.
- **Update**: Update the details of an existing product.
- **Delete**: Remove a product from the database.

## Product Model

```csharp
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
}

## Technologies Used

- **ASP.NET Core**
- **SQL Server**
- **Stored Procedures**

## Create Stored Procedures

Execute the following SQL script in SQL Server to create the required stored procedures:

```sql
CREATE PROCEDURE [dbo].[InsertProduct]
    @Name NVARCHAR(100),
    @Price DECIMAL(18,2)
AS 
BEGIN
    INSERT INTO Products (Name, Price) VALUES (@Name, @Price);
END
GO

CREATE PROCEDURE [dbo].[UpdateProduct]
    @Id INT,
    @Name NVARCHAR(100),
    @Price DECIMAL(18,2)
AS
BEGIN
    UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id;
END
GO

CREATE PROCEDURE [dbo].[GetProducts]
AS 
BEGIN
    SELECT * FROM Products;
END
GO

CREATE PROCEDURE [dbo].[GetProductById]
    @Id INT
AS 
BEGIN
    SELECT * FROM Products WHERE Id = @Id;
END
GO

CREATE PROCEDURE [dbo].[DeleteProduct]
    @Id INT
AS 
BEGIN 
    DELETE FROM Products WHERE Id = @Id;
END
GO


## Understanding CRUD Without Entity Framework

While Entity Framework (EF) is a popular choice for handling data access in .NET applications, it is not the only way to perform CRUD operations. 
This project demonstrates that you can interact with the database directly through Stored Procedures, offering greater control and potentially improved performance in certain scenarios. Using Stored Procedures can also enhance security by preventing SQL injection attacks.
