IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Projects] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Tasks] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [ProjectId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tasks_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Entries] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [Hours] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [TaskId] int NOT NULL,
    CONSTRAINT [PK_Entries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Entries_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Entries_TaskId] ON [Entries] ([TaskId]);
GO

CREATE INDEX [IX_Tasks_ProjectId] ON [Tasks] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240619125718_InitialCreate', N'8.0.6');
GO

COMMIT;
GO

