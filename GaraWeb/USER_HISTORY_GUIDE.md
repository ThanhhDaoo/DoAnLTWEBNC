# Hướng dẫn: Lịch sử theo User

## Đã hoàn thành

### 1. Cập nhật Database
- ✅ Thêm cột `Username` vào bảng `YEUCAU_DICHVU`
- ✅ Thêm cột `Username` vào bảng `LIENHE`

### 2. Cập nhật Models
- ✅ Thêm property `Username` vào `YeucauDichVu.cs`
- ✅ Thêm property `Username` vào `LienHe.cs`

### 3. Cập nhật Controllers
- ✅ `DichVuController`: Lưu username khi đặt dịch vụ
- ✅ `LienHeController`: Lưu username khi liên hệ
- ✅ `AccountController.MyRequests`: Filter theo username
- ✅ `AccountController.MyContacts`: Filter theo username

## Cách hoạt động

### Khi user đặt dịch vụ:
1. User đăng nhập
2. Chọn dịch vụ và điền form
3. Hệ thống tự động lưu `Username` của user vào database
4. User có thể xem lại trong "Lịch sử yêu cầu dịch vụ"

### Khi user liên hệ:
1. User đăng nhập
2. Điền form liên hệ
3. Hệ thống tự động lưu `Username` của user vào database
4. User có thể xem lại trong "Lịch sử liên hệ"

### Xem lịch sử:
- Mỗi user chỉ thấy yêu cầu/liên hệ của chính họ
- Nếu chưa có: Hiển thị "Chưa có..." với nút để đặt dịch vụ/liên hệ
- Nếu có: Hiển thị danh sách đầy đủ

## Lưu ý

### Dữ liệu cũ:
- Các yêu cầu/liên hệ trước khi cập nhật sẽ có `Username = NULL`
- Những dữ liệu này sẽ không hiển thị trong lịch sử của bất kỳ user nào
- Có thể cập nhật thủ công nếu cần

### Yêu cầu:
- User PHẢI đăng nhập mới có thể đặt dịch vụ hoặc liên hệ
- Đã thêm `[Authorize]` attribute vào các action

## Test

### Bước 1: Đăng nhập
```
Username: Thanhdao
Password: 123456
```

### Bước 2: Đặt dịch vụ
1. Vào trang Dịch vụ
2. Chọn một dịch vụ
3. Điền form và đặt dịch vụ

### Bước 3: Liên hệ
1. Vào trang Liên hệ
2. Điền form và gửi

### Bước 4: Xem lịch sử
1. Click vào avatar → "Lịch sử yêu cầu dịch vụ"
2. Click vào avatar → "Lịch sử liên hệ"
3. Chỉ thấy dữ liệu của user `Thanhdao`

## Files đã sửa

### Database:
- `add-username-columns.sql` - Script thêm cột Username

### Models:
- `GaraWeb/Models/YeucauDichVu.cs`
- `GaraWeb/Models/LienHe.cs`

### Controllers:
- `GaraWeb/Controllers/DichVuController.cs`
- `GaraWeb/Controllers/LienHeController.cs`
- `GaraWeb/Controllers/AccountController.cs`

## Kết quả

✅ Mỗi user chỉ xem được lịch sử của chính họ
✅ Không cần nhập số điện thoại
✅ Tự động filter theo username
✅ Hiển thị "Chưa có" nếu user chưa đặt dịch vụ/liên hệ
