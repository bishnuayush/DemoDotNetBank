
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/22/2022 15:20:13
-- Generated from EDMX file: D:\Swarup\BankAppDemoProject\.Net MVC\ManageCashOfHomeDemo\BankApp.Db\BankDatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BankDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ACCOUNT_BRANCH]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Account] DROP CONSTRAINT [FK_ACCOUNT_BRANCH];
GO
IF OBJECT_ID(N'[dbo].[FK_ACCOUNT_USER]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Account] DROP CONSTRAINT [FK_ACCOUNT_USER];
GO
IF OBJECT_ID(N'[dbo].[FK_CASHTRANSACTION_TRANSACTION]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashTransaction] DROP CONSTRAINT [FK_CASHTRANSACTION_TRANSACTION];
GO
IF OBJECT_ID(N'[dbo].[FK_TRANSACTION_ACCOUNT]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transaction] DROP CONSTRAINT [FK_TRANSACTION_ACCOUNT];
GO
IF OBJECT_ID(N'[dbo].[FK_TRANSACTION_USER]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transaction] DROP CONSTRAINT [FK_TRANSACTION_USER];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account];
GO
IF OBJECT_ID(N'[dbo].[Branch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Branch];
GO
IF OBJECT_ID(N'[dbo].[CashTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashTransaction];
GO
IF OBJECT_ID(N'[dbo].[Transaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transaction];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [AccountId] int  NULL,
    [Amount] int  NULL,
    [Deposit] int  NULL,
    [Withdrawl] int  NULL,
    [Date] datetime  NULL,
    [TransactionType] nvarchar(50)  NULL
);
GO

-- Creating table 'CashTransactions'
CREATE TABLE [dbo].[CashTransactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TransactionId] int  NULL,
    [cash] nvarchar(50)  NULL,
    [Count] int  NULL,
    [C100] int  NULL,
    [C200] int  NULL,
    [C500] int  NULL,
    [C2000] int  NULL
);
GO

-- Creating table 'Branches'
CREATE TABLE [dbo].[Branches] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BanchNumber] nvarchar(100)  NULL,
    [BranchName] nvarchar(100)  NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [AccountNumber] nvarchar(100)  NULL,
    [MinBalance] int  NULL,
    [WithdrawLimit] int  NULL,
    [Balance] int  NULL,
    [BranchId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(100)  NULL,
    [LastName] nvarchar(100)  NULL,
    [MobileNumber] nvarchar(50)  NULL,
    [Address] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NULL,
    [Password] nvarchar(100)  NULL,
    [Image] nvarchar(100)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CashTransactions'
ALTER TABLE [dbo].[CashTransactions]
ADD CONSTRAINT [PK_CashTransactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Branches'
ALTER TABLE [dbo].[Branches]
ADD CONSTRAINT [PK_Branches]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TransactionId] in table 'CashTransactions'
ALTER TABLE [dbo].[CashTransactions]
ADD CONSTRAINT [FK_CASHTRANSACTION_TRANSACTION]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[Transactions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CASHTRANSACTION_TRANSACTION'
CREATE INDEX [IX_FK_CASHTRANSACTION_TRANSACTION]
ON [dbo].[CashTransactions]
    ([TransactionId]);
GO

-- Creating foreign key on [BranchId] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [FK_ACCOUNT_BRANCH]
    FOREIGN KEY ([BranchId])
    REFERENCES [dbo].[Branches]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACCOUNT_BRANCH'
CREATE INDEX [IX_FK_ACCOUNT_BRANCH]
ON [dbo].[Accounts]
    ([BranchId]);
GO

-- Creating foreign key on [AccountId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_TRANSACTION_ACCOUNT]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TRANSACTION_ACCOUNT'
CREATE INDEX [IX_FK_TRANSACTION_ACCOUNT]
ON [dbo].[Transactions]
    ([AccountId]);
GO

-- Creating foreign key on [UserId] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [FK_ACCOUNT_USER]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACCOUNT_USER'
CREATE INDEX [IX_FK_ACCOUNT_USER]
ON [dbo].[Accounts]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_TRANSACTION_USER]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TRANSACTION_USER'
CREATE INDEX [IX_FK_TRANSACTION_USER]
ON [dbo].[Transactions]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------