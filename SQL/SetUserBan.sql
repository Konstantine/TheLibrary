USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[SetUserBan]    Script Date: 10/05/2013 15:46:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[SetUserBan]
@id int
as
update Users
set IsBanned = 1
where UserId = @id
