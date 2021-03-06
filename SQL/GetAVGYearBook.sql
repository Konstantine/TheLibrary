USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[GetAVGYearBook]    Script Date: 10/05/2013 15:45:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetAVGYearBook]
@year int
as
select book.Name from Book inner join BookIssuance  on Book.BookId = BookIssuance.BookId
where YEAR(IssuanceDate) = @year
group by Book.Name having  COUNT(BookIssuanceId) >= (select COUNT(BookIssuanceId) 
														from BookIssuance where YEAR(IssuanceDate) = @year)/COUNT(Book.BookId)
