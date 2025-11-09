# Hệ thống Quản lý Gara Ô tô

## Mô tả dự án
Hệ thống quản lý gara ô tô được xây dựng bằng ASP.NET Core Web API và MVC, bao gồm các chức năng quản lý khách hàng, xe, dịch vụ, sản phẩm và hóa đơn.

## Cấu trúc dự án
- **GaraAPI**: Web API cung cấp các endpoint RESTful
- **GaraMVC**: Giao diện web MVC gọi API
- **GaRa.sql**: Script tạo database và dữ liệu mẫu

## Yêu cầu hệ thống
- .NET 8.0 SDK
- SQL Server (LocalDB hoặc SQL Server Express)
- Visual Studio 2022 hoặc VS Code

## Cài đặt và chạy dự án

### 1. Tạo Database
1. Mở SQL Server Management Studio hoặc Azure Data Studio
2. Chạy script `GaRa.sql` để tạo database và dữ liệu mẫu
3. Kiểm tra connection string trong `GaraAPI/appsettings.json`

### 2. Chạy API
```bash
cd GaraAPI
dotnet restore
dotnet run
```
API sẽ chạy tại: `https://localhost:7000`

### 3. Chạy MVC
```bash
cd GaraMVC
dotnet restore
dotnet run
```
MVC sẽ chạy tại: `https://localhost:7001`

## Cấu trúc Database

### Các bảng chính:
- **USERS**: Quản lý người dùng và phân quyền
- **KHACHHANG**: Thông tin khách hàng
- **XE**: Thông tin xe của khách hàng
- **DICHVU**: Danh sách dịch vụ gara
- **SANPHAM**: Danh sách sản phẩm/phụ tùng
- **HOADON**: Hóa đơn dịch vụ
- **CHITIET_HDDV**: Chi tiết dịch vụ trong hóa đơn
- **CHITIET_HDSP**: Chi tiết sản phẩm trong hóa đơn

## API Endpoints

### Khách hàng
- `GET /api/KhachHang` - Lấy danh sách khách hàng
- `GET /api/KhachHang/{id}` - Lấy thông tin khách hàng
- `POST /api/KhachHang` - Tạo khách hàng mới
- `PUT /api/KhachHang/{id}` - Cập nhật khách hàng
- `DELETE /api/KhachHang/{id}` - Xóa khách hàng

### Xe
- `GET /api/Xe` - Lấy danh sách xe
- `GET /api/Xe/{id}` - Lấy thông tin xe
- `GET /api/Xe/ByKhachHang/{maKH}` - Lấy xe theo khách hàng
- `POST /api/Xe` - Thêm xe mới
- `PUT /api/Xe/{id}` - Cập nhật xe
- `DELETE /api/Xe/{id}` - Xóa xe

### Dịch vụ
- `GET /api/DichVu` - Lấy danh sách dịch vụ
- `GET /api/DichVu/Active` - Lấy dịch vụ đang hoạt động
- `POST /api/DichVu` - Thêm dịch vụ mới
- `PUT /api/DichVu/{id}` - Cập nhật dịch vụ
- `DELETE /api/DichVu/{id}` - Xóa dịch vụ

### Sản phẩm
- `GET /api/SanPham` - Lấy danh sách sản phẩm
- `GET /api/SanPham/InStock` - Lấy sản phẩm còn tồn
- `POST /api/SanPham` - Thêm sản phẩm mới
- `PUT /api/SanPham/{id}` - Cập nhật sản phẩm
- `DELETE /api/SanPham/{id}` - Xóa sản phẩm

### Hóa đơn
- `GET /api/HoaDon` - Lấy danh sách hóa đơn
- `GET /api/HoaDon/{id}` - Lấy chi tiết hóa đơn
- `POST /api/HoaDon` - Tạo hóa đơn mới
- `PUT /api/HoaDon/{id}/Complete` - Hoàn thành hóa đơn
- `DELETE /api/HoaDon/{id}` - Xóa hóa đơn

### Thống kê
- `GET /api/ThongKe/TongQuan` - Tổng quan hệ thống
- `GET /api/ThongKe/DoanhThuTheoThang` - Doanh thu theo tháng
- `GET /api/ThongKe/TopDichVu` - Top dịch vụ bán chạy
- `GET /api/ThongKe/TopSanPham` - Top sản phẩm bán chạy
- `GET /api/ThongKe/HoaDonGanDay` - Hóa đơn gần đây

## Tính năng chính

### 1. Quản lý Khách hàng
- Thêm, sửa, xóa thông tin khách hàng
- Quản lý xe của khách hàng
- Tìm kiếm khách hàng theo số điện thoại

### 2. Quản lý Dịch vụ
- Quản lý danh sách dịch vụ gara
- Thiết lập giá dịch vụ
- Bật/tắt dịch vụ

### 3. Quản lý Sản phẩm
- Quản lý kho phụ tùng
- Theo dõi số lượng tồn kho
- Cập nhật giá sản phẩm

### 4. Quản lý Hóa đơn
- Tạo hóa đơn dịch vụ
- Thêm dịch vụ và sản phẩm vào hóa đơn
- Tính toán tổng tiền tự động
- Quản lý trạng thái hóa đơn

### 5. Báo cáo Thống kê
- Tổng quan hệ thống
- Doanh thu theo tháng
- Top dịch vụ và sản phẩm bán chạy
- Danh sách hóa đơn gần đây

## Tài khoản mặc định
- **Admin**: username: `admin`, password: `123456`
- **Customer**: username: `customer1`, password: `123456`

## Lưu ý
- Đảm bảo SQL Server đang chạy trước khi khởi động ứng dụng
- Kiểm tra connection string trong appsettings.json
- API và MVC cần chạy đồng thời để hoạt động đầy đủ
- CORS đã được cấu hình để cho phép MVC gọi API

## Công nghệ sử dụng
- **Backend**: ASP.NET Core 8.0 Web API
- **Frontend**: ASP.NET Core 8.0 MVC
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **UI**: Bootstrap 5, jQuery

