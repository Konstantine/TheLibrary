USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[GetAuthorsBook]    Script Date: 10/05/2013 15:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetAuthorsBook]
@authorName nvarchar(50),
@authorLast nvarchar(50)
as
select Book.Name as "Название", Author.Name as "Имя", Author.Lastname as "Фамилия" from Author inner join Book on Author.AuthorId = Book.AuthorId 
where Author.Name like @authorName and Author.Lastname like @authorLast;
