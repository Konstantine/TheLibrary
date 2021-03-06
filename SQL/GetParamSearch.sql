USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[GetParamSearch]    Script Date: 10/05/2013 15:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetParamSearch]
@name nvarchar(50),
@year nvarchar(10)
as
select Book.Name from Book
where Name like @name and year(PublishDate) = cast(@year as int) or Name like @name or year(PublishDate) = cast(@year as int)
