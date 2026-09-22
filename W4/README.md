# 🎓 W4 - Advanced Student Management API

Một dự án **ASP.NET Core 8 Web API** chuyên sâu dành cho hệ thống quản lý học sinh và trường học. Dự án được thiết kế chuẩn mực theo mô hình **Clean Architecture (4 Tầng)** kết hợp **CQRS (MediatR)**, **AutoMapper**, **FluentValidation**, và hệ thống bảo mật toàn diện bằng **JWT Authentication** cùng cơ chế **Refresh Token Rotation (Xoay vòng token)** và **Phân quyền đa cấp (Role & Policy Authorization)**.

---

## 🚀 Công nghệ & Thư viện sử dụng

- **Framework:** ASP.NET Core 8.0 Web API (.NET 8)
- **Ngôn ngữ:** C# 12
- **Cơ sở dữ liệu:** SQL Server (LocalDB / SQL Server Express)
- **ORM & Data Access:** Entity Framework Core 8.0 & Dapper
- **Kiến trúc:** Clean Architecture (4 Projects) + CQRS Pattern (MediatR)
- **Object Mapping:** AutoMapper
- **Validation:** FluentValidation (Tự động validate Model)
- **Xác thực & Bảo mật:** JWT Bearer, PBKDF2 Password Hashing, Refresh Token
- **Caching:** In-Memory Cache (`Microsoft.Extensions.Caching.Memory`) & Distributed Redis Cache (`StackExchange.Redis`, `Microsoft.Extensions.Caching.StackExchangeRedis`)
- **Logging:** Serilog (`Serilog.AspNetCore`, `Serilog.Sinks.Console`, `Serilog.Sinks.File`)
- **Tracing:** Correlation ID Middleware (`X-Correlation-Id`) tích hợp `Serilog.Context.LogContext`
- **Tài liệu API:** Swagger UI (Tích hợp nút ổ khóa Bearer Authorization)

---

## 📂 Cấu trúc dự án (Mô hình Clean Architecture)

```text
📦 W4.slnx
 ┣ 📂 W4.API                   <-- TẦNG 1: PRESENTATION LAYER
 ┃ ┣ 📂 Controllers          : Nhận HTTP Requests, phân quyền [Authorize], gọi MediatR.
 ┃ ┣ 📂 Middlewares          : Bắt lỗi toàn cục (ExceptionMiddleware), ghi nhật ký (LoggingMiddleware).
 ┃ ┣ 📂 Extensions           : Đăng ký Dependency Injection (DI) tập trung.
 ┃ ┣ 📜 Program.cs           : Cấu hình Pipeline, JWT Bearer, Policy Authorization & Swagger.
 ┃ ┗ 📜 appsettings.json     : Lưu ConnectionString và cấu hình Secret Key cho JWT.
 ┃
 ┣ 📂 W4.Application           <-- TẦNG 2: BUSINESS LOGIC & CQRS LAYER
 ┃ ┣ 📂 Common               : Cấu hình hệ thống (JwtSettings).
 ┃ ┣ 📂 DTOs                 : Requests và Responses chuyển tải dữ liệu.
 ┃ ┣ 📂 Features             : Tổ chức nghiệp vụ theo chuẩn CQRS:
 ┃ ┃ ┣ 📂 Auth               : Commands cho Register, Login, RefreshToken, RevokeToken.
 ┃ ┃ ┣ 📂 Students           : Commands (Thêm, Sửa, Xóa) & Queries (Tìm kiếm, Xem chi tiết).
 ┃ ┃ ┣ 📂 Classes            : Commands & Queries quản lý lớp học.
 ┃ ┃ ┣ 📂 Subjects           : Commands & Queries quản lý môn học.
 ┃ ┃ ┗ 📂 Scores             : Commands & Queries quản lý điểm số.
 ┃ ┣ 📂 Interfaces           : Giao diện ITokenService, IPasswordHasher, IUserRepository...
 ┃ ┣ 📂 Mappings             : Profiles AutoMapper (StudentProfile, ClassProfile...).
 ┃ ┗ 📂 Validations          : Các bộ luật kiểm tra dữ liệu bằng FluentValidation.
 ┃
 ┣ 📂 W4.Infrastructure        <-- TẦNG 3: DATA ACCESS & INFRASTRUCTURE LAYER
 ┃ ┣ 📂 Data                 : ApplicationDbContext (EF Core nối SQL Server).
 ┃ ┣ 📂 Repositories         : Triển khai các Repository truy xuất DB.
 ┃ ┣ 📂 Services             : Triển khai TokenService (JWT), PasswordHasher (PBKDF2).
 ┃ ┗ 📂 Migrations           : Lịch sử tiến hóa Database (EF Core Migrations).
 ┃
 ┗ 📂 W4.Domain                <-- TẦNG 4: DOMAIN LAYER (CỐT LÕI)
   ┣ 📂 Entities             : Các thực thể trung tâm (User, RefreshToken, Student, Class, Subject, Score).
   ┗ 📂 Enums                : GenderType, RoleType...
```

---

## ⚙️ Cấu hình Hệ thống (`appsettings.json`)

Mở file `W4.API/appsettings.json` và cấu hình chuỗi kết nối Database, Redis và thông số JWT:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=StudentManagement;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False",
    "Redis": "localhost:6379"
  },
  "Jwt": {
    "Key": "ChuoiKhoaBiMatToiThieu256BitRatDaiDungDeKyChuKyDienTuHMACSHA256",
    "Issuer": "W4.API",
    "Audience": "W4.Client",
    "DurationInMinutes": 15
  }
}
```

### 🗄️ Khởi động Redis Cache (Bản Portable có sẵn):
Dự án đã tích hợp sẵn Redis Server portable trong thư mục `redis/`:
1. Mở Terminal và di chuyển vào thư mục `redis`:
   ```bash
   cd redis
   .\redis-server.exe
   ```
2. Redis sẽ khởi chạy tại cổng mặc định `localhost:6379`.
*(Lưu ý: Thư mục `redis/` và file dữ liệu `dump.rdb` đã được cấu hình `.gitignore` để tránh commit dữ liệu rác lên Git).*

### 🛠️ Các bước khởi tạo Database:
1. Mở Terminal tại thư mục `W4.API`:
   ```bash
   dotnet ef database update --project ../W4.Infrastructure --startup-project .
   ```
2. Khởi chạy ứng dụng:
   ```bash
   dotnet run --project W4.API
   ```
3. Truy cập Swagger UI: **`https://localhost:62182/swagger`** *(hoặc `http://localhost:62183/swagger`)*.

---

## 🔐 Hệ thống Xác thực & Phân quyền (Auth & Authorization)

### 1. Cơ chế hoạt động
- **Bảo vệ Mật khẩu:** Sử dụng thuật toán **PBKDF2 (SHA-256)** với 10,000 vòng lặp kết hợp 16 bytes muối ngẫu nhiên (`Salt`). So khớp mật khẩu bằng `CryptographicOperations.FixedTimeEquals` chống tấn công đo thời gian (Timing Attack).
- **Access Token:** Định dạng JWT có chữ ký điện tử HMAC-SHA256, thời gian sống ngắn (**15 phút**), mang theo các Claims (`sub`, `unique_name`, `role`, `jti`).
- **Refresh Token Rotation:** Chuỗi ngẫu nhiên 64-bytes lưu trong Database (hạn **7 ngày**). Khi đổi token mới, token cũ bị hủy ngay lập tức (`IsUsed = true`) và cấp phát cặp token mới để chống đánh cắp token (Replay Attack).
- **Thu hồi (Revoke / Logout):** Đổi cờ `IsRevoked = true` để vô hiệu hóa phiên làm việc.

---

### 2. Bảng Ma trận Phân quyền toàn hệ thống

Toàn bộ hệ thống được phân quyền nghiêm ngặt dựa trên **Roles** và **Policies**:

| Phân hệ | Endpoint | Method | Phân quyền áp dụng | Quyền hạn thực tế |
| :--- | :--- | :---: | :--- | :--- |
| **Auth** | `/api/auth/*` | POST | `[AllowAnonymous]` | 🌐 Mọi người (Kể cả khách chưa đăng nhập) |
| **Sinh viên** | `/api/student` | POST | `[Authorize(Policy = "AdminOnly")]` | 👑 **Chỉ duy nhất Admin** mới được thêm |
| | `/api/student/{id}` | DELETE | `[Authorize(Policy = "AdminOnly")]` | 👑 **Chỉ duy nhất Admin** mới được xóa |
| | `/api/student/{id}` | PUT | `[Authorize(Policy = "CanManageStudents")]` | 👑 **Admin** & 👨‍🏫 **Teacher** được cập nhật |
| | `/api/student/search` | GET | `[Authorize(Policy = "CanManageStudents")]` | 👑 **Admin** & 👨‍🏫 **Teacher** được tìm kiếm |
| | `/api/student/{id}` | GET | `[Authorize(Policy = "CanManageStudents")]` | 👑 **Admin** & 👨‍🏫 **Teacher** được xem chi tiết |
| | `/api/student/class/{id}`| GET | `[Authorize(Policy = "CanManageStudents")]` | 👑 **Admin** & 👨‍🏫 **Teacher** xem theo lớp |
| **Lớp học** | `/api/class` | GET | `[Authorize]` | 👥 Mọi tài khoản đăng nhập đều xem được |
| | `/api/class/{id}` | GET | `[Authorize]` | 👥 Mọi tài khoản đăng nhập đều xem được |
| | `/api/class` | POST/PUT/DELETE | `[Authorize(Policy = "AdminOnly")]` | 👑 **Chỉ Admin** được Thêm/Sửa/Xóa lớp |
| **Môn học** | `/api/subject` | GET | `[Authorize]` | 👥 Mọi tài khoản đăng nhập đều xem được |
| | `/api/subject` | POST/PUT/DELETE | `[Authorize(Policy = "AdminOnly")]` | 👑 **Chỉ Admin** được Thêm/Sửa/Xóa môn |
| **Điểm số** | `/api/score/{id}` | GET | `[Authorize]` | 👥 Mọi tài khoản đăng nhập đều xem được |
| | `/api/score` | POST/PUT | `[Authorize(Policy = "CanManageStudents")]` | 👑 **Admin** & 👨‍🏫 **Teacher** (Nhập/Sửa điểm) |
| | `/api/score/{id}` | DELETE | `[Authorize(Policy = "AdminOnly")]` | 👑 **Chỉ Admin** (Xóa cột điểm) |

---

## 🧪 Chi tiết các API Endpoints

### 🔑 1. Nhóm Xác thực (`/api/auth`)

#### a. Đăng ký tài khoản: `POST /api/auth/register`
* **Request Body:**
  ```json
  {
    "username": "admin_hieu",
    "password": "Password123@",
    "email": "admin@school.edu.vn",
    "role": "Admin"
  }
  ```
  *(Role hỗ trợ: `"Admin"`, `"Teacher"`, `"User"`)*

#### b. Đăng nhập: `POST /api/auth/login`
* **Request Body:**
  ```json
  {
    "username": "admin_hieu",
    "password": "Password123@"
  }
  ```
* **Response (200 OK):**
  ```json
  {
    "success": true,
    "message": "Đăng nhập thành công!",
    "data": {
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
      "refreshToken": "k7Jq8mZ1N9...rX4vP0L2w==",
      "username": "admin_hieu",
      "role": "Admin"
    }
  }
  ```

#### c. Làm mới Token: `POST /api/auth/refresh-token`
* **Request Body:**
  ```json
  {
    "accessToken": "chuỗi_access_token_vừa_hết_hạn",
    "refreshToken": "k7Jq8mZ1N9...rX4vP0L2w=="
  }
  ```

#### d. Thu hồi Token (Đăng xuất): `POST /api/auth/revoke-token`
* **Request Body:**
  ```json
  {
    "refreshToken": "k7Jq8mZ1N9...rX4vP0L2w=="
  }
  ```

---

### 👨‍🎓 2. Nhóm Sinh viên (`/api/student`)
- **`GET /api/student/search?keyword=Hieu&pageNumber=1&pageSize=10`**: Tìm kiếm và phân trang sinh viên.
- **`GET /api/student/{id}`**: Xem chi tiết 1 sinh viên.
- **`GET /api/student/class/{classId}`**: Xem danh sách sinh viên theo lớp.
- **`POST /api/student`**: Thêm sinh viên mới *(Chỉ Admin)*.
  ```json
  {
    "name": "Nguyễn Minh Hiếu",
    "dateOfBirth": "2002-05-20T00:00:00Z",
    "gender": 1,
    "classId": "GUID_HOAC_MA_LOP"
  }
  ```
- **`PUT /api/student/{id}`**: Sửa thông tin sinh viên *(Admin hoặc Teacher)*.
- **`DELETE /api/student/{id}`**: Xóa sinh viên *(Chỉ Admin)*.

---

### 🏫 3. Nhóm Lớp học (`/api/class`)
- **`GET /api/class`**: Danh sách tất cả lớp học.
- **`GET /api/class/{id}`**: Chi tiết lớp học.
- **`POST /api/class`**: Tạo lớp mới *(Admin)*.
  ```json
  {
    "name": "12A1",
    "homeRoomTeacher": "Thầy Nguyễn Văn A"
  }
  ```
- **`PUT /api/class/{id}`**: Sửa tên/giáo viên chủ nhiệm *(Admin)*.
- **`DELETE /api/class/{id}`**: Xóa lớp học *(Admin)*.

---

### 📚 4. Nhóm Môn học (`/api/subject`)
- **`GET /api/subject`**: Danh sách môn học.
- **`POST /api/subject`**: Tạo môn học mới *(Admin)*.
  ```json
  {
    "name": "Lập trình C# Nâng cao",
    "credits": 3
  }
  ```
- **`PUT /api/subject/{id}`**: Sửa thông tin môn *(Admin)*.
- **`DELETE /api/subject/{id}`**: Xóa môn *(Admin)*.

---

### 📝 5. Nhóm Điểm số (`/api/score`)
- **`GET /api/score/{id}`**: Xem điểm số theo ID.
- **`POST /api/score`**: Chấm điểm cho sinh viên *(Admin hoặc Teacher)*.
  ```json
  {
    "studentId": "GUID_SINH_VIEN",
    "subjectId": "GUID_MON_HOC",
    "scoreValue": 9.5
  }
  ```
- **`PUT /api/score/{id}`**: Sửa điểm đã chấm *(Admin hoặc Teacher)*.
- **`DELETE /api/score/{id}`**: Xóa điểm *(Chỉ Admin)*.

---

## ⚡ Hệ thống Caching 2 Tầng (In-Memory & Distributed Redis Cache)

Dự án áp dụng chiến lược Caching hiện đại nhằm tối ưu hóa tốc độ phản hồi và giảm tải áp lực truy vấn cho Database:

### 1. In-Memory Cache (`IMemoryCache`) - Áp dụng cho Danh mục Môn học (`/api/subject`)
- **Đặc tính:** Dữ liệu môn học có tần suất đọc lớn nhưng ít biến động.
- **Vị trí lưu trữ:** Trực tiếp trong bộ nhớ RAM của tiến trình Web API.
- **Chính sách hết hạn (TTL):**
  - `AbsoluteExpirationRelativeToNow`: 5 phút (hết hạn cứng).
  - `SlidingExpiration`: 2 phút (tự động gia hạn thêm nếu có truy vấn liên tục).
- **Cơ chế Eviction:** Khi thực hiện Thêm/Sửa/Xóa môn học (`CreateSubjectCommand`, `UpdateSubjectCommand`, `DeleteSubjectCommand`), hệ thống tự động xóa cache `CacheKeys.SubjectAll` để đảm bảo dữ liệu mới nhất được cập nhật.

### 2. Distributed Cache (`IDistributedCache` + Redis) - Áp dụng cho Lớp học (`/api/class`)
- **Đặc tính:** Lưu trữ tập trung trên Redis Server (cổng `6379`), cho phép chia sẻ cache giữa nhiều instance API khi mở rộng (Scaling).
- **Key Naming Convention:**
  - Danh sách toàn bộ lớp học: `w4:classes:all`
  - Chi tiết lớp theo ID: `w4:classes:{id}`
- **Extension thông minh:** [`DistributedCacheExtensions.cs`](file:///c:/NGUYENMINHHIEU/Workspace/C%23/CS/W4/W4.Application/Common/DistributedCacheExtensions.cs) hỗ trợ các phương thức generic `GetRecordAsync<T>`, `SetRecordAsync<T>`, `RemoveRecordAsync` tự động serialize/deserialize JSON.
- **Cache Resiliency & Graceful Degradation (Khả năng chịu lỗi cao cấp):**
  - Cấu hình Timeout kết nối tối đa 1 giây (`ConnectTimeout = 1000ms`, `AbortOnConnectFail = false`).
  - Tự động bọc phòng vệ `try-catch`: Nếu server Redis bị tắt hoặc mất mạng, hệ thống **không bao giờ bị lỗi 500**, mà tự động bỏ qua cache và **fallback truy vấn thẳng xuống Database** chỉ trong 1 giây, bảo vệ tối đa trải nghiệm của người dùng.

---

## 📝 Hệ thống Logging chuyên nghiệp với Serilog

Hệ thống thay thế hoàn toàn logging mặc định của .NET bằng **Serilog** chuẩn Enterprise:

### 1. Ghi log đa kênh (Sinks)
- **Console Sink:** Định dạng ngắn gọn, màu sắc trực quan phục vụ debug trực tiếp trên terminal.
- **Daily Rolling File Sink:**
  - Tự động tạo file log mới mỗi ngày tại thư mục `log/app-YYYYMMDD.txt`.
  - Giữ lại lịch sử trong 30 ngày gần nhất (`retainedFileCountLimit: 30`), các file cũ hơn tự động được xóa để tiết kiệm dung lượng ổ cứng.

### 2. Request Logging Tinh gọn (`app.UseSerilogRequestLogging()`)
- Gom toàn bộ thông tin vòng đời của HTTP Request thành **1 dòng duy nhất**:
  ```text
  [INF] [W4.API] [Development] [af03dba7-...] HTTP GET /api/class responded 200 in 35.4 ms
  ```
- Tự động lọc bỏ các log rác nội bộ của Microsoft và các request chuyển hướng HTTP 307.

### 3. Log Enrichers & Metadata
Mọi dòng log đều được tự động đóng dấu các siêu dữ liệu quan trọng:
- `[Application]`: Tên ứng dụng (`W4.API`).
- `[Environment]`: Môi trường triển khai (`Development`, `Production`).
- `[CorrelationId]`: Mã định danh truy vết của riêng từng Request.

---

## 🔍 Cơ chế Truy vết Lỗi (Correlation ID & Error Tracing)

Hỗ trợ đội ngũ vận hành và lập trình viên truy vết và chẩn đoán sự cố trong vài giây:

1. **`CorrelationIdMiddleware`:**
   - Đứng ở vị trí đầu tiên trong HTTP Pipeline.
   - Kiểm tra Header `X-Correlation-Id` từ Client gửi lên (nếu có thì tái sử dụng, nếu không có thì tự sinh chuỗi `Guid` duy nhất).
   - Đẩy mã này vào `LogContext` của Serilog xuyên suốt luồng xử lý và đính kèm lại vào Response Header `X-Correlation-Id`.
2. **Chuẩn hóa Error Response (`ApiResponse<T>`):**
   - Khi có sự cố (400, 404, 500), `ExceptionMiddleware` tự động trích xuất mã truy vết và trả về trường `TraceId` trong JSON:
     ```json
     {
       "success": false,
       "message": "Đã xảy ra lỗi hệ thống. Vui lòng thử lại sau.",
       "data": null,
       "traceId": "af03dba7-0c8b-454a-b7d4-3d878d297765"
     }
     ```
   - Khách hàng chỉ cần cung cấp mã `TraceId`, lập trình viên mở file log tìm kiếm là xác định được ngay toàn bộ stack trace và ngữ cảnh lỗi!

---

## 🎯 Hướng dẫn Kiểm thử trên Swagger UI (Nút Ổ Khóa 🔓)

Dự án đã tích hợp sẵn cơ chế **OpenAPI Bearer Security** ngay trên giao diện Swagger:

1. Chạy server bằng `dotnet run --project W4.API`.
2. Mở trình duyệt vào link: `https://localhost:62182/swagger`.
3. Gọi API `POST /api/auth/login` với tài khoản của bạn để lấy chuỗi `token`.
4. Cuộn lên đầu trang, bấm vào nút màu xanh **`Authorize` 🔓** (góc trên bên phải).
5. Dán token vào ô **Value** $\rightarrow$ Bấm nút **Authorize** $\rightarrow$ Bấm **Close**.
6. Biểu tượng ổ khóa chuyển sang trạng thái đã khóa 🔒. Bây giờ bạn có thể thử nghiệm mọi API trực tiếp trên trình duyệt!

---

## 🧪 Kịch bản Test Phân quyền Thực tế

Tạo 3 tài khoản qua `POST /api/auth/register` để kiểm tra phân quyền:
- **Admin:** `admin_test` / `Admin@123` (Role: `Admin`)
- **Giáo viên:** `teacher_test` / `Teacher@123` (Role: `Teacher`)
- **Học sinh/Khách:** `user_test` / `User@123` (Role: `User`)

| Tình huống Test | Token sử dụng | Kết quả quan sát được |
| :--- | :--- | :--- |
| **Không đăng nhập** | Không gửi Token | Bị chặn ngay từ cửa với mã **`401 Unauthorized`**. |
| **User thường xóa sinh viên** | Token của `user_test` | Server biết danh tính nhưng từ chối với mã **`403 Forbidden`**. |
| **Giáo viên sửa điểm** | Token của `teacher_test` | Thành công **`200 OK`**. |
| **Giáo viên xóa sinh viên** | Token của `teacher_test` | Bị chặn với mã **`403 Forbidden`** (chỉ Admin mới được xóa). |
| **Admin thực hiện mọi thao tác** | Token của `admin_test` | Toàn quyền Thêm, Sửa, Xóa thành công **`200 OK`**. |

---

## ⚠️ Bảng giải mã Status Codes
- **`200 OK` / `201 Created`**: Thao tác thành công.
- **`400 Bad Request`**: Dữ liệu gửi lên không đúng luật (do FluentValidation chặn lại).
- **`401 Unauthorized`**: Chưa đăng nhập, token sai hoặc token hết hạn.
- **`403 Forbidden`**: Đã đăng nhập nhưng không đủ quyền thực hiện hành động.
- **`404 Not Found`**: Bản ghi cần tìm không tồn tại.
- **`500 Internal Server Error`**: Lỗi hệ thống bất ngờ (bắt qua ExceptionMiddleware).
