BEGIN TRANSACTION;
GO

ALTER TABLE [App] ADD [ForAuthorization] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220927114154_ForAuthorization_Field', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [RegisterationInfo] DROP CONSTRAINT [FK_RegisterationInfo_AspNetUsers_UserId];
GO

ALTER TABLE [RegisterationInfo] DROP CONSTRAINT [PK_RegisterationInfo];
GO

EXEC sp_rename N'[RegisterationInfo]', N'RegistrationInfo';
GO

EXEC sp_rename N'[RegistrationInfo].[IX_RegisterationInfo_UserId]', N'IX_RegistrationInfo_UserId', N'INDEX';
GO

ALTER TABLE [RegistrationInfo] ADD CONSTRAINT [PK_RegistrationInfo] PRIMARY KEY ([Id]);
GO

ALTER TABLE [RegistrationInfo] ADD CONSTRAINT [FK_RegistrationInfo_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220927114533_Rename_RegisterationInfo', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId];
GO

ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId];
GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId];
GO

ALTER TABLE [RegistrationInfo] DROP CONSTRAINT [FK_RegistrationInfo_AspNetUsers_UserId];
GO

ALTER TABLE [ScopeClaim] DROP CONSTRAINT [FK_ScopeClaim_AspNetUsers_UserId];
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [PK_AspNetUsers];
GO

EXEC sp_rename N'[AspNetUsers]', N'User';
GO

ALTER TABLE [User] ADD CONSTRAINT [PK_User] PRIMARY KEY ([Id]);
GO

ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK_AspNetUserClaims_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK_AspNetUserLogins_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [RegistrationInfo] ADD CONSTRAINT [FK_RegistrationInfo_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [ScopeClaim] ADD CONSTRAINT [FK_ScopeClaim_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220927115102_RenameToUser', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Teller] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [AppId] int NOT NULL,
    [TellerType] nvarchar(max) NOT NULL,
    [RegisteredDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Teller] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Teller_App_AppId] FOREIGN KEY ([AppId]) REFERENCES [App] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Teller_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TellerRegister] (
    [Id] int NOT NULL IDENTITY,
    [TellerId] int NOT NULL,
    [GroupId] int NOT NULL,
    [OpenDate] datetime2 NOT NULL,
    [CloseDate] datetime2 NULL,
    [RegistererId] nvarchar(max) NOT NULL,
    [CloserId] nvarchar(max) NULL,
    CONSTRAINT [PK_TellerRegister] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TellerRegister_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Group] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TellerRegister_Teller_TellerId] FOREIGN KEY ([TellerId]) REFERENCES [Teller] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Teller_AppId] ON [Teller] ([AppId]);
GO

CREATE UNIQUE INDEX [IX_Teller_UserId_AppId] ON [Teller] ([UserId], [AppId]);
GO

CREATE INDEX [IX_TellerRegister_GroupId] ON [TellerRegister] ([GroupId]);
GO

CREATE INDEX [IX_TellerRegister_TellerId] ON [TellerRegister] ([TellerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221010143123_teller_registration', N'6.0.8');
GO

COMMIT;
GO

