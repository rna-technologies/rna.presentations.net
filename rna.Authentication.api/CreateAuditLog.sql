BEGIN TRANSACTION;
GO

ALTER TABLE [TellerRegister] DROP CONSTRAINT [FK_TellerRegister_Teller_TellerId];
GO

CREATE TABLE [AuditLog] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(max) NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [TableName] nvarchar(max) NOT NULL,
    [DateTime] datetime2 NOT NULL,
    [OldValues] nvarchar(max) NULL,
    [NewValues] nvarchar(max) NULL,
    [AffectedColumns] nvarchar(max) NULL,
    [PrimaryKey] nvarchar(max) NOT NULL,
    [AppId] int NOT NULL,
    [GroupId] int NULL,
    [DepartmentId] int NULL,
    CONSTRAINT [PK_AuditLog] PRIMARY KEY ([Id])
);
GO

ALTER TABLE [TellerRegister] ADD CONSTRAINT [FK_TellerRegister_Teller_TellerId] FOREIGN KEY ([TellerId]) REFERENCES [Teller] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230416094655_Create_AuditLog', N'6.0.8');
GO

COMMIT;
GO

