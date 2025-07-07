CREATE DATABASE LibraryDB;
GO
USE LibraryDB;
GO

-- Tabele
CREATE TABLE Authors (
    AuthorID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Country NVARCHAR(50)
);

CREATE TABLE Publishers (
    PublisherID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Books (
    BookID INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    YearPublished INT,
    AuthorID INT NOT NULL REFERENCES Authors(AuthorID),
    PublisherID INT NULL,
    CONSTRAINT FK_Books_Publishers FOREIGN KEY (PublisherID) REFERENCES Publishers(PublisherID)
);

CREATE TABLE Genres (
    GenreID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50)
);

CREATE TABLE Members (
    MemberID INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(100)
);

CREATE TABLE Loans (
    LoanID INT IDENTITY PRIMARY KEY,
    BookID INT,
    MemberID INT,
    LoanDate DATE
);

CREATE TABLE BookGenres (
    BookID INT,
    GenreID INT,
    PRIMARY KEY (BookID, GenreID)
);

CREATE TABLE Countries (
    CountryID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50)
);

CREATE TABLE AuthorAwards (
    AuthorID INT,
    Award NVARCHAR(100)
);

CREATE TABLE Addresses (
    AddressID INT IDENTITY PRIMARY KEY,
    MemberID INT,
    Line1 NVARCHAR(100)
);

-- Date ini?iale
INSERT INTO Authors(Name, Country) VALUES
 ('Jane Austen', 'UK'),
 ('Mark Twain', 'USA'),
 ('Haruki Murakami', 'Japan');

INSERT INTO Publishers(Name) VALUES
('Penguin Random House'),
('HarperCollins');

INSERT INTO Books(Title, YearPublished, AuthorID) VALUES
 ('Pride and Prejudice', 1813, 1),
 ('Sense and Sensibility', 1811, 1),
 ('Adventures of Huckleberry Finn', 1884, 2),
 ('Norwegian Wood', 1987, 3);

-- Proceduri stocate: DROP dac? exist? + CREATE

IF OBJECT_ID('sp_InsertBook', 'P') IS NOT NULL
    DROP PROCEDURE sp_InsertBook;
GO
CREATE PROCEDURE sp_InsertBook
  @Title NVARCHAR(150), 
  @YearPublished INT, 
  @AuthorID INT
AS
INSERT INTO Books(Title, YearPublished, AuthorID) VALUES (@Title, @YearPublished, @AuthorID);
GO

IF OBJECT_ID('sp_UpdateBook', 'P') IS NOT NULL
    DROP PROCEDURE sp_UpdateBook;
GO
CREATE PROCEDURE sp_UpdateBook
  @BookID INT, 
  @Title NVARCHAR(150), 
  @YearPublished INT
AS
UPDATE Books SET Title=@Title, YearPublished=@YearPublished WHERE BookID=@BookID;
GO

IF OBJECT_ID('sp_DeleteBook', 'P') IS NOT NULL
    DROP PROCEDURE sp_DeleteBook;
GO
CREATE PROCEDURE sp_DeleteBook
  @BookID INT
AS
DELETE FROM Books WHERE BookID=@BookID;
GO

IF OBJECT_ID('sp_InsertPublisher', 'P') IS NOT NULL
    DROP PROCEDURE sp_InsertPublisher;
GO
CREATE PROCEDURE sp_InsertPublisher
  @Name NVARCHAR(100)
AS
INSERT INTO Publishers(Name) VALUES (@Name);
GO

IF OBJECT_ID('sp_UpdatePublisher', 'P') IS NOT NULL
    DROP PROCEDURE sp_UpdatePublisher;
GO
CREATE PROCEDURE sp_UpdatePublisher
  @PublisherID INT, 
  @Name NVARCHAR(100)
AS
UPDATE Publishers SET Name=@Name WHERE PublisherID=@PublisherID;
GO

IF OBJECT_ID('sp_DeletePublisher', 'P') IS NOT NULL
    DROP PROCEDURE sp_DeletePublisher;
GO
CREATE PROCEDURE sp_DeletePublisher
  @PublisherID INT
AS
DELETE FROM Publishers WHERE PublisherID=@PublisherID;
GO

IF OBJECT_ID('sp_InsertBookWithPublisher', 'P') IS NOT NULL
    DROP PROCEDURE sp_InsertBookWithPublisher;
GO
CREATE PROCEDURE sp_InsertBookWithPublisher
  @Title NVARCHAR(150), 
  @YearPublished INT, 
  @AuthorID INT,
  @PublisherID INT = NULL
AS
INSERT INTO Books(Title, YearPublished, AuthorID, PublisherID) 
VALUES (@Title, @YearPublished, @AuthorID, @PublisherID);
GO

IF OBJECT_ID('sp_UpdateBookWithPublisher', 'P') IS NOT NULL
    DROP PROCEDURE sp_UpdateBookWithPublisher;
GO
CREATE PROCEDURE sp_UpdateBookWithPublisher
  @BookID INT, 
  @Title NVARCHAR(150), 
  @YearPublished INT,
  @AuthorID INT,
  @PublisherID INT = NULL
AS
UPDATE Books 
SET Title = @Title, 
    YearPublished = @YearPublished,
    AuthorID = @AuthorID,
    PublisherID = @PublisherID
WHERE BookID = @BookID;
GO

SELECT * FROM Books
SELECT * FROM Authors
SELECT * FROM Publishers


UPDATE Books SET PublisherID = 1 WHERE BookID IN (1, 2);

UPDATE Books SET PublisherID = 1 WHERE BookID IN (6);

UPDATE Books SET PublisherID = 2 WHERE BookID IN (3, 4);

ALTER PROCEDURE sp_InsertBook
  @Title NVARCHAR(150), 
  @YearPublished INT, 
  @AuthorID INT,
  @PublisherID INT = NULL
AS
INSERT INTO Books(Title, YearPublished, AuthorID, PublisherID) 
VALUES (@Title, @YearPublished, @AuthorID, @PublisherID);


ALTER PROCEDURE sp_UpdateBook
  @BookID       INT,
  @Title        NVARCHAR(150),
  @YearPublished INT,
  @AuthorID     INT = NULL,        -- ad?ugat
  @PublisherID  INT = NULL         -- ad?ugat
AS
UPDATE Books
SET Title = @Title,
    YearPublished = @YearPublished
WHERE BookID = @BookID;
GO
