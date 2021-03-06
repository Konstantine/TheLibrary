USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[GetTopBadUsers]    Script Date: 10/05/2013 15:46:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetTopBadUsers]
@year int
as
if (year(GETDATE()) = @year)
select Top(10) Users.Name from Users inner join BookIssuance on Users.UserId = BookIssuance.UserId
where RequiredReturnDate < GETDATE() and (RealReturnDate is null or GETDATE()<RealReturnDate)
group by Users.Name order by count(BookIssuanceId) desc
else
select Top(10) Users.Name from Users inner join BookIssuance on Users.UserId = BookIssuance.UserId
where RequiredReturnDate < CAST(cast(@year as varchar(10))+'-12-31' as date) and (RealReturnDate is null or CAST(cast(@year as varchar(10))+'-12-31' as date)<RealReturnDate)
group by Users.Name order by count(BookIssuanceId) desc
