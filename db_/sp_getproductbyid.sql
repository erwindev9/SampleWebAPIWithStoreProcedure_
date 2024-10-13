USE [ProductDB]
GO

/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 10/13/2024 8:56:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[GetProductById]
	@Id int
as 
begin
	select * from Products where Id = @Id;
end
GO


