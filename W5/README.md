# Student Management Database

Dự án này chứa các kịch bản (scripts) SQL Server để xây dựng và quản lý cơ sở dữ liệu cho một hệ thống **Quản lý Sinh viên (Student Management)**. Đây có thể là phần Backend/Database được sử dụng để tích hợp với ứng dụng C# (WinForms/WPF/ASP.NET).

## 📂 Cấu trúc các file

- `Student.sql`: Khởi tạo Database (`StudentManagement`), tạo bảng, khóa chính, khóa ngoại, chèn dữ liệu mẫu (Sample Data) và tạo View thống kê.
- `stored_procedures.sql`: Chứa các Stored Procedures (Thủ tục lưu trữ) hỗ trợ CRUD (Thêm, Đọc, Sửa, Xóa), thống kê và phân trang dữ liệu.
- `backup_restore.sql`: Chứa kịch bản mẫu để sao lưu (Backup) và phục hồi (Restore).

## 🗄️ Cấu trúc Cơ sở dữ liệu

1. **`Classes` (Lớp học)**: `Id` (PK), `Name`
2. **`Students` (Sinh viên)**: `Id` (PK), `Name`, `Class_Id` (FK)
3. **`Subjects` (Môn học)**: `Id` (PK), `Name`
4. **`Marks` (Điểm thi)**: `Student_Id` (FK), `Subject_Id` (FK), `Score`, `ExamDate`

---

## 🚀 Hướng dẫn Cài đặt & Khởi chạy

Để thiết lập database trên máy của bạn, thực hiện tuần tự các bước sau:

**Bước 1: Khởi tạo Database và Dữ liệu mẫu**
1. Mở **SQL Server Management Studio (SSMS)** và kết nối vào Server của bạn.
2. Mở file `Student.sql`.
3. Bấm **Execute** (hoặc nhấn `F5`).
   *Kết quả mong đợi: Thông báo "Commands completed successfully". Database `StudentManagement` được tạo cùng với các bảng và dữ liệu mẫu.*

**Bước 2: Khởi tạo các Stored Procedures**
1. Mở file `stored_procedures.sql`.
2. Bấm **Execute** (hoặc nhấn `F5`).
   *Kết quả mong đợi: Các thủ tục lưu trữ (sp_GetAllStudents, sp_CreateStudent,...) sẽ được đưa vào database.*

---

## 🧪 Hướng dẫn Chạy thử (Testing)

Sau khi cài đặt xong, bạn có thể mở một cửa sổ truy vấn mới (New Query) trong SSMS, đảm bảo đang trỏ vào DB `StudentManagement` bằng lệnh `USE StudentManagement;` và chạy các lệnh dưới đây để test:

### 1. Test Dữ liệu mẫu (View & Select)
Kiểm tra xem dữ liệu sinh viên và điểm số đã được tạo đúng chưa:
```sql
-- Lấy danh sách toàn bộ sinh viên
SELECT * FROM Students;

-- Xem báo cáo tổng hợp điểm số (dựa trên View)
SELECT * FROM v_StudentReport ORDER BY GPA DESC;
```

### 2. Test các Stored Procedures (CRUD)

**Test Thêm mới sinh viên:**
```sql
DECLARE @NewId INT;
EXEC sp_CreateStudent 
    @Name = N'Vũ Trọng Phụng', 
    @ClassId = 'C01', 
    @NewStudentId = @NewId OUTPUT;

SELECT @NewId AS 'ID Sinh viên mới vừa tạo';
```

**Test Lấy sinh viên theo ID:**
```sql
-- Chú ý: Thay số 6 bằng ID sinh viên mới tạo ở bước trên nếu khác
EXEC sp_GetStudentById @Id = 6; 
```

**Test Phân trang (Paging):**
```sql
-- Lấy trang số 1, mỗi trang có 2 sinh viên
DECLARE @Total INT;
EXEC sp_GetStudentsWithPaging 
    @PageIndex = 1, 
    @PageSize = 2, 
    @TotalRecords = @Total OUTPUT;

SELECT @Total AS 'Tổng số sinh viên trong DB';
```

---

## 💻 Tích hợp với C# 

Chuỗi kết nối (Connection String) mẫu để dùng trong ADO.NET hoặc Entity Framework:

```xml
<!-- Dùng Windows Authentication -->
Server=YOUR_SERVER_NAME;Database=StudentManagement;Trusted_Connection=True;

<!-- Dùng SQL Server Authentication -->
Server=YOUR_SERVER_NAME;Database=StudentManagement;User Id=sa;Password=your_password;
```
*(Nếu làm tính năng Restore DB trong C#, hãy nhớ trỏ DB vào `master` thay vì `StudentManagement`)*
