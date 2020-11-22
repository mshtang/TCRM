CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProductName] NVARCHAR(100) NOT NULL, 
	[Description] NVARCHAR(MAX) NOT NULL, 
	[RetailPrice] MONEY NOT NULL,
	[Tax] INT NOT NULL DEFAULT 0,
	[QuantityInStock] INT NOT NULL DEFAULT 1,
	[CreateDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
	CONSTRAINT [FK_Product_ToTaxCategory] FOREIGN KEY (Tax) REFERENCES TaxCategory(Id)
);
