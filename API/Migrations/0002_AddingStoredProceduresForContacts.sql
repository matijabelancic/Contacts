CREATE PROCEDURE dbo.GetContactById
    @Id UNIQUEIDENTIFIER
AS
BEGIN
SELECT * FROM dbo.Contacts WHERE Id = @Id;
END
GO

CREATE PROCEDURE dbo.GetContactsPaged
    @PageNumber INT,
    @PageSize INT,
    @SortColumn NVARCHAR(50),
    @SortDirection BIT = 1, -- 1 = ASC, 0 = DESC
    @SearchTerm NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

SELECT COUNT(*) AS TotalCount
FROM dbo.Contacts
WHERE (@SearchTerm IS NULL OR
       Name LIKE '%' + @SearchTerm + '%' OR
       Surname LIKE '%' + @SearchTerm + '%' OR
       Email LIKE '%' + @SearchTerm + '%');

;WITH ContactsCTE AS (
    SELECT
        Id,
        Name,
        Surname,
        Email,
        CreatedAt,
        UpdatedAt,
        ROW_NUMBER() OVER (
                ORDER BY
                    CASE WHEN @SortColumn = 'Name' AND @SortDirection = 1 THEN Name END ASC,
                    CASE WHEN @SortColumn = 'Name' AND @SortDirection = 0 THEN Name END DESC,
                    CASE WHEN @SortColumn = 'Surname' AND @SortDirection = 1 THEN Surname END ASC,
                    CASE WHEN @SortColumn = 'Surname' AND @SortDirection = 0 THEN Surname END DESC,
                    CASE WHEN @SortColumn = 'Email' AND @SortDirection = 1 THEN Email END ASC,
                    CASE WHEN @SortColumn = 'Email' AND @SortDirection = 0 THEN Email END DESC,
                    Id
                ) AS RowNum
    FROM dbo.Contacts
    WHERE (@SearchTerm IS NULL OR
           Name LIKE '%' + @SearchTerm + '%' OR
           Surname LIKE '%' + @SearchTerm + '%' OR
           Email LIKE '%' + @SearchTerm + '%')
)
 SELECT
     Id,
     Name,
     Surname,
     Email,
     CreatedAt,
     UpdatedAt
 FROM ContactsCTE
 WHERE RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize
 ORDER BY RowNum;
END
GO

CREATE PROCEDURE dbo.CreateContact
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(255),
    @Surname NVARCHAR(255),
    @Email NVARCHAR(100),
    @CreatedAt DATETIME2,
    @UpdatedAt DATETIME2
AS
BEGIN
INSERT INTO dbo.Contacts (Id, Name, Surname, Email, CreatedAt, UpdatedAt)
VALUES (@Id, @Name, @Surname, @Email, @CreatedAt, @UpdatedAt);
END
GO

CREATE PROCEDURE dbo.UpdateContact
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(255),
    @Surname NVARCHAR(255),
    @Email NVARCHAR(100),
    @UpdatedAt DATETIME2
AS
BEGIN
UPDATE dbo.Contacts
SET Name = @Name,
    Surname = @Surname,
    Email = @Email,
    UpdatedAt = @UpdatedAt
WHERE Id = @Id;
END
GO

CREATE PROCEDURE dbo.DeleteContact
    @Id UNIQUEIDENTIFIER
AS
BEGIN
DELETE FROM dbo.Contacts WHERE Id = @Id;
END
GO