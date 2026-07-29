# 🎓 Student Management API

Một dự án **ASP.NET Core Web API** được xây dựng nhằm thực hành các kiến thức nền tảng của RESTful API.

## 🚀 Công nghệ sử dụng

- ASP.NET Core Web API
- C#
- Swagger / OpenAPI
- LINQ
- Dependency Injection (DI)

---

# 📂 Cấu trúc dự án

```
W3
│
├── Controllers
│   └── StudentsController.cs
│
├── DTOs
│   ├── Requests
│   │   ├── CreateStudentRequest.cs
│   │   ├── UpdateStudentRequest.cs
│   │   └── StudentQueryRequest.cs
│   │
│   └── Responses
│       └── StudentResponse.cs
│
├── Interfaces
│   └── IStudentService.cs
│
├── Models
│   └── Student.cs
│
├── Responses
│   └── ApiResponse.cs
│
├── Services
│   └── StudentService.cs
│
├── Program.cs
└── appsettings.json
```

---

# ✨ Chức năng

- Lấy danh sách sinh viên
- Lấy sinh viên theo ID
- Thêm sinh viên mới
- Cập nhật thông tin sinh viên
- Xóa sinh viên
- Tìm kiếm theo từ khóa
- Phân trang dữ liệu

---

# 📌 REST API

| Method | Endpoint | Chức năng |
|---------|----------|-----------|
| GET | `/api/students` | Lấy danh sách sinh viên |
| GET | `/api/students/{id}` | Lấy sinh viên theo ID |
| POST | `/api/students` | Thêm sinh viên |
| PUT | `/api/students/{id}` | Cập nhật sinh viên |
| DELETE | `/api/students/{id}` | Xóa sinh viên |

---

# 🔍 Search

Tìm kiếm theo tên sinh viên.

### Request

```http
GET /api/students?keyword=an
```

---

# 📄 Pagination

Lấy dữ liệu theo trang.

### Request

```http
GET /api/students?page=1&pageSize=5
```

---

# 🔎 Search + Pagination

Kết hợp tìm kiếm và phân trang.

### Request

```http
GET /api/students?keyword=an&page=1&pageSize=5
```

---

# 📦 Response mẫu

```json
{
  "success": true,
  "message": "Success",
  "data": [
    {
      "id": "c4d0b6f2-ef0f-4a4f-a91f-9d1b7f8d4b8d",
      "name": "Nguyen Van A"
    }
  ]
}
```

---

# ▶️ Hướng dẫn chạy dự án

## Clone repository

```bash
git clone <repository-url>
```

## Di chuyển vào thư mục dự án

```bash
cd W3
```

## Restore package

```bash
dotnet restore
```

## Chạy ứng dụng

```bash
dotnet run
```

---

# 📖 Swagger

Sau khi chạy thành công, truy cập:

```
https://localhost:<port>/swagger
```

Ví dụ:

```
https://localhost:7090/swagger
```

---

# 🧠 Kiến thức đã áp dụng

- RESTful API Convention
- CRUD API
- DTO Pattern
- Service Layer
- Dependency Injection
- Generic ApiResponse<T>
- LINQ (Where, Select, Skip, Take)
- Pagination
- Keyword Search
- Swagger/OpenAPI

---