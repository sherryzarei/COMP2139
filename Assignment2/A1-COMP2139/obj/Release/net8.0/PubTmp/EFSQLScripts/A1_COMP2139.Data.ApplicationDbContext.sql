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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    IF SCHEMA_ID(N'Identity') IS NULL EXEC(N'CREATE SCHEMA [Identity];');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[Cars] (
        [CarId] int NOT NULL IDENTITY,
        [RentalCompany] nvarchar(50) NOT NULL,
        [Manufacturer] nvarchar(50) NOT NULL,
        [Model] nvarchar(50) NOT NULL,
        [Year] int NOT NULL,
        [Price] float NOT NULL,
        [Availability] int NOT NULL,
        [ImageName] nvarchar(max) NULL,
        CONSTRAINT [PK_Cars] PRIMARY KEY ([CarId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[Flights] (
        [FlightId] int NOT NULL IDENTITY,
        [FlightNumber] nvarchar(max) NOT NULL,
        [Airline] nvarchar(50) NOT NULL,
        [Departure] nvarchar(100) NOT NULL,
        [Arrival] nvarchar(100) NOT NULL,
        [DepartureDate] datetime2 NOT NULL,
        [ArrivalDate] datetime2 NOT NULL,
        [Price] float NOT NULL,
        [Availability] int NOT NULL,
        [ImageName] nvarchar(max) NULL,
        CONSTRAINT [PK_Flights] PRIMARY KEY ([FlightId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[Hotels] (
        [HotelId] int NOT NULL IDENTITY,
        [HotelName] nvarchar(25) NOT NULL,
        [Location] nvarchar(100) NOT NULL,
        [Amenities] nvarchar(max) NULL,
        [Availability] int NOT NULL,
        [Price] real NOT NULL,
        [ImageName] nvarchar(max) NULL,
        CONSTRAINT [PK_Hotels] PRIMARY KEY ([HotelId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[Role] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[User] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [usernameChangeLimit] int NOT NULL,
        [ProfilePicture] varbinary(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[Book] (
        [BookId] int NOT NULL IDENTITY,
        [FlightId] int NULL,
        [HotelId] int NULL,
        [CarId] int NULL,
        [GuestEmail] nvarchar(max) NULL,
        [GuestNumber] nvarchar(max) NULL,
        [ReceiptNumber] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Book] PRIMARY KEY ([BookId]),
        CONSTRAINT [FK_Book_Cars_CarId] FOREIGN KEY ([CarId]) REFERENCES [Identity].[Cars] ([CarId]),
        CONSTRAINT [FK_Book_Flights_FlightId] FOREIGN KEY ([FlightId]) REFERENCES [Identity].[Flights] ([FlightId]),
        CONSTRAINT [FK_Book_Hotels_HotelId] FOREIGN KEY ([HotelId]) REFERENCES [Identity].[Hotels] ([HotelId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[RoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RoleClaims_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Role] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[UserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserClaims_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[UserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_UserLogins_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[UserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_UserRoles_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Role] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserRoles_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE TABLE [Identity].[UserToken] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_UserToken] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_UserToken_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE INDEX [IX_Book_CarId] ON [Identity].[Book] ([CarId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE INDEX [IX_Book_FlightId] ON [Identity].[Book] ([FlightId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE INDEX [IX_Book_HotelId] ON [Identity].[Book] ([HotelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Identity].[Role] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE INDEX [IX_RoleClaims_RoleId] ON [Identity].[RoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [Identity].[User] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Identity].[User] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE INDEX [IX_UserClaims_UserId] ON [Identity].[UserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE INDEX [IX_UserLogins_UserId] ON [Identity].[UserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    CREATE INDEX [IX_UserRoles_RoleId] ON [Identity].[UserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410153729_UserIdentity'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240410153729_UserIdentity', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410172242_Roles'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240410172242_Roles', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410175259_RolesChange'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240410175259_RolesChange', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410185701_NewChanges'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Identity].[UserToken]') AND [c].[name] = N'Name');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Identity].[UserToken] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Identity].[UserToken] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410185701_NewChanges'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Identity].[UserToken]') AND [c].[name] = N'LoginProvider');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Identity].[UserToken] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Identity].[UserToken] ALTER COLUMN [LoginProvider] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410185701_NewChanges'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Identity].[UserLogins]') AND [c].[name] = N'ProviderKey');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Identity].[UserLogins] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Identity].[UserLogins] ALTER COLUMN [ProviderKey] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410185701_NewChanges'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Identity].[UserLogins]') AND [c].[name] = N'LoginProvider');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Identity].[UserLogins] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Identity].[UserLogins] ALTER COLUMN [LoginProvider] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240410185701_NewChanges'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240410185701_NewChanges', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414012049_CarComment'
)
BEGIN
    CREATE TABLE [Identity].[CarComments] (
        [carCommentId] int NOT NULL IDENTITY,
        [Content] nvarchar(500) NOT NULL,
        [DatePosted] datetime2 NOT NULL,
        [carId] int NOT NULL,
        CONSTRAINT [PK_CarComments] PRIMARY KEY ([carCommentId]),
        CONSTRAINT [FK_CarComments_Cars_carId] FOREIGN KEY ([carId]) REFERENCES [Identity].[Cars] ([CarId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414012049_CarComment'
)
BEGIN
    CREATE TABLE [Identity].[FlightComments] (
        [FlightCommentId] int NOT NULL IDENTITY,
        [Content] nvarchar(500) NOT NULL,
        [DatePosted] datetime2 NOT NULL,
        [FlightId] int NOT NULL,
        CONSTRAINT [PK_FlightComments] PRIMARY KEY ([FlightCommentId]),
        CONSTRAINT [FK_FlightComments_Flights_FlightId] FOREIGN KEY ([FlightId]) REFERENCES [Identity].[Flights] ([FlightId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414012049_CarComment'
)
BEGIN
    CREATE TABLE [Identity].[HotelComments] (
        [HotelCommentId] int NOT NULL IDENTITY,
        [Content] nvarchar(500) NOT NULL,
        [DatePosted] datetime2 NOT NULL,
        [HotelId] int NOT NULL,
        CONSTRAINT [PK_HotelComments] PRIMARY KEY ([HotelCommentId]),
        CONSTRAINT [FK_HotelComments_Hotels_HotelId] FOREIGN KEY ([HotelId]) REFERENCES [Identity].[Hotels] ([HotelId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414012049_CarComment'
)
BEGIN
    CREATE INDEX [IX_CarComments_carId] ON [Identity].[CarComments] ([carId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414012049_CarComment'
)
BEGIN
    CREATE INDEX [IX_FlightComments_FlightId] ON [Identity].[FlightComments] ([FlightId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414012049_CarComment'
)
BEGIN
    CREATE INDEX [IX_HotelComments_HotelId] ON [Identity].[HotelComments] ([HotelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414012049_CarComment'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240414012049_CarComment', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414014515_FlightComment'
)
BEGIN
    ALTER TABLE [Identity].[FlightComments] DROP CONSTRAINT [FK_FlightComments_Flights_FlightId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414014515_FlightComment'
)
BEGIN
    EXEC sp_rename N'[Identity].[FlightComments].[FlightId]', N'flightId', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414014515_FlightComment'
)
BEGIN
    EXEC sp_rename N'[Identity].[FlightComments].[FlightCommentId]', N'flightCommentId', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414014515_FlightComment'
)
BEGIN
    EXEC sp_rename N'[Identity].[FlightComments].[IX_FlightComments_FlightId]', N'IX_FlightComments_flightId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414014515_FlightComment'
)
BEGIN
    ALTER TABLE [Identity].[FlightComments] ADD CONSTRAINT [FK_FlightComments_Flights_flightId] FOREIGN KEY ([flightId]) REFERENCES [Identity].[Flights] ([FlightId]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240414014515_FlightComment'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240414014515_FlightComment', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    ALTER TABLE [Identity].[HotelComments] DROP CONSTRAINT [FK_HotelComments_Hotels_HotelId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    EXEC sp_rename N'[Identity].[HotelComments].[HotelId]', N'hotelId', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    EXEC sp_rename N'[Identity].[HotelComments].[HotelCommentId]', N'hotelCommentId', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    EXEC sp_rename N'[Identity].[HotelComments].[IX_HotelComments_HotelId]', N'IX_HotelComments_hotelId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    ALTER TABLE [Identity].[Book] ADD [Email] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    ALTER TABLE [Identity].[Book] ADD [UserId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    ALTER TABLE [Identity].[Book] ADD [Username] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    CREATE INDEX [IX_Book_UserId] ON [Identity].[Book] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    ALTER TABLE [Identity].[Book] ADD CONSTRAINT [FK_Book_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    ALTER TABLE [Identity].[HotelComments] ADD CONSTRAINT [FK_HotelComments_Hotels_hotelId] FOREIGN KEY ([hotelId]) REFERENCES [Identity].[Hotels] ([HotelId]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416165420_BookUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240416165420_BookUser', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416172359_BookOptional'
)
BEGIN
    ALTER TABLE [Identity].[Book] DROP CONSTRAINT [FK_Book_User_UserId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416172359_BookOptional'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Identity].[Book]') AND [c].[name] = N'Username');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Identity].[Book] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Identity].[Book] ALTER COLUMN [Username] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416172359_BookOptional'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Identity].[Book]') AND [c].[name] = N'UserId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Identity].[Book] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Identity].[Book] ALTER COLUMN [UserId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416172359_BookOptional'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Identity].[Book]') AND [c].[name] = N'Email');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Identity].[Book] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Identity].[Book] ALTER COLUMN [Email] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416172359_BookOptional'
)
BEGIN
    ALTER TABLE [Identity].[Book] ADD CONSTRAINT [FK_Book_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416172359_BookOptional'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240416172359_BookOptional', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240416230328_mssql_migration_811'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240416230328_mssql_migration_811', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417002252_mssql_migration_406'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240417002252_mssql_migration_406', N'8.0.4');
END;
GO

COMMIT;
GO

