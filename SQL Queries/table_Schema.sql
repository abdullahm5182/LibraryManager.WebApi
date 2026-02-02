--CREATE DATABASE LibraryManager_Dev
--USE LibraryManager_Dev

CREATE TABLE tbl_Books (
    Id INT IDENTITY(1,1) PRIMARY KEY ,   -- Auto increment
    Title NVARCHAR(255) NOT NULL,
    AuthorFirstName NVARCHAR(100) NOT NULL,
    AuthorLastName NVARCHAR(100) NOT NULL,
    Publisher NVARCHAR(255) NOT NULL,
    YearPublished INT,
    ISBN NVARCHAR(20),
    Price DECIMAL(10,2) NOT NULL
);
  
  INSERT INTO tbl_Books 
    (Title, AuthorFirstName, AuthorLastName, Publisher, YearPublished, ISBN, Price)
VALUES
    ('Clean Code', 'Robert', 'Martin', 'Prentice Hall', 2008, '9780132350884', 45.99),
    ('The Pragmatic Programmer', 'Andrew', 'Hunt', 'Addison-Wesley', 1999, '9780201616224', 39.50),
    ('Design Patterns', 'Erich', 'Gamma', 'Addison-Wesley', 1994, '9780201633610', 49.95),
    ('Refactoring', 'Martin', 'Fowler', 'Addison-Wesley', 1999, '9780201485677', 42.00);


	INSERT INTO tbl_Books 
    (Title, AuthorFirstName, AuthorLastName, Publisher, YearPublished, ISBN, Price)
VALUES 
    ('Domain-Driven Design', 'Eric', 'Evans', 'Addison-Wesley', 2003, '9780321125217', 55.00),
    ('Working Effectively with Legacy Code', 'Michael', 'Feathers', 'Prentice Hall', 2004, '9780131177055', 47.25),
    ('Head First Design Patterns', 'Eric', 'Freeman', 'O�Reilly Media', 2004, '9780596007126', 39.95),
    ('Patterns of Enterprise Application Architecture', 'Martin', 'Fowler', 'Addison-Wesley', 2002, '9780321127426', 50.00),
    ('Agile Software Development', 'Robert', 'Martin', 'Prentice Hall', 2002, '9780135974445', 44.99),
    ('Test Driven Development', 'Kent', 'Beck', 'Addison-Wesley', 2002, '9780321146533', 38.75);
 

CREATE TYPE udt_BookBulk_save AS TABLE
(
    Title NVARCHAR(255) NOT NULL,
    AuthorFirstName NVARCHAR(100) NOT NULL,
    AuthorLastName NVARCHAR(100) NOT NULL,
    Publisher NVARCHAR(255) NOT NULL,
    YearPublished INT,
    ISBN NVARCHAR(20),
    Price DECIMAL(10,2) NOT NULL
)