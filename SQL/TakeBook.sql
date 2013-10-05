USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[TakeBook]    Script Date: 10/05/2013 15:47:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[TakeBook]
@bookid int,
@userId int
as
insert into BookIssuance (BookId,UserId,IssuanceDate,RequiredReturnDate)
values (@bookid,@userId,GETDATE(),DATEADD(MONTH,+1,GETDATE()))
