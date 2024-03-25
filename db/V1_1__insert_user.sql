USE [User App]
GO

INSERT INTO [dbo].[User]
           ([Id]
           ,[LogOnName]
           ,[PasswordHash]
           ,[IsEnabled]
           ,[ExpiryDate]
           ,[PasswordChangeDate]
           ,[FirstName]
           ,[LastName])
     VALUES
		(NEWID(), N'example1', N'passwordhash1', 1, '2024-03-18 12:00:00', '2024-03-18 12:00:00', N'John', N'Doe'),
		(NEWID(), N'example2', N'passwordhash2', 0, '2024-03-18 12:00:00', '2024-03-18 12:00:00', N'Jane', N'Smith'),
		(NEWID(), N'example3', N'passwordhash3', 1, '2024-03-18 12:00:00', '2024-03-18 12:00:00', N'Alice', N'Johnson');
GO


