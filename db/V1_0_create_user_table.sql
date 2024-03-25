USE [User App]
GO

/****** Object:  Table [dbo].[User]    Script Date: 3/18/2024 7:19:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[LogOnName] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[ExpiryDate] [datetime] NULL,
	[PasswordChangeDate] [datetime] NOT NULL,
	[FirstName] [nchar](255) NOT NULL,
	[LastName] [nchar](255) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

