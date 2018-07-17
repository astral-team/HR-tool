
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/17/2018 08:45:09
-- Generated from EDMX file: C:\Users\Александр\Documents\GitHub\HR-tool\httpListener\httpListener\UserDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [UserDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LoginsSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionSet] DROP CONSTRAINT [FK_LoginsSession];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[LoginsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginsSet];
GO
IF OBJECT_ID(N'[dbo].[SessionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SessionSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LoginsSet'
CREATE TABLE [dbo].[LoginsSet] (
    [Id] uniqueidentifier  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [DateOff] nvarchar(max)  NOT NULL,
    [Hash] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SessionSet'
CREATE TABLE [dbo].[SessionSet] (
    [Id] uniqueidentifier  NOT NULL,
    [LoginId] uniqueidentifier  NOT NULL,
    [SessionKey] uniqueidentifier  NOT NULL,
    [ExpTime] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LoginsSet'
ALTER TABLE [dbo].[LoginsSet]
ADD CONSTRAINT [PK_LoginsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SessionSet'
ALTER TABLE [dbo].[SessionSet]
ADD CONSTRAINT [PK_SessionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LoginId] in table 'SessionSet'
ALTER TABLE [dbo].[SessionSet]
ADD CONSTRAINT [FK_LoginsSession]
    FOREIGN KEY ([LoginId])
    REFERENCES [dbo].[LoginsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoginsSession'
CREATE INDEX [IX_FK_LoginsSession]
ON [dbo].[SessionSet]
    ([LoginId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------