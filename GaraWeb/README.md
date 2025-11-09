# GaraWeb - Trang web cho người dùng cuối

Đây là trang web dành cho khách hàng của Gara Ô Tô ABC, cho phép người dùng xem dịch vụ, sản phẩm và đặt hàng trực tuyến.

## Tính năng chính

### 🏠 Trang chủ
- Giới thiệu về gara
- Hiển thị dịch vụ nổi bật
- Hiển thị sản phẩm nổi bật
- Call-to-action liên hệ

### 🔧 Dịch vụ
- Xem danh sách tất cả dịch vụ
- Chi tiết từng dịch vụ
- Đặt dịch vụ trực tuyến
- Form liên hệ đặt dịch vụ

### 🛒 Sản phẩm
- Xem danh sách sản phẩm
- Chi tiết sản phẩm
- Thêm vào giỏ hàng
- Quản lý giỏ hàng
- Cập nhật số lượng
- Xóa sản phẩm khỏi giỏ

### 📞 Liên hệ
- Form liên hệ
- Thông tin gara
- Giờ làm việc
- Địa chỉ và liên hệ

## Công nghệ sử dụng

- **Backend**: ASP.NET Core MVC
- **Frontend**: Bootstrap 5, jQuery, Font Awesome
- **API**: Kết nối với GaraAPI
- **Session**: Quản lý giỏ hàng

## Cấu hình

### API Settings
Cấu hình URL API trong `appsettings.json`:
```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7001/api"
  }
}
```

### Chạy dự án
```bash
cd GaraWeb
dotnet run
```

Truy cập: https://localhost:5001

## Cấu trúc dự án

```
GaraWeb/
├── Controllers/          # Controllers
│   ├── HomeController.cs
│   ├── DichVuController.cs
│   ├── SanPhamController.cs
│   └── LienHeController.cs
├── Models/              # Models
│   ├── DichVu.cs
│   ├── SanPham.cs
│   ├── GioHangItem.cs
│   └── ThongTinLienHe.cs
├── Services/            # Services
│   ├── IApiService.cs
│   └── ApiService.cs
├── Views/               # Views
│   ├── Home/
│   ├── DichVu/
│   ├── SanPham/
│   └── LienHe/
└── wwwroot/            # Static files
```

## Tính năng giỏ hàng

- Lưu trữ trong Session
- Thêm/sửa/xóa sản phẩm
- Cập nhật số lượng
- Tính tổng tiền
- Hiển thị số lượng trên header

## Responsive Design

- Mobile-first approach
- Bootstrap 5 responsive grid
- Touch-friendly interface
- Optimized for all devices
