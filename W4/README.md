# 🎓 W4 - Advanced Student Management API

Một dự án **ASP.NET Core Web API** chuyên sâu dành cho quản lý học sinh, được xây dựng theo chuẩn **Clean Architecture** (Kiến trúc sạch) ở cấp độ doanh nghiệp. 

Dự án áp dụng các pattern và nguyên tắc thiết kế hiện đại như **Repository & Service Pattern**, **Dependency Injection**, **FluentValidation**, và quản lý ngoại lệ tập trung (Global Exception Handling).

---

## 🚀 Công nghệ & Thư viện sử dụng

- **Framework:** ASP.NET Core 8 Web API
- **Ngôn ngữ:** C#
- **Database ORM:** Entity Framework Core (EF Core 8) / SQL Server
- **Xác thực dữ liệu:** FluentValidation
- **Tài liệu API:** Swagger / OpenAPI
- **Patterns:** Dependency Injection (DI), Repository Pattern, Service Pattern
- **Kiến trúc:** Clean Architecture (4-Layer Solution)

---

## 📂 Cấu trúc dự án (Clean Architecture)

Dự án được chia thành 4 Project (`.csproj`) riêng biệt trong một Solution (`W4.slnx`) để đảm bảo tính module hóa và bóc tách hoàn toàn logic nghiệp vụ khỏi các yếu tố kỹ thuật (Database, API Framework):

```text
📦 W4 (MyECommerce.API Style)
 ┣ 📂 W4.API                   <-- TẦNG 1: GIAO DIỆN & CẤU HÌNH (Presentation Layer)
 ┃ ┣ 📂 Controllers          (Nhận Request, trả Response qua HTTP)
 ┃ ┣ 📂 Middlewares          (Pipeline xử lý lỗi, logging)
 ┃ ┣ 📂 Extensions           (Đăng ký Dependency Injection)
 ┃ ┣ 📜 Program.cs           (Entry point, cấu hình hệ thống)
 ┃ ┗ 📜 appsettings.json     (Chuỗi kết nối Database)
 ┃
 ┣ 📂 W4.Application           <-- TẦNG 2: NGHIỆP VỤ (Business Logic Layer)
 ┃ ┣ 📂 DTOs                 (Các object truyền nhận dữ liệu: Requests/Responses)
 ┃ ┣ 📂 Validations          (Kiểm tra tính hợp lệ dữ liệu bằng FluentValidation)
 ┃ ┣ 📂 Interfaces           (Định nghĩa Interface cho các Service)
 ┃ ┗ 📂 Implementations      (Triển khai chi tiết logic của Service)
 ┃
 ┣ 📂 W4.Infrastructure        <-- TẦNG 3: HẠ TẦNG & TRUY CẬP DỮ LIỆU (Data Access Layer)
 ┃ ┣ 📂 Data                 (Chứa ApplicationDbContext của EF Core)
 ┃ ┣ 📂 Repositories         (Triển khai pattern Repository để giao tiếp SQL)
 ┃ ┃ ┣ 📂 Interfaces         (Các Interface của Repository)
 ┃ ┃ ┗ 📂 Implementations    (Code gọi tới Database)
 ┃ ┗ 📂 Migrations           (Lịch sử thay đổi Schema CSDL)
 ┃
 ┗ 📂 W4.Domain                <-- TẦNG 4: THỰC THỂ CỐT LÕI (Domain Layer)
   ┣ 📂 Entities             (Các class tương đương với Table trong DB: Class, Student, Score...)
   ┗ 📂 Enums                (Các kiểu liệt kê dùng chung: GenderType...)
```

### 🔄 Luồng phụ thuộc (Dependency Rules)
Trong Clean Architecture, các project chỉ được phép tham chiếu theo chiều hướng vào tâm (vào tầng Domain):
- **Domain** không phụ thuộc vào bất kỳ project nào.
- **Application** phụ thuộc vào **Domain**.
- **Infrastructure** phụ thuộc vào **Domain**.
- **API** phụ thuộc vào **Application** và **Infrastructure**.

---

## 🛠 Cách Cài đặt và Chạy dự án

1. Mở Terminal / PowerShell tại thư mục gốc của dự án.
2. Build solution để khôi phục (restore) các packages:
   ```bash
   dotnet build W4.slnx
   ```
3. Đảm bảo cấu hình chuỗi kết nối (`DefaultConnection`) trong `W4.API/appsettings.json` trỏ đúng vào SQL Server của bạn.
4. Chạy dự án (mặc định sẽ chạy tầng API):
   ```bash
   dotnet run --project W4.API/W4.API.csproj
   ```
5. Mở trình duyệt và truy cập `https://localhost:<port>/swagger` để kiểm thử các Endpoints qua giao diện Swagger UI.

---

## 💡 Điểm nổi bật trong kiến trúc

- **Controller cực mỏng (Fat Service, Thin Controller):** Controller chỉ làm nhiệm vụ nhận Request, đẩy xuống Service xử lý và trả về Response.
- **Che giấu Entity (DTO Pattern):** Người dùng API không bao giờ nhìn thấy hoặc thao tác trực tiếp với các Entity (như `Student`, `Class`). Mọi giao tiếp đều thông qua các lớp trung gian (DTOs như `StudentResponse`, `CreateStudentRequest`).
- **Xác thực dữ liệu tách biệt:** Việc kiểm tra `Name`, `DateOfBirth`, `Score`... không nằm trong Controller mà được ủy quyền hoàn toàn cho `FluentValidation` nằm tại tầng Application, đảm bảo code Controller vô cùng sạch sẽ.
- **Repository Pattern:** Toàn bộ code Entity Framework Core (`.FirstOrDefault()`, `.Where()`, `.Include()`) bị nhốt hoàn toàn ở tầng Infrastructure. Tầng nghiệp vụ (Application) chỉ gọi các hàm trừu tượng qua Interface.

---
> Code được cấu trúc lại mô phỏng theo mô hình Clean Architecture chuyên nghiệp nhằm chuẩn bị cho việc mở rộng dự án lớn trong tương lai.
