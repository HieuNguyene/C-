USE master;
GO
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'StudentManagement')
BEGIN
    ALTER DATABASE StudentManagement SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE StudentManagement;
END
GO

CREATE DATABASE StudentManagement
COLLATE SQL_Latin1_General_CP1_CI_AS;
GO

USE StudentManagement;
GO

-- 3. Tạo bảng Lớp học (Classes)
CREATE TABLE Classes (
    Id NVARCHAR(50) PRIMARY KEY, 
    Name NVARCHAR(250) NOT NULL UNIQUE 
);
GO


CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(250) NOT NULL,
    Class_Id NVARCHAR(50) NULL, 
    CONSTRAINT FK_Students_Classes FOREIGN KEY (Class_Id)
        REFERENCES Classes(Id) ON DELETE SET NULL
);
GO

CREATE TABLE Subjects (
    Id NVARCHAR(50) PRIMARY KEY,
    Name NVARCHAR(250) NOT NULL UNIQUE 
);
GO


CREATE TABLE Marks (
    Student_Id INT NOT NULL,
    Subject_Id NVARCHAR(50) NOT NULL,
    Score DECIMAL(4,2) NOT NULL CHECK (Score BETWEEN 0 AND 10), 
    ExamDate DATE NOT NULL DEFAULT GETDATE(), 

    CONSTRAINT FK_Marks_Students FOREIGN KEY (Student_Id)
        REFERENCES Students(Id) ON DELETE CASCADE,
        
    CONSTRAINT FK_Marks_Subjects FOREIGN KEY (Subject_Id)
        REFERENCES Subjects(Id) ON DELETE CASCADE,
        
    PRIMARY KEY (Student_Id, Subject_Id)
);
GO



INSERT INTO Classes (Id, Name) VALUES 
('C01', N'Lớp CNTT K15'),
('C02', N'Lớp Điện tử K15'),
('C03', N'Lớp Kế toán K15');
GO

INSERT INTO Students (Name, Class_Id) VALUES 
(N'Nguyễn Văn An', 'C01'),
(N'Trần Thị Bình', 'C02'),
(N'Lê Văn Cường', 'C01'),
(N'Phạm Thị Diệu', 'C02'),
(N'Hoàng Văn Hải', NULL); 
GO

INSERT INTO Subjects (Id, Name) VALUES 
('SUB01', N'Cơ sở dữ liệu'),
('SUB02', N'Lập trình C#'),
('SUB03', N'Toán cao cấp');
GO

INSERT INTO Marks (Student_Id, Subject_Id, Score, ExamDate) VALUES 
(1, 'SUB01', 8.50, '2026-08-10'),
(1, 'SUB02', 10.00, '2026-08-11'),
(2, 'SUB01', 4.50, '2026-08-10'),
(2, 'SUB02', 7.25, '2026-08-11'),
(3, 'SUB01', 9.00, '2026-08-10'),
(3, 'SUB03', 3.50, '2026-08-12'),
(4, 'SUB02', 8.00, '2026-08-11'),
(4, 'SUB03', 6.00, '2026-08-12');
GO


SELECT * FROM Students
WHERE Name LIKE N'Nguyễn%' OR Name LIKE N'%Hải%';


SELECT * FROM Marks
WHERE Score BETWEEN 5.0 AND 9.0;


SELECT * FROM Students
WHERE Class_Id IS NULL;

SELECT * FROM Subjects
ORDER BY Name DESC;



SELECT 
    s.Id AS StudentId,
    s.Name AS StudentName,
    c.Name AS ClassName
FROM Students s
LEFT JOIN Classes c ON s.Class_Id = c.Id;
GO

SELECT 
    s.Name AS StudentName,
    sub.Name AS SubjectName,
    m.Score AS Score,
    m.ExamDate
FROM Marks m
INNER JOIN Students s ON m.Student_Id = s.Id
INNER JOIN Subjects sub ON m.Subject_Id = sub.Id
ORDER BY s.Name ASC, m.Score DESC;
GO

SELECT 
    c.Id AS ClassId,
    c.Name AS ClassName,
    COUNT(s.Id) AS TotalStudents
FROM Classes c
LEFT JOIN Students s ON c.Id = s.Class_Id
GROUP BY c.Id, c.Name;
GO

SELECT 
    s.Id AS StudentId,
    s.Name AS StudentName,
    AVG(m.Score) AS AverageMark,
    COUNT(m.Subject_Id) AS ExaminedSubjects
FROM Students s
INNER JOIN Marks m ON s.Id = m.Student_Id
GROUP BY s.Id, s.Name
HAVING AVG(m.Score) >= 6.0;
GO

IF EXISTS (SELECT * FROM sys.views WHERE name = 'v_StudentReport')
    DROP VIEW v_StudentReport;
GO

CREATE VIEW v_StudentReport AS
SELECT 
    s.Id AS StudentId,
    s.Name AS StudentName,
    c.Name AS ClassName,
    COUNT(m.Subject_Id) AS TotalSubjects,
    AVG(m.Score) AS GPA,
    CASE 
        WHEN AVG(m.Score) >= 8.0 THEN N'Giỏi'
        WHEN AVG(m.Score) >= 6.5 THEN N'Khá'
        WHEN AVG(m.Score) >= 5.0 THEN N'Trung bình'
        ELSE N'Yếu'
    END AS Classification
FROM Students s
LEFT JOIN Classes c ON s.Class_Id = c.Id
LEFT JOIN Marks m ON s.Id = m.Student_Id
GROUP BY s.Id, s.Name, c.Name;
GO


SELECT * FROM v_StudentReport
ORDER BY GPA DESC;
GO
