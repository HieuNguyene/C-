CREATE DATABASE StudentManagement;
GO

USE StudentManagement;
GO

CREATE TABLE StudentS(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(250),
    Class NVARCHAR(250)
);
GO

CREATE TABLE Classes(
    Id NVARCHAR(250) PRIMARY KEY,
    Name NVARCHAR(250)
);
GO

CREATE TABLE Subjects(
    Id NVARCHAR(250) PRIMARY KEY,
    Name NVARCHAR(250)
);

CREATE TABLE Mark(
    Student_Id INT NOT NULL,
    Subjects_Id NVARCHAR NOT NULL,
    Mark DECIMAL(3,2) NOT NULL CHECK (Mark BETWEEN 0 AND 10),

    CONSTRAINT FK_Marks_Students FOREIGN KEY( Student_Id)
        REFERENCES StudentS(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Marks_Subjects FOREIGN KEY(Subjects_Id)
        REFERENCES Subjects(Id) ON DELETE CASCADE,
    PRIMARY KEY(Student_Id,Subjects_Id) -- Khóa phức hợp để đảm bảo mỗi một học viên chỉ có một dòng điểm cho mỗi môn
)
GO

