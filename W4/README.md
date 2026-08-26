# 🎓 W4 - Advanced Student Management API

Một dự án **ASP.NET Core Web API** chuyên sâu dành cho quản lý học sinh. Dự án này được thiết kế dựa trên mô hình **Clean Architecture (4 Tầng)**, bóc tách hoàn toàn logic nghiệp vụ (Business Logic) khỏi tầng giao diện (API) và tầng truy cập dữ liệu (Database).

Dự án áp dụng các pattern và nguyên tắc thiết kế hiện đại như **Repository Pattern**, **Service Pattern**, **Dependency Injection**, **FluentValidation**, và **Global Exception Handling**.

---

## 🚀 Công nghệ & Thư viện sử dụng

- **Framework:** ASP.NET Core 8 Web API
- **Ngôn ngữ:** C# 12
- **Cơ sở dữ liệu (Database):** SQL Server
- **ORM:** Entity Framework Core 8
- **Xác thực dữ liệu (Validation):** FluentValidation
- **Tài liệu API (API Documentation):** Swagger / OpenAPI
- **Kiến trúc:** Clean Architecture (4-Layer Solution)

---

## 📂 Chi tiết cấu trúc dự án (Mô hình 4 Tầng)

Dự án được chia làm 4 dự án nhỏ (`.csproj`) giúp cách ly hoàn toàn các chức năng, đảm bảo mã nguồn dễ bảo trì, dễ thay thế và dễ test.

```text
📦 W4.slnx
 ┣ 📂 W4.API                   <-- TẦNG 1: GIAO DIỆN & CẤU HÌNH (Presentation Layer)
 ┃ ┣ 📂 Controllers          : Nhận HTTP Requests từ Client, kiểm tra và gọi Service, sau đó trả về HTTP Responses.
 ┃ ┣ 📂 Middlewares          : Xử lý ngoại lệ toàn cầu (Bắt lỗi tự động và trả về JSON chuẩn).
 ┃ ┣ 📂 Extensions           : Chứa cấu hình Dependency Injection (DI).
 ┃ ┣ 📜 Program.cs           : Entry point - Nơi khởi chạy ứng dụng.
 ┃ ┗ 📜 appsettings.json     : Lưu trữ chuỗi kết nối Database.
 ┃
 ┣ 📂 W4.Application           <-- TẦNG 2: NGHIỆP VỤ (Business Logic Layer)
 ┃ ┣ 📂 DTOs                 : (Data Transfer Objects) Đóng gói dữ liệu gửi/nhận (Requests/Responses).
 ┃ ┣ 📂 Validations          : Luật kiểm tra dữ liệu bằng FluentValidation (VD: Tên không rỗng).
 ┃ ┣ 📂 Interfaces           : Giao diện cho Service (VD: IStudentService).
 ┃ ┗ 📂 Implementations      : Triển khai chi tiết logic nghiệp vụ.
 ┃
 ┣ 📂 W4.Infrastructure        <-- TẦNG 3: HẠ TẦNG & TRUY CẬP DỮ LIỆU (Data Access Layer)
 ┃ ┣ 📂 Data                 : Chứa `ApplicationDbContext` (Cấu hình EF Core).
 ┃ ┣ 📂 Repositories         : Nơi duy nhất chứa các câu lệnh truy vấn dữ liệu (LINQ, EF Core).
 ┃ ┃ ┣ 📂 Interfaces         : Giao diện Repository (VD: IStudentRepository).
 ┃ ┃ ┗ 📂 Implementations    : Triển khai gọi Database.
 ┃ ┗ 📂 Migrations           : Lịch sử thay đổi Schema CSDL.
 ┃
 ┗ 📂 W4.Domain                <-- TẦNG 4: THỰC THỂ CỐT LÕI (Domain Layer)
   ┣ 📂 Entities             : Các class đại diện cho bảng CSDL (Student, Class...).
   ┗ 📂 Enums                : Các tập hợp hằng số (VD: GenderType).
```

---

## 🔄 Cách thức hoạt động (Luồng xử lý dữ liệu)

Mọi thao tác của người dùng (ví dụ: **Thêm học sinh mới**) đều đi qua một luồng chặt chẽ theo chiều từ ngoài vào trong:

1. **Client** gửi yêu cầu POST `/api/students` kèm dữ liệu JSON.
2. **Controller** nhận Request. Dữ liệu đi qua **FluentValidation** để kiểm tra tính hợp lệ. 
3. Nếu hợp lệ, Controller gọi **StudentService** (Application) và truyền DTO vào.
4. **Service** xử lý nghiệp vụ, chuyển DTO thành Entity `Student`, sau đó gọi **StudentRepository**.
5. **Repository** dùng EF Core tạo lệnh `INSERT` và lưu xuống SQL Server.
6. Dữ liệu lưu thành công, Repository trả Entity về cho Service. Service chuyển thành DTO Response.
7. **Controller** gói dữ liệu vào `ApiResponse` và gửi HTTP `201 Created` cho Client.

---

## 🛠 Hướng dẫn Cài đặt & Sử dụng

### 1. Yêu cầu hệ thống
- Tải và cài đặt **.NET 8 SDK**.
- Tải và cài đặt **SQL Server** (hoặc dùng SQL Server Express / LocalDB).

### 2. Thiết lập cơ sở dữ liệu
1. Mở file `W4.API/appsettings.json` và cập nhật mục `DefaultConnection` sao cho trỏ đúng vào SQL Server của bạn.
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=TÊN_SERVER_CỦA_BẠN;Database=W4_StudentDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
   }
   ```
2. Mở Terminal tại thư mục `W4.API` và chạy lệnh tạo Database:
   ```bash
   dotnet ef database update
   ```
3. (Tùy chọn): Bạn có thể chạy script `seed.sql` trong SQL Server Management Studio (SSMS) để nạp sẵn dữ liệu mẫu.

### 3. Chạy ứng dụng
Mở Terminal tại thư mục gốc và gõ lệnh:
```bash
dotnet run --project W4.API/W4.API.csproj
```
Khi ứng dụng khởi chạy thành công, đường link `https://localhost:<port>` sẽ xuất hiện.

---

## 🧪 Hướng dẫn Test API (Swagger / Postman)

Dưới đây là danh sách đầy đủ các **Endpoints** của dự án. Bạn có thể sử dụng giao diện **Swagger** tích hợp sẵn tại `https://localhost:<port>/swagger` hoặc tạo mới Collection trong **Postman** để test.

### 🏫 1. Class API (Quản lý Lớp học)
- **`GET /api/Class`**: Lấy danh sách tất cả các lớp.
- **`GET /api/Class/{id}`**: Lấy thông tin chi tiết một lớp bằng ID.
- **`POST /api/Class`**: Tạo lớp học mới.
  - **Body (JSON):**
    ```json
    {
      "name": "12A1",
      "homeRoomTeacher": "Nguyễn Văn A"
    }
    ```
- **`PUT /api/Class/{id}`**: Sửa thông tin lớp học.
- **`DELETE /api/Class/{id}`**: Xóa một lớp học.

### 👨‍🎓 2. Student API (Quản lý Học sinh)
- **`GET /api/Student/search`**: Tìm kiếm và phân trang học sinh.
  - **Params:** `keyword` (Tên học sinh), `pageNumber` (Trang số mấy), `pageSize` (Bao nhiêu dòng 1 trang).
- **`GET /api/Student/{id}`**: Xem chi tiết 1 học sinh bằng ID.
- **`POST /api/Student`**: Thêm một học sinh mới.
  - **Body (JSON):**
    ```json
    {
      "name": "Trần Thị B",
      "dateOfBirth": "2005-08-15T00:00:00Z",
      "gender": 2, 
      "classId": "DÁN_ID_LỚP_HỌC_VÀO_ĐÂY"
    }
    ```
    *(Enum Gender: 1=Nam, 2=Nữ, 3=Khác. `classId` có thể bỏ null)*
- **`PUT /api/Student/{id}`**: Sửa thông tin học sinh.
- **`DELETE /api/Student/{id}`**: Xóa học sinh khỏi hệ thống.

### 📚 3. Subject API (Quản lý Môn học)
- **`GET /api/Subject`**: Xem danh sách môn học.
- **`GET /api/Subject/{id}`**: Lấy thông tin 1 môn học.
- **`POST /api/Subject`**: Tạo môn học mới.
  - **Body (JSON):**
    ```json
    {
      "name": "Toán Học",
      "credits": 3
    }
    ```
- **`PUT /api/Subject/{id}`**: Cập nhật thông tin môn học.
- **`DELETE /api/Subject/{id}`**: Xóa môn học.

### 📝 4. Score API (Quản lý Điểm số)
- **`GET /api/Score/student/{studentId}`**: Lấy toàn bộ bảng điểm của 1 học sinh cụ thể.
- **`POST /api/Score`**: Chấm điểm môn học cho một học sinh.
  - **Body (JSON):**
    ```json
    {
      "studentId": "DÁN_ID_HỌC_SINH_VÀO_ĐÂY",
      "subjectId": "DÁN_ID_MÔN_HỌC_VÀO_ĐÂY",
      "scoreValue": 9.5
    }
    ```
- **`PUT /api/Score/{id}`**: Sửa lại điểm số đã chấm.
  - **Body (JSON):**
    ```json
    {
      "scoreValue": 10
    }
    ```
- **`DELETE /api/Score/{id}`**: Xóa một cột điểm.

### ⚠️ Một số mã lỗi HTTP (Status Codes) thường gặp:
- **`200 OK` / `201 Created`**: Gọi API thành công.
- **`400 Bad Request`**: Dữ liệu gửi lên sai định dạng (VD: bỏ trống tên, tuổi âm...). Lỗi này do FluentValidation bắt.
- **`404 Not Found`**: ID truy vấn không tồn tại.
- **`500 Internal Server Error`**: Lỗi hệ thống bất ngờ.

---

## 💡 Tại sao lại dùng Clean Architecture?

- **Dễ bảo trì:** Mỗi tầng làm đúng một việc (Single Responsibility). Nếu muốn đổi Database từ SQL Server sang MySQL, bạn chỉ cần sửa ở tầng Infrastructure mà không làm ảnh hưởng tầng Application hay API.
- **Bảo mật:** Tránh bộc lộ cấu trúc Database ra ngoài Internet nhờ kỹ thuật "Che giấu Entity" bằng các DTOs.
- **An toàn:** Controller mỏng dính giúp tránh lỗi. Toàn bộ tính toán nguy hiểm đều nằm ở tầng Application, dễ dàng bắt lỗi tập trung.
