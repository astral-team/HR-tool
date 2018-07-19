
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/19/2018 10:11:26
-- Generated from EDMX file: C:\Users\home.PTIZ\Documents\GitHub\HR-tool\httpListener\httpListener\БД\UserDB.edmx
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
    [DateOff] datetimeoffset  NOT NULL,
    [Hash] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SessionSet'
CREATE TABLE [dbo].[SessionSet] (
    [Id] uniqueidentifier  NOT NULL,
    [LoginId] uniqueidentifier  NOT NULL,
    [SessionKey] uniqueidentifier  NOT NULL,
    [ExpTime] datetimeoffset  NOT NULL
);
GO

-- Creating table 'PositionSet'
CREATE TABLE [dbo].[PositionSet] (
    [Id] uniqueidentifier  NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [Salary] nvarchar(max)  NOT NULL,
    [Schedule] nvarchar(max)  NOT NULL,
    [Trips] bit  NOT NULL,
    [About] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProfileSet'
CREATE TABLE [dbo].[ProfileSet] (
    [Id] uniqueidentifier  NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [BirthDate] datetimeoffset  NOT NULL,
    [PhoneNumer] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Sex] bit  NOT NULL,
    [Position] nvarchar(max)  NOT NULL,
    [Education] nvarchar(max)  NOT NULL,
    [MaritalStatus] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Photo] varbinary(max)  NOT NULL,
    [Children] bit  NOT NULL,
    [Сitizen] nvarchar(max)  NOT NULL,
    [About] nvarchar(max)  NOT NULL,
    [DateOff] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProfileToPositionSet'
CREATE TABLE [dbo].[ProfileToPositionSet] (
    [Id] uniqueidentifier  NOT NULL,
    [ProfileId] uniqueidentifier  NOT NULL,
    [PositionId] uniqueidentifier  NOT NULL
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

-- Creating primary key on [Id] in table 'PositionSet'
ALTER TABLE [dbo].[PositionSet]
ADD CONSTRAINT [PK_PositionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProfileSet'
ALTER TABLE [dbo].[ProfileSet]
ADD CONSTRAINT [PK_ProfileSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProfileToPositionSet'
ALTER TABLE [dbo].[ProfileToPositionSet]
ADD CONSTRAINT [PK_ProfileToPositionSet]
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

-- Creating foreign key on [ProfileId] in table 'ProfileToPositionSet'
ALTER TABLE [dbo].[ProfileToPositionSet]
ADD CONSTRAINT [FK_ProfileProfileToPosition]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[ProfileSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileProfileToPosition'
CREATE INDEX [IX_FK_ProfileProfileToPosition]
ON [dbo].[ProfileToPositionSet]
    ([ProfileId]);
GO

-- Creating foreign key on [PositionId] in table 'ProfileToPositionSet'
ALTER TABLE [dbo].[ProfileToPositionSet]
ADD CONSTRAINT [FK_PositionProfileToPosition]
    FOREIGN KEY ([PositionId])
    REFERENCES [dbo].[PositionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PositionProfileToPosition'
CREATE INDEX [IX_FK_PositionProfileToPosition]
ON [dbo].[ProfileToPositionSet]
    ([PositionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------