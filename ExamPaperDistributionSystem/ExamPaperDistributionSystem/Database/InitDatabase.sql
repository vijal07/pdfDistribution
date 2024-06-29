-- Create the database
CREATE DATABASE ExamPaperDB;
GO

USE ExamPaperDB;
GO

-- Create Roles table
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- Create Users table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
GO

-- Create ExamPapers table
CREATE TABLE ExamPapers (
    PaperId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    FileName NVARCHAR(100) NOT NULL,
    FilePath NVARCHAR(255) NOT NULL,
    UploadedBy  NVARCHAR(50) NOT NULL,
    UploadedAt DATETIME NOT NULL DEFAULT GETDATE(),
    Encrypted VARBINARY(MAX) NOT NULL,
    FOREIGN KEY (UploadedBy) REFERENCES Users(UserId)
);
GO

-- Create AccessLogs table
CREATE TABLE AccessLogs (
    LogId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Action NVARCHAR(100) NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
GO

-- Insert default roles
INSERT INTO Roles (RoleName) VALUES ('Administrator'), ('Examiner'), ('Invigilator');
GO

-- Insert default admin user
INSERT INTO Users (Username, Password, RoleId) VALUES ('admin', 'admin_password_hash', 1);
GO

-- Create stored procedures

-- Procedure to add a new user
CREATE PROCEDURE AddUser
    @Username NVARCHAR(50),
    @Password NVARCHAR(255),
    @RoleId INT
AS
BEGIN
    INSERT INTO Users (Username, Password, RoleId)
    VALUES (@Username, @Password, @RoleId);
END;
GO

-- Procedure to get a user by username
CREATE PROCEDURE GetUserByUsername
    @Username NVARCHAR(50)
AS
BEGIN
    SELECT UserId, Username, Password, RoleId
    FROM Users
    WHERE Username = @Username;
END;
GO

-- Procedure to add an exam paper
CREATE PROCEDURE AddExamPaper
    @Title NVARCHAR(100),
    @FilePath NVARCHAR(255),
    @UploadedBy INT,
    @Encrypted VARBINARY(MAX)
AS
BEGIN
    INSERT INTO ExamPapers (Title, FilePath, UploadedBy, Encrypted)
    VALUES (@Title, @FilePath, @UploadedBy, @Encrypted);
END;
GO

-- Procedure to get all exam papers
CREATE PROCEDURE GetAllExamPapers
AS
BEGIN
    SELECT PaperId, Title, FilePath, UploadedBy, UploadedAt
    FROM ExamPapers;
END;
GO

-- Procedure to log access
CREATE PROCEDURE LogAccess
    @UserId INT,
    @Action NVARCHAR(100)
AS
BEGIN
    INSERT INTO AccessLogs (UserId, Action)
    VALUES (@UserId, @Action);
END;
GO
