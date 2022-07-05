BEGIN TRANSACTION;
GO

ALTER TABLE [RegisterationInfo] DROP CONSTRAINT [FK_RegisterationInfo_AspNetUsers_UserId];
GO

DROP INDEX [IX_RegisterationInfo_UserId] ON [RegisterationInfo];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Role]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Role] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Role] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
ALTER TABLE [Role] ADD DEFAULT N'' FOR [Name];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Role]') AND [c].[name] = N'Description');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Role] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Role] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
ALTER TABLE [Role] ADD DEFAULT N'' FOR [Description];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RegisterationInfo]') AND [c].[name] = N'UserId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [RegisterationInfo] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [RegisterationInfo] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
ALTER TABLE [RegisterationInfo] ADD DEFAULT N'' FOR [UserId];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RegisterationInfo]') AND [c].[name] = N'RegistrarId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [RegisterationInfo] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [RegisterationInfo] ALTER COLUMN [RegistrarId] nvarchar(max) NOT NULL;
ALTER TABLE [RegisterationInfo] ADD DEFAULT N'' FOR [RegistrarId];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DocumentCategory]') AND [c].[name] = N'Description');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [DocumentCategory] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [DocumentCategory] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
ALTER TABLE [DocumentCategory] ADD DEFAULT N'' FOR [Description];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Document]') AND [c].[name] = N'Description');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Document] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Document] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
ALTER TABLE [Document] ADD DEFAULT N'' FOR [Description];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'ZipCode');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [ZipCode] nvarchar(max) NOT NULL;
ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [ZipCode];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'MiddleName');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [MiddleName] nvarchar(max) NOT NULL;
ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [MiddleName];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'LastName');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [LastName] nvarchar(max) NOT NULL;
ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [LastName];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'IsTellerable');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [AspNetUsers] ADD DEFAULT CAST(0 AS bit) FOR [IsTellerable];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Gender');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [Gender] nvarchar(max) NOT NULL;
ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [Gender];
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'FirstName');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [FirstName] nvarchar(max) NOT NULL;
ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [FirstName];
GO

ALTER TABLE [AspNetUsers] ADD [IsPerson] bit NOT NULL DEFAULT CAST(1 AS bit);
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppDocumentCategory]') AND [c].[name] = N'Name');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [AppDocumentCategory] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [AppDocumentCategory] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
ALTER TABLE [AppDocumentCategory] ADD DEFAULT N'' FOR [Name];
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppDocumentCategory]') AND [c].[name] = N'Description');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [AppDocumentCategory] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [AppDocumentCategory] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
ALTER TABLE [AppDocumentCategory] ADD DEFAULT N'' FOR [Description];
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'FullName');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [FullName] NVARCHAR(MAX) NOT NULL;
GO

CREATE TABLE [App] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_App] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [GroupLocation] (
    [Id] int NOT NULL IDENTITY,
    [Country] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [Suburb] nvarchar(max) NOT NULL,
    [Place] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_GroupLocation] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [GroupProfile] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Postal] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Website] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_GroupProfile] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [GroupType] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_GroupType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Group] (
    [Id] int NOT NULL IDENTITY,
    [AppId] int NOT NULL,
    [GroupLocationId] int NULL,
    [GroupProfileId] int NULL,
    [SuperGroupId] int NULL,
    [Description] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Group] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Group_App_AppId] FOREIGN KEY ([AppId]) REFERENCES [App] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Group_Group_SuperGroupId] FOREIGN KEY ([SuperGroupId]) REFERENCES [Group] ([Id]),
    CONSTRAINT [FK_Group_GroupLocation_GroupLocationId] FOREIGN KEY ([GroupLocationId]) REFERENCES [GroupLocation] ([Id]),
    CONSTRAINT [FK_Group_GroupProfile_GroupProfileId] FOREIGN KEY ([GroupProfileId]) REFERENCES [GroupProfile] ([Id])
);
GO

CREATE TABLE [Department] (
    [Id] int NOT NULL IDENTITY,
    [GroupId] int NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Department_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Group] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_RegisterationInfo_UserId] ON [RegisterationInfo] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_App_Name] ON [App] ([Name]);
GO

CREATE UNIQUE INDEX [IX_Department_GroupId_Name] ON [Department] ([GroupId], [Name]);
GO

CREATE INDEX [IX_Group_AppId] ON [Group] ([AppId]);
GO

CREATE INDEX [IX_Group_GroupLocationId] ON [Group] ([GroupLocationId]);
GO

CREATE INDEX [IX_Group_GroupProfileId] ON [Group] ([GroupProfileId]);
GO

CREATE INDEX [IX_Group_SuperGroupId] ON [Group] ([SuperGroupId]);
GO

ALTER TABLE [RegisterationInfo] ADD CONSTRAINT [FK_RegisterationInfo_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220704210219_Groups_Apps_Others', N'6.0.2');
GO

COMMIT;
GO

