USE [ProductDB]
GO

/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 10/13/2024 8:55:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[GetProducts]
as 
begin
	select * from Products;
end
GO


