

USE StudentManagement;
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllStudents')
    DROP PROCEDURE sp_GetAllStudents;
GO

CREATE PROCEDURE sp_GetAllStudents
AS
BEGIN
    -- Chỉ ra cho SQL Server bỏ qua việc đếm số dòng bị ảnh hưởng để tối ưu hiệu năng
    SET NOCOUNT ON; 

    SELECT 
        s.Id AS StudentId,
        s.Name AS StudentName,
        c.Name AS ClassName
    FROM StudentS s
    LEFT JOIN Classes c ON s.Class_Id = c.Id
    ORDER BY s.Id;
END;
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetStudentsByClass')
    DROP PROCEDURE sp_GetStudentsByClass;
GO

CREATE PROCEDURE sp_GetStudentsByClass
    @ClassId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        s.Id AS StudentId,
        s.Name AS StudentName,
        s.Class_Id AS ClassId
    FROM StudentS s
    WHERE s.Class_Id = @ClassId;
END;
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CountStudentsByClass')
    DROP PROCEDURE sp_CountStudentsByClass;
GO

CREATE PROCEDURE sp_CountStudentsByClass
    @ClassId NVARCHAR(50),        
    @TotalStudents INT OUTPUT      
AS
BEGIN
    SET NOCOUNT ON;

   
    SELECT @TotalStudents = COUNT(*) 
    FROM StudentS
    WHERE Class_Id = @ClassId;
END;
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CreateStudent')
    DROP PROCEDURE sp_CreateStudent;
GO

CREATE PROCEDURE sp_CreateStudent
    @Name NVARCHAR(250),
    @ClassId NVARCHAR(50),
    @NewStudentId INT OUTPUT 
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO StudentS (Name, Class_Id)
    VALUES (@Name, @ClassId);

    SET @NewStudentId = SCOPE_IDENTITY();
END;
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetStudentById')
    DROP PROCEDURE sp_GetStudentById;
GO

CREATE PROCEDURE sp_GetStudentById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        s.Id AS StudentId,
        s.Name AS StudentName,
        s.Class_Id AS ClassId,
        c.Name AS ClassName
    FROM StudentS s
    LEFT JOIN Classes c ON s.Class_Id = c.Id
    WHERE s.Id = @Id;
END;
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateStudent')
    DROP PROCEDURE sp_UpdateStudent;
GO

CREATE PROCEDURE sp_UpdateStudent
    @Id INT,
    @Name NVARCHAR(250),
    @ClassId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE StudentS
    SET Name = @Name,
        Class_Id = @ClassId
    WHERE Id = @Id;
END;
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteStudent')
    DROP PROCEDURE sp_DeleteStudent;
GO

CREATE PROCEDURE sp_DeleteStudent
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM StudentS
    WHERE Id = @Id;
END;
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetStudentsWithPaging')
    DROP PROCEDURE sp_GetStudentsWithPaging;
GO

CREATE PROCEDURE sp_GetStudentsWithPaging
    @PageIndex INT = 1,           
    @PageSize INT = 10,            
    @TotalRecords INT OUTPUT        
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @TotalRecords = COUNT(*) FROM StudentS;

    DECLARE @Offset INT;
    SET @Offset = (@PageIndex - 1) * @PageSize;

    SELECT 
        s.Id AS StudentId,
        s.Name AS StudentName,
        c.Name AS ClassName
    FROM StudentS s
    LEFT JOIN Classes c ON s.Class_Id = c.Id
    ORDER BY s.Id ASC -- Luôn cần ORDER BY để phân trang chính xác
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;
GO

