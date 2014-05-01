CREATE TABLE [dbo].[MyCart] (
    [Id]          INT            IDENTITY(1,1) NOT NULL,
    [UserName]    NVARCHAR (50)  NULL,
    [ProductID]   INT             NULL,
    [Quantity]    INT            DEFAULT ((1)) NULL,
    [Price]       DECIMAL (18)   DEFAULT ((-1)) NULL,
    [ProductName] NVARCHAR (MAX) NULL, 
    CONSTRAINT [PK_MyCart] PRIMARY KEY ([Id])
);

