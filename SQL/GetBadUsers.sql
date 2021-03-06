USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[GetBadUsers]    Script Date: 10/05/2013 15:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetBadUsers]
@date date
as
select Users.Name from Users inner join BookIssuance on Users.UserId = BookIssuance.UserId
where RequiredReturnDate < Cast(@date as date) and (RealReturnDate is null or Cast(@date as date)<RealReturnDate)
group by Users.Name
