# 📦 Product Management API

## 📖 Giới thiệu
**Product Management API** là một dự án ứng dụng Web API RESTful được xây dựng trên nền tảng **.NET 8 (ASP.NET Core)**. Dự án cung cấp các điểm cuối (endpoints) để quản lý danh sách sản phẩm, áp dụng kiến trúc đa tầng (N-Tier Architecture) giúp chia tách trách nhiệm rõ ràng, dễ bảo trì và mở rộng.

## 🚀 Các tính năng nổi bật
* **Quản lý sản phẩm (CRUD):** Hỗ trợ đầy đủ các thao tác Thêm, Đọc, Cập nhật thông tin sản phẩm.
* **Phân trang & Tìm kiếm (Pagination & Search):** Tích hợp tính năng phân trang (`PagedResult`) và tìm kiếm sản phẩm theo từ khóa thông qua `ProductSearchRequest`.
* **Tự động Xác thực Dữ liệu (Auto Validation):** Sử dụng thư viện `FluentValidation` để tự động kiểm tra tính hợp lệ của dữ liệu đầu vào (Create, Update, Search) trước khi đi vào hệ thống.
* **Xử lý Lỗi Toàn cục (Global Exception Handling):** Sử dụng `ExceptionMiddleware` để chủ động bắt và trả về định dạng lỗi chuẩn hóa khi hệ thống gặp sự cố.
* **Ghi Log Hệ thống:** Tích hợp `LoggingMiddleware` để theo dõi luồng yêu cầu (Request) và phản hồi (Response).
* **Kho lưu trữ giả lập (Mock Database):** Sử dụng `MockProductRepository` giúp khởi chạy và kiểm thử API ngay lập tức mà không cần cài đặt SQL Server.
* **Tài liệu API (Swagger/OpenAPI):** Tích hợp sẵn `Swashbuckle.AspNetCore` giúp xem tài liệu và gửi thử các HTTP Request trực tiếp trên giao diện trình duyệt.

## 🛠 Công nghệ sử dụng
* **Ngôn ngữ:** C#
* **Framework:** .NET 8.0 / ASP.NET Core Web API
* **Thư viện bên thứ ba:**
  * `FluentValidation.AspNetCore` (Kiểm tra dữ liệu)
  * `Swashbuckle.AspNetCore` (Giao diện Swagger)

## 📂 Cấu trúc thư mục dự án

Dự án được tổ chức theo cấu trúc tiêu chuẩn như sau:

* `ControllersBase/`: Chứa các API Controllers (VD: `ProductController.cs`) đóng vai trò tiếp nhận HTTP Request từ người dùng.
* `DTOs/` (Data Transfer Objects): Các đối tượng dùng để giao tiếp dữ liệu giữa Client và Server.
  * `Request/`: Chứa các class nhận dữ liệu vào (`ProductCreateRequest`, `ProductSearchRequest`, `ProductUpdateRequest`).
  * `Response/`: Chứa các class trả dữ liệu ra (`ProductResponse`).
  * `PagedResult.cs`: Class dùng để đóng gói dữ liệu phân trang.
* `Entity/`: Chứa các đối tượng thực thể đại diện cho cấu trúc bảng trong CSDL (`Product.cs`).
* `Middleware/`: Chứa các đoạn mã can thiệp vào Request pipeline (`ExceptionMiddleware.cs`, `LoggingMiddleware.cs`).
* `Repository/`: Chịu trách nhiệm tương tác trực tiếp với kho lưu trữ dữ liệu (`IProductRepository`, `MockProductRepository`).
* `Service/`: Chứa toàn bộ logic và nghiệp vụ cốt lõi của ứng dụng (`IProductService`, `ProductService`).
* `Validation/`: Chứa các quy tắc (Rules) thiết lập cho thư viện FluentValidation (`ProductCreateRequestValidator`, v.v.).

## ⚙️ Hướng dẫn cài đặt và chạy dự án

### 1. Yêu cầu hệ thống
* Cài đặt **.NET 8.0 SDK** trở lên.
* IDE: Visual Studio 2022, Visual Studio Code, hoặc JetBrains Rider.

### 2. Khởi chạy dự án
Mở Terminal/Command Prompt tại thư mục chứa file `ProductManagment.csproj` và chạy lệnh sau:

```bash
# Phục hồi các thư viện NuGet (Bao gồm FluentValidation, Swashbuckle)
dotnet restore

# Biên dịch và chạy dự án
dotnet run