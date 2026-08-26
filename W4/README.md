# 🎓 W4 - Advanced Student Management API

Một dự án **ASP.NET Core Web API** chuyên sâu được xây dựng theo chuẩn kiến trúc doanh nghiệp. Dự án đã được tái cấu trúc sang **N-Tier (Layered Architecture)** với các Class Library riêng biệt, áp dụng các pattern và nguyên tắc thiết kế hiện đại như **Repository & Service Pattern**, **Domain-Driven Design (DDD)**, và quản lý lỗi tập trung.

---

## 🚀 Công nghệ & Thư viện sử dụng

- **Khung ứng dụng:** ASP.NET Core 8 Web API
- **Ngôn ngữ:** C#
- **Database ORM:** Entity Framework Core (EF Core)
- **Xác thực dữ liệu:** FluentValidation
- **Tài liệu API:** Swagger / OpenAPI
- **Patterns:** Dependency Injection (DI), Repository Pattern, Service Pattern
- **Kiến trúc:** Layered Architecture (N-Tier Multi-Project Solution)

---

## 📂 Kiến trúc dự án (Multi-Project Layered Architecture)

Dự án được chia thành 6 Project (Class Library) riêng biệt để quản lý Dependency chặt chẽ, tối ưu hóa việc bảo trì và đáp ứng chính xác yêu cầu bóc tách kiến trúc:

```text
W4 Solution
│
├── W4.Model (Domain Layer)
│   ├── Entities/         # Domain Models (Student, Class, Score, Subject)
│   └── Enums/            # Các kiểu liệt kê (GenderType...)
│
├── W4.Context (Infrastructure Layer)
│   ├── Data/             # ApplicationDbContext
│   └── Migrations/       # EF Core Migrations
│
├── W4.Repository (Data Access Layer)
│   ├── Interfaces/       # IStudentRepository, IClassRepository...
│   └── Implementations/  # Triển khai truy xuất Database bằng EF Core
│
├── W4.Service (Business / Application Layer)
│   ├── Interfaces/       # IStudentService, IClassService...
│   ├── Implementations/  # Triển khai Business Logic, kiểm tra tính hợp lệ
│   └── DTOs/             # Data Transfer Objects (Requests/Responses)
│
├── W4.Common (Cross-Cutting Concerns)
│   └── Responses/        # Chuẩn hóa format trả về (ApiResponse<T>)
│
└── W4 (API Layer / Presentation Layer)
    ├── ControllersBase/  # Chứa các API Controllers
    ├── FluentValidation/ # Validation rules (CreateClassValidator...)
    ├── middleware/       # Middlewares (LoggingMiddleware, ExceptionMiddleware)
    └── Program.cs        # Cấu hình DI, DbContext, Middlewares
```

### 🔁 Luồng Dependency (Sự phụ thuộc)
Kiến trúc này tuân thủ nguyên tắc Dependency của Layered Architecture:
- **W4 (Api)** gọi -> `W4.Service`, `W4.Common`, `W4.Context`, `W4.Model`
- **W4.Service** gọi -> `W4.Repository`, `W4.Model`, `W4.Common`
- **W4.Repository** gọi -> `W4.Context`, `W4.Model`, `W4.Common`
- **W4.Context** gọi -> `W4.Model`
- **W4.Model & W4.Common** -> Đứng độc lập *(Không phụ thuộc vào project nào)*

Quy tắc này đảm bảo tầng logic nghiệp vụ (`Service`) và giao diện (`Api`) không bị dính chặt vào công nghệ truy xuất dữ liệu bên dưới. Cả Service và Repository đều giao tiếp thông qua **Interfaces** để tối ưu cho Dependency Injection và Unit Testing.

---

## ✨ Các tính năng nổi bật

- **Kiến trúc đa tầng (N-Tier):** Ràng buộc phụ thuộc nghiêm ngặt qua các project C# riêng biệt.
- **Quản lý Sinh viên (Student):** Thêm, Sửa, Xóa, Lấy chi tiết, Tìm kiếm theo từ khóa (Keyword) & Phân trang (Pagination).
- **Quản lý Lớp học (Class):** Thêm, Sửa, Xóa, Quản lý sinh viên trong lớp.
- **Quản lý Môn học (Subject):** Quản lý danh mục các môn học trong nhà trường.
- **Quản lý Điểm số (Score):** 
  - Khóa chính độc lập, ràng buộc chặt chẽ với Sinh viên và Môn học.
  - Áp dụng các quy tắc như: Không cho phép nhập 2 điểm cho cùng 1 môn học của 1 sinh viên.
- **Xử lý lỗi tập trung (Global Exception Handling):** 
  - Sử dụng `ExceptionMiddleware` để tự động bọc lỗi thành JSON.
  - Tự động map `KeyNotFoundException` thành lỗi HTTP 404, `ValidationException` thành HTTP 400.
- **Custom Logging:** 
  - Ghi log thời gian thực thi (Execution Time) và phương thức (Method, Path) qua `LoggingMiddleware`.
- **Bảo vệ dữ liệu (Encapsulation):**
  - Các Entity (Models) được thiết kế theo tư duy DDD, giấu kín (`private set`) các thuộc tính và cung cấp các hàm cập nhật an toàn.

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
| **Subject** | GET | `/api/subjects` | Lấy danh sách môn học |
| **Subject** | POST | `/api/subjects` | Thêm môn học mới |
| **Subject** | PUT | `/api/subjects/{id}` | Đổi tên môn học |
| **Subject** | DELETE | `/api/subjects/{id}` | Xóa môn học |
| **Score** | GET | `/api/scores/student/{studentId}` | Xem bảng điểm sinh viên |
| **Score** | POST | `/api/scores` | Nhập điểm mới |
| **Score** | PUT | `/api/scores/{scoreId}` | Cập nhật điểm |
| **Score** | DELETE | `/api/scores/{scoreId}` | Xóa điểm |

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
   dotnet restore W4.slnx
   ```

3. **Cập nhật Database (Entity Framework Core):**
   *Đảm bảo bạn đã config chuỗi kết nối (Connection String) trong `appsettings.json`*
   ```bash
   dotnet ef database update --project W4.Context --startup-project W4
   ```

4. **Chạy ứng dụng:**
   ```bash
   dotnet run --project W4
   ```

5. **Truy cập Swagger (Giao diện test API):**
   Mở trình duyệt và truy cập: `http://localhost:5069/swagger` (hoặc port tương ứng hiển thị trên console).

---

## 🧠 Kiến thức & Best Practices đã áp dụng
- **SOLID Principles:** Tách biệt Interface và Implementation (Sử dụng DI).
- **Layered Architecture:** Đóng gói độc lập các lớp bằng các Project C# (Class Library).
- **Guard Clauses:** Kiểm tra tham số kỹ lưỡng ở đầu mỗi hàm Service.
- **RESTful API Anti-pattern Avoidance:** Không trả về HTTP 200 OK khi có lỗi nghiệp vụ.
- **Validation Pipeline:** Dùng FluentValidation cắt giảm hoàn toàn các câu lệnh IF kiểm tra dữ liệu ở Controller.
