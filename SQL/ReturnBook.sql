USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[ReturnBook]    Script Date: 10/05/2013 15:46:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ReturnBook]
@issuanceid int
as
update BookIssuance
set RealReturnDate = GETDATE()
where BookIssuanceId = @issuanceid
