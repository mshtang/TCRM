﻿CREATE TABLE [dbo].[TaxCategory]
(
	[Id] INT NOT NULL PRIMARY KEY,	
	[TaxRate] MONEY NOT NULL UNIQUE, 
	[TaxLevel] NVARCHAR(20) NOT NULL
);