﻿CREATE TABLE [dbo].[Users] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserName]  NVARCHAR (MAX) NOT NULL,
    [Password]  NVARCHAR (MAX) NOT NULL,
    [Role]      INT            DEFAULT ((0)) NOT NULL,
    [UserEmail] NVARCHAR (MAX) NULL,
    [Address] NVARCHAR(MAX) NULL  , 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

