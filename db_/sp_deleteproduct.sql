USE [ProductDB]
GO

/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 10/13/2024 8:57:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[DeleteProduct]
	@Id int
as 
begin 
	delete Products where Id =@Id;
end
GO


