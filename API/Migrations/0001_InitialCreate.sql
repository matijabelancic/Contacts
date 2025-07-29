IF OBJECT_ID('dbo.Contacts', 'U') IS NULL
BEGIN
CREATE TABLE dbo.Contacts
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Surname NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NULL,
);
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Contacts)
BEGIN
INSERT INTO dbo.Contacts (Id, Name, Surname, Email, CreatedAt, UpdatedAt)
VALUES
    (NEWID(), 'James', 'Smith', 'james.smith@example.com', GETDATE(), NULL),
    (NEWID(), 'Mary', 'Johnson', 'mary.johnson@example.org', GETDATE(), NULL),
    (NEWID(), 'Robert', 'Williams', 'robert.williams@example.net', GETDATE(), NULL),
    (NEWID(), 'Patricia', 'Brown', 'patricia.brown@example.com', GETDATE(), NULL),
    (NEWID(), 'Michael', 'Jones', 'michael.jones@example.org', GETDATE(), NULL),
    (NEWID(), 'Linda', 'Garcia', 'linda.garcia@example.net', GETDATE(), NULL),
    (NEWID(), 'William', 'Miller', 'william.miller@example.com', GETDATE(), NULL),
    (NEWID(), 'Barbara', 'Davis', 'barbara.davis@example.org', GETDATE(), NULL),
    (NEWID(), 'David', 'Rodriguez', 'david.rodriguez@example.net', GETDATE(), NULL),
    (NEWID(), 'Elizabeth', 'Martinez', 'elizabeth.martinez@example.com', GETDATE(), NULL),
    (NEWID(), 'Richard', 'Hernandez', 'richard.hernandez@example.org', GETDATE(), NULL),
    (NEWID(), 'Susan', 'Lopez', 'susan.lopez@example.net', GETDATE(), NULL),
    (NEWID(), 'Joseph', 'Gonzalez', 'joseph.gonzalez@example.com', GETDATE(), NULL),
    (NEWID(), 'Jessica', 'Wilson', 'jessica.wilson@example.org', GETDATE(), NULL),
    (NEWID(), 'Thomas', 'Anderson', 'thomas.anderson@example.net', GETDATE(), NULL),
    (NEWID(), 'Sarah', 'Thomas', 'sarah.thomas@example.com', GETDATE(), NULL),
    (NEWID(), 'Charles', 'Taylor', 'charles.taylor@example.org', GETDATE(), NULL),
    (NEWID(), 'Karen', 'Moore', 'karen.moore@example.net', GETDATE(), NULL),
    (NEWID(), 'Christopher', 'Jackson', 'christopher.jackson@example.com', GETDATE(), NULL),
    (NEWID(), 'Nancy', 'Martin', 'nancy.martin@example.org', GETDATE(), NULL),
    (NEWID(), 'Daniel', 'Lee', 'daniel.lee@example.net', GETDATE(), NULL),
    (NEWID(), 'Lisa', 'Perez', 'lisa.perez@example.com', GETDATE(), NULL),
    (NEWID(), 'Matthew', 'Thompson', 'matthew.thompson@example.org', GETDATE(), NULL),
    (NEWID(), 'Margaret', 'White', 'margaret.white@example.net', GETDATE(), NULL),
    (NEWID(), 'Anthony', 'Harris', 'anthony.harris@example.com', GETDATE(), NULL),
    (NEWID(), 'Sandra', 'Sanchez', 'sandra.sanchez@example.org', GETDATE(), NULL),
    (NEWID(), 'Mark', 'Clark', 'mark.clark@example.net', GETDATE(), NULL),
    (NEWID(), 'Donna', 'Ramirez', 'donna.ramirez@example.com', GETDATE(), NULL),
    (NEWID(), 'Paul', 'Lewis', 'paul.lewis@example.org', GETDATE(), NULL),
    (NEWID(), 'Carol', 'Robinson', 'carol.robinson@example.net', GETDATE(), NULL);
END
GO

