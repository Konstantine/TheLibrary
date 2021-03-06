USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[GetTopYearAuthor]    Script Date: 10/05/2013 15:46:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetTopYearAuthor]
@year int
as
select Top 1 Author.Name, Author.Lastname from Author inner join Book on Author.AuthorId = book.AuthorId inner join BookIssuance on Book.BookId = BookIssuance.BookId
where year(BookIssuance.IssuanceDate) = @year
group by Author.Name, Author.Lastname having COUNT(BookIssuanceId)>0 order by Count(BookIssuanceId) desc
