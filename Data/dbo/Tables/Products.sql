CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductName] VARCHAR(128) NOT NULL,
	[Description] VARCHAR(MAX) NOT NULL, 
	[RetailPrice] MONEY NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [LastUpdated] DATETIME2 NOT NULL DEFAULT getutcdate(),
)
