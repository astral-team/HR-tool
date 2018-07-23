
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/23/2018 10:50:00
-- Generated from EDMX file: C:\Users\Student\Documents\GitHub\HR-tool\httpListener\httpListener\БД\UserDB.edmx
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
IF OBJECT_ID(N'[dbo].[FK_ProfileProfileToPosition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProfileToPositionSet] DROP CONSTRAINT [FK_ProfileProfileToPosition];
GO
IF OBJECT_ID(N'[dbo].[FK_PositionProfileToPosition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProfileToPositionSet] DROP CONSTRAINT [FK_PositionProfileToPosition];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileExperience]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExperienceSet] DROP CONSTRAINT [FK_ProfileExperience];
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
IF OBJECT_ID(N'[dbo].[PositionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PositionSet];
GO
IF OBJECT_ID(N'[dbo].[ProfileSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProfileSet];
GO
IF OBJECT_ID(N'[dbo].[ProfileToPositionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProfileToPositionSet];
GO
IF OBJECT_ID(N'[dbo].[ExperienceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExperienceSet];
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
    [SalaryFrom] bigint  NOT NULL,
    [Schedule] nvarchar(max)  NOT NULL,
    [Trips] bit  NOT NULL,
    [About] nvarchar(max)  NOT NULL,
    [Rate] float  NOT NULL,
    [DateOff] datetimeoffset  NOT NULL,
    [SalaryTo] bigint  NOT NULL,
    [IsOwn] bit  NOT NULL
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
    [Education] nvarchar(max)  NOT NULL,
    [MaritalStatus] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Photo] varbinary(max)  NOT NULL,
    [Сitizen] nvarchar(max)  NOT NULL,
    [About] nvarchar(max)  NOT NULL,
    [DateOff] datetimeoffset  NOT NULL,
    [Responed] bit  NOT NULL,
    [ResumeLink] nvarchar(max)  NOT NULL,
    [Interviews] nvarchar(max)  NOT NULL,
    [IsReadyToTrips] bit  NOT NULL,
    [SalaryTo] bigint  NOT NULL,
    [SalaryFrom] bigint  NOT NULL,
    [Position] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProfileToPositionSet'
CREATE TABLE [dbo].[ProfileToPositionSet] (
    [Id] uniqueidentifier  NOT NULL,
    [ProfileId] uniqueidentifier  NOT NULL,
    [PositionId] uniqueidentifier  NOT NULL,
    [DateOff] datetimeoffset  NOT NULL
);
GO

-- Creating table 'ExperienceSet'
CREATE TABLE [dbo].[ExperienceSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CompanyName] nvarchar(max)  NOT NULL,
    [Position] nvarchar(max)  NOT NULL,
    [ProfileId] uniqueidentifier  NOT NULL,
    [FromDate] datetimeoffset  NOT NULL,
    [ToDate] datetimeoffset  NOT NULL,
    [About] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [DateOff] datetimeoffset  NOT NULL
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

-- Creating primary key on [Id] in table 'ExperienceSet'
ALTER TABLE [dbo].[ExperienceSet]
ADD CONSTRAINT [PK_ExperienceSet]
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

-- Creating foreign key on [ProfileId] in table 'ExperienceSet'
ALTER TABLE [dbo].[ExperienceSet]
ADD CONSTRAINT [FK_ProfileExperience]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[ProfileSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileExperience'
CREATE INDEX [IX_FK_ProfileExperience]
ON [dbo].[ExperienceSet]
    ([ProfileId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------