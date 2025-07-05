
CREATE  OR ALTER  PROCEDURE [dbo].[sp_Find_Users_By_Email]
    @Email VARCHAR(256)
AS
BEGIN
    select * from Users where Email = @Email
END;

GO

CREATE  OR ALTER  PROCEDURE [dbo].[sp_Insert_Session]
    @IpSession VARCHAR(50),
    @Token VARCHAR(MAX),
    @IssueDate DATETIME,
    @ExpirationDate DATETIME,
    @IsEnabled BIT,
    @UsersId UNIQUEIDENTIFIER,
    @CreatedAt DATETIME,
    @CreatedBy VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Session] (
        IpSession,
        Token,
        IssueDate,
        ExpirationDate,
        IsEnabled,
        UsersId,
        CreatedAt,
        CreatedBy,
        UpdatedAt,
        UpdatedBy,
        DeletedAt,
        DeletedBy
    )
    VALUES (
        @IpSession,
        @Token,
        @IssueDate,
        @ExpirationDate,
        @IsEnabled,
        @UsersId,
        @CreatedAt,
        @CreatedBy,
        NULL, 
        NULL, 
        NULL, 
        NULL  
    );
END;

GO

CREATE  OR ALTER  PROCEDURE [dbo].[sp_Insert_User]
    @Id UNIQUEIDENTIFIER,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(256),
    @Password VARCHAR(8000),
    @PhoneNumber VARCHAR(20),
    @Address VARCHAR(510),
    @DateOfBirth DATE,
    @IsActive BIT,
    @LoginAttempts INT = 0,
    @CreatedBy VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Users] (
        [Id],
        [FirstName],
        [LastName],
        [Email],
        [Password],
        [PhoneNumber],
        [Address],
        [DateOfBirth],
        [IsActive],
        [LoginAttempts],
        [CreatedAt],
        [CreatedBy],
        [UpdatedAt],
        [UpdatedBy],
        [DeletedAt],
        [DeletedBy]
    )
    VALUES (
        @Id,
        @FirstName,
        @LastName,
        @Email,
        @Password,
        @PhoneNumber,
        @Address,
        @DateOfBirth,
        @IsActive,
        @LoginAttempts,
        GETDATE(),
        @CreatedBy,
        NULL, NULL, 
        NULL, NULL
    );
END;

GO

CREATE  OR ALTER  PROCEDURE [dbo].[sp_InsertUser]
    @Id UNIQUEIDENTIFIER,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(256),
    @Password VARCHAR(8000),
    @PhoneNumber VARCHAR(20),
    @Address VARCHAR(510),
    @DateOfBirth DATE,
    @IsActive BIT,
    @LoginAttempts INT = 0,
    @CreatedBy VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Users] (
        [Id],
        [FirstName],
        [LastName],
        [Email],
        [Password],
        [PhoneNumber],
        [Address],
        [DateOfBirth],
        [IsActive],
        [LoginAttempts],
        [CreatedAt],
        [CreatedBy],
        [UpdatedAt],
        [UpdatedBy],
        [DeletedAt],
        [DeletedBy]
    )
    VALUES (
        @Id,
        @FirstName,
        @LastName,
        @Email,
        @Password,
        @PhoneNumber,
        @Address,
        @DateOfBirth,
        @IsActive,
        @LoginAttempts,
        GETDATE(),
        @CreatedBy,
        NULL, NULL, 
        NULL, NULL
    );
END;

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_RegisterUser]
    @Id UNIQUEIDENTIFIER,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(256),
    @Password VARCHAR(MAX),
    @PhoneNumber VARCHAR(20),
    @Address VARCHAR(255),
    @DateOfBirth DATE
AS
BEGIN

    INSERT INTO Users (
        Id, FirstName, LastName, Email, Password,
        PhoneNumber, Address, DateOfBirth,
        CreatedAt, IsActive
    )
    VALUES (
        @Id, @FirstName, @LastName, @Email, @Password,
        @PhoneNumber, @Address, @DateOfBirth,
        GETUTCDATE(), 1
    );
END;

GO

CREATE  OR ALTER  PROCEDURE [dbo].[sp_Update_Attempts_Users_By_Id]
    @Id uniqueidentifier,
	@Attempts int
AS
BEGIN
    update Users set LoginAttempts = @Attempts,UpdatedBy = CreatedBy,UpdatedAt = GETDATE()  where Id = @Id 
END;

GO

CREATE  OR ALTER  PROCEDURE [dbo].[sp_Update_Enabled_Session]
	@UsersId UNIQUEIDENTIFIER,
	@IsEnabled BIT

AS
BEGIN

	update [Session] set IsEnabled = @IsEnabled where UsersId = @UsersId

END
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_Find_Subscriptions_By_UserId]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        a.*,
		ss.*
    FROM Subscription a
	INNER JOIN StatusSubscription ss ON ss.id = a.StatusId
    WHERE UserId = @UserId
END;