# 🎓 W4 - Advanced Student Management API

Một dự án **ASP.NET Core Web API** chuyên sâu được xây dựng theo chuẩn kiến trúc doanh nghiệp. Dự án áp dụng các pattern và nguyên tắc thiết kế hiện đại như **3-Tier Architecture (Repository & Service Pattern)**, **Domain-Driven Design (DDD)**, và quản lý lỗi tập trung.

---

## 🚀 Công nghệ & Thư viện sử dụng

- **Khung ứng dụng:** ASP.NET Core 8 Web API (hoặc phiên bản .NET mới nhất)
- **Ngôn ngữ:** C#
- **Database ORM:** Entity Framework Core (EF Core)
- **Xác thực dữ liệu:** FluentValidation
- **Tài liệu API:** Swagger / OpenAPI
- **Patterns:** Dependency Injection (DI), Repository Pattern, Service Pattern
- **Kiến trúc:** 3-Tier Architecture

---

## 📂 Kiến trúc dự án (3-Tier)

Dự án được chia thành các tầng rõ ràng để tối ưu hóa việc bảo trì và mở rộng:

1. **Controllers (Tầng API):** Tiếp nhận HTTP Request, điều hướng tới Service và trả về HTTP Response.
2. **Services (Tầng Nghiệp vụ):** Nơi chứa toàn bộ *Business Logic*, kiểm tra tính hợp lệ trước khi thao tác dữ liệu.
3. **Repositories (Tầng Dữ liệu):** Nơi trực tiếp giao tiếp với Database thông qua EF Core.
4. **Middlewares:** Chặn và xử lý Request/Response (Ghi Log, Bắt lỗi tập trung).

```text
W4
│
├── ControllersBase/      # Chứa các API Controllers (ClassController, StudentController...)
├── DTOs/                 # Các Data Transfer Objects (Requests/Responses) dùng để giao tiếp
├── FluentValidation/     # Chứa các rules kiểm tra dữ liệu đầu vào (CreateClassValidator...)
├── Interface/            # Định nghĩa các Interfaces cho Service và Repository
├── middleware/           # Middlewares tùy chỉnh (LoggingMiddleware, ExceptionMiddleware)
├── model/                # Domain Models / Entities (Student, Class, Score, Subject)
├── Repository/           # Triển khai truy xuất Database (ClassRepository, StudentRepository...)
├── Service/              # Triển khai Business Logic (ClassService, StudentService...)
├── Responses/            # Chuẩn hóa format trả về (ApiResponse<T>)
└── Program.cs            # Cấu hình DI, DbContext, Middlewares
```

---

## ✨ Các tính năng nổi bật

- **Quản lý Sinh viên (Student):** Thêm, Sửa, Xóa, Lấy chi tiết, Tìm kiếm theo từ khóa (Keyword) & Phân trang (Pagination).
- **Quản lý Lớp học (Class):** Thêm, Sửa, Xóa, Quản lý sinh viên trong lớp.
- **Quản lý Điểm số (Score):** 
  - Khóa chính độc lập, ràng buộc chặt chẽ với Sinh viên và Môn học.
  - Áp dụng các quy tắc như: Không cho phép nhập 2 điểm cho cùng 1 môn học.
- **Xử lý lỗi tập trung (Global Exception Handling):** 
  - Sử dụng `ExceptionMiddleware` để tự động bọc lỗi thành JSON.
  - Tự động map `KeyNotFoundException` thành lỗi HTTP 404, `ValidationException` thành HTTP 400.
- **Custom Logging:** 
  - Ghi log thời gian thực thi (Execution Time) và phương thức (Method, Path) qua `LoggingMiddleware`.
- **Bảo vệ dữ liệu (Encapsulation):**
  - Các Entity (Models) được thiết kế theo tư duy DDD, giấu kín (private set) các thuộc tính và cung cấp các hàm cập nhật riêng biệt như `UpdateClassName()`, `AddStudent()`.

---

## 📌 Bảng tham chiếu API (Endpoints)

| Đối tượng | Method | Endpoint | Chức năng |
|---------|----------|-----------|-----------|
| **Student** | GET | `/api/students/search?Keyword=an&Page=1&PageSize=10` | Tìm kiếm & Phân trang |
| **Student** | GET | `/api/students/{id}` | Lấy chi tiết |
| **Student** | POST | `/api/students` | Thêm mới |
| **Student** | PUT | `/api/students/{id}` | Cập nhật |
| **Student** | DELETE | `/api/students/{id}` | Xóa |
| **Class** | GET | `/api/classes` | Lấy danh sách lớp |
| **Class** | POST | `/api/classes` | Tạo lớp mới |
| **Class** | PUT | `/api/classes?classId={id}` | Sửa thông tin lớp |
| **Class** | DELETE | `/api/classes?classId={id}` | Xóa lớp |

---

## 📦 Chuẩn dữ liệu trả về (ApiResponse)

Mọi API đều trả về cùng một định dạng JSON thống nhất giúp Frontend dễ dàng xử lý:

**Khi Thành Công:**
```json
{
  "success": true,
  "message": "Đã thành công tạo lớp học C01",
  "data": {
    "classId": "C01",
    "className": "Lop 10A"
  }
}
```

**Khi Thất Bại (Ví dụ Lỗi Validation 400):**
```json
{
  "success": false,
  "message": "Dữ liệu không hợp lệ",
  "data": [
    {
      "propertyName": "ClassId",
      "errorMessage": "Mã lớp không được để trống!"
    }
  ]
}
```

---

## ▶️ Hướng dẫn chạy dự án

1. **Clone repository:**
   ```bash
   git clone <repository-url>
   ```

2. **Cài đặt dependencies / Restore package:**
   ```bash
   cd W4
   dotnet restore
   ```

3. **Cập nhật Database (Entity Framework Core):**
   *Đảm bảo bạn đã config chuỗi kết nối (Connection String) trong `appsettings.json`*
   ```bash
   dotnet ef database update
   ```

4. **Chạy ứng dụng:**
   ```bash
   dotnet run
   ```

5. **Truy cập Swagger (Giao diện test API):**
   Mở trình duyệt và truy cập: `http://localhost:5069/swagger` (hoặc port tương ứng hiển thị trên console).

---

## 🧠 Kiến thức & Best Practices đã áp dụng
- **SOLID Principles:** Tách biệt Interface và Implementation.
- **Guard Clauses:** Kiểm tra tham số kỹ lưỡng ở đầu mỗi hàm Service.
- **RESTful API Anti-pattern Avoidance:** Không trả về HTTP 200 OK khi có lỗi nghiệp vụ.
- **Domain-Driven Design (DDD):** Sử dụng các Aggregate Root, chặn thay đổi dữ liệu tùy tiện bằng `private set`.
- **Validation Pipeline:** Dùng FluentValidation cắt giảm hoàn toàn các câu lệnh IF kiểm tra dữ liệu ở Controller.