
BACKUP DATABASE StudentManagement
TO DISK = 'C:\Backup\StudentManagement.bak'
WITH 
    FORMAT, -- Ghi đè lên các bản backup cũ nếu có cùng tên file
    MEDIANAME = 'SQLServerBackups',
    NAME = 'Full Backup of StudentManagement';
GO



USE master;
GO

-- Chuyển DB sang chế độ 1 người dùng và ngắt các kết nối đang mở
ALTER DATABASE StudentManagement 
SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

-- Thực hiện Restore (Ghi đè - REPLACE)
RESTORE DATABASE StudentManagement
FROM DISK = 'C:\Backup\StudentManagement.bak'
WITH REPLACE;
GO

-- Khôi phục lại chế độ nhiều người dùng sau khi Restore xong
ALTER DATABASE StudentManagement 
SET MULTI_USER;
GO
