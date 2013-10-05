USE [TheLibrary]
GO
/****** Object:  StoredProcedure [dbo].[GetAllNamesakes]    Script Date: 10/05/2013 15:31:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetAllNamesakes]
as
select Name, Lastname from Author where Lastname in(select Lastname from Author group by Lastname having COUNT(Lastname)>1);
