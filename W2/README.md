# Week 2 - Thực hành C# Console Application

## Giới thiệu

Đây là project được thực hiện trong tuần học thứ 2 nhằm củng cố các kiến thức nền tảng của ngôn ngữ C# và lập trình hướng đối tượng (OOP). Dự án xây dựng một chương trình **Quản lý sinh viên** chạy trên Console, đồng thời áp dụng các kỹ thuật như Interface, LINQ và Async/Await.

---

## Mục tiêu

- Hiểu và áp dụng Interface trong thiết kế chương trình.
- Xây dựng các chức năng CRUD cho đối tượng Student.
- Thực hành xử lý dữ liệu với LINQ.
- Làm quen với lập trình bất đồng bộ bằng `async/await`.
- Rèn luyện kỹ năng tổ chức mã nguồn theo mô hình Service.

---

## Công nghệ sử dụng

- C#
- .NET 8
- Console Application
- LINQ
- Async/Await
- Git & GitHub

---

## Cấu trúc thư mục

```
Week2
│
├── D1
│   ├── model
│   │     Student.cs
│   │
│   └── service
│         StudentService.cs
│         StatisticService.cs
│
├── D2
│   └── interface
│         IStudentService.cs
│         IStatisticService.cs
│
├── Program.cs
└── Week2.csproj
```

---

## Chức năng

### Quản lý sinh viên

- Thêm sinh viên
- Hiển thị danh sách sinh viên
- Tìm kiếm sinh viên theo mã
- Cập nhật thông tin sinh viên
- Xóa sinh viên
- Kiểm tra dữ liệu đầu vào

---

### Thống kê

- Đếm tổng số sinh viên
- Tìm sinh viên lớn tuổi nhất
- Tìm sinh viên nhỏ tuổi nhất
- Tính tuổi trung bình
- Sắp xếp sinh viên theo tên
- Nhóm sinh viên theo chữ cái đầu
- Tìm các sinh viên trùng tên

---

## LINQ được sử dụng

Trong project có sử dụng các phương thức LINQ như:

- Where()
- Select()
- OrderBy()
- OrderByDescending()
- GroupBy()
- FirstOrDefault()
- Count()
- Average()
- Max()
- Min()
- Any()
- All()

---

## Async/Await

Một số chức năng CRUD được mô phỏng truy cập cơ sở dữ liệu bằng:

```csharp
await Task.Delay(2000);
```

Mục đích là giúp làm quen với lập trình bất đồng bộ trước khi chuyển sang làm việc với SQL Server và Entity Framework Core.

---

## Kiến thức áp dụng

- Class và Object
- Encapsulation
- Interface
- Service Pattern
- Collections
- LINQ
- Async/Await
- Validate dữ liệu đầu vào
- Xử lý ngoại lệ

---

## Cách chạy chương trình

### Clone project

```bash
git clone https://github.com/HieuNguyene/C-.git
```

### Mở project

Mở bằng **Visual Studio 2022**.

### Chạy chương trình

```
Ctrl + F5
```

hoặc

```
F5
```

---