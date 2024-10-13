USE [ProductDB]
GO

/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 10/13/2024 8:58:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[UpdateProduct]
	@Id int,
	@Name nvarchar(100),
	@Price decimal(18,2)
as
begin
	Update Products set Name = @Name, Price = @Price Where Id = @Id;
end
GO


