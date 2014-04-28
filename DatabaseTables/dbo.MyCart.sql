CREATE TABLE [dbo].[MyCart] (
    [Id]          INT            NOT NULL DEFAULT AUTO_INCREMENT,
    [UserName]    NVARCHAR (50)  NULL,
    [ProductID]   INT            NOT NULL,
    [Quantity]    INT            DEFAULT ((1)) NOT NULL,
    [Price]       DECIMAL (18)   DEFAULT ((-1)) NOT NULL,
    [ProductName] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

