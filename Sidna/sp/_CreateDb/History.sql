/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.1000)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [SidnaDB]
GO

/****** Object:  Table [dbo].[History]    Script Date: 7/15/2021 11:21:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[History](
	[ID] [bigint] NOT NULL,
	[Date] [datetime] NULL,
	[Str] [float] NULL,
	[End] [float] NULL,
	[Hig] [float] NULL,
	[Low] [float] NULL,
	[Avg] [float] NULL,
	[Type] [tinyint] NULL,
	[Slope] [tinyint] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[History] ADD  DEFAULT ((0)) FOR [Slope]
GO


