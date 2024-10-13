USE [ProductDB]
GO

/****** Object:  StoredProcedure [dbo].[InsertProduct]    Script Date: 10/13/2024 8:55:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[InsertProduct]
	@Name nvarchar(100),
	@Price decimal(18,2)
as 
begin
	insert into Products (Name, Price) values (@Name,@Price);
end
GO


