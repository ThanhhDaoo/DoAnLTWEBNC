-- =============================================
-- DỮ LIỆU TEST CHO GIAO DIỆN ADMIN - GaraMVC
-- Chạy sau khi đã chạy GaRa.sql
-- =============================================

USE GaraOToManagement;
GO

-- =============================================
-- 1. THÊM USERS (Admin và Customer)
-- Password: 123456 (SHA256 hash)
-- =============================================
INSERT INTO USERS (Username, Password, Email, Role, IsActive) VALUES
('admin2', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'admin2@gara.com', 'Admin', 1),
('nhanvien1', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'nv1@gara.com', 'Admin', 1),
('khach3', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'khach3@gmail.com', 'Customer', 1),
('khach4', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'khach4@gmail.com', 'Customer', 1),
('khach5', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'khach5@gmail.com', 'Customer', 1),
('khach6', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'khach6@gmail.com', 'Customer', 1),
('khach7', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'khach7@gmail.com', 'Customer', 1),
('khach8', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'khach8@gmail.com', 'Customer', 0); -- Tài khoản bị khóa

-- =============================================
-- 2. THÊM KHÁCH HÀNG
-- =============================================
INSERT INTO KHACHHANG (TenKH, SDT, DiaChi, Email, UserId) VALUES
(N'Lê Văn C', '0923456789', N'123 Nguyễn Trãi, Thanh Xuân, Hà Nội', 'khach3@gmail.com', 6),
(N'Phạm Thị D', '0934567890', N'456 Lê Lợi, Quận 1, TP.HCM', 'khach4@gmail.com', 7),
(N'Hoàng Văn E', '0945678901', N'789 Trần Phú, Hải Châu, Đà Nẵng', 'khach5@gmail.com', 8),
(N'Ngô Thị F', '0956789012', N'321 Hùng Vương, Ninh Kiều, Cần Thơ', 'khach6@gmail.com', 9),
(N'Đặng Văn G', '0967890123', N'654 Lý Thường Kiệt, Hồng Bàng, Hải Phòng', 'khach7@gmail.com', 10),
(N'Vũ Thị H', '0978901234', N'987 Nguyễn Huệ, Quận 3, TP.HCM', 'khach8@gmail.com', 11),
(N'Bùi Văn I', '0989012345', N'147 Điện Biên Phủ, Ba Đình, Hà Nội', NULL, NULL),
(N'Trịnh Thị K', '0990123456', N'258 Võ Văn Tần, Quận 10, TP.HCM', NULL, NULL);

-- =============================================
-- 3. THÊM XE
-- =============================================
INSERT INTO XE (BienSo, HangXe, DoiXe, MauXe, MaKH) VALUES
('29A-11111', N'Toyota Camry', 2022, N'Đen', 3),
('30B-22222', N'Honda Civic', 2021, N'Trắng', 3),
('51C-33333', N'Mazda CX-5', 2023, N'Đỏ', 4),
('43D-44444', N'Hyundai Tucson', 2020, N'Xám', 5),
('92E-55555', N'Kia Seltos', 2022, N'Xanh', 6),
('65F-66666', N'Ford Ranger', 2021, N'Bạc', 7),
('15G-77777', N'VinFast VF8', 2023, N'Xanh Navy', 8),
('29H-88888', N'Mercedes C200', 2022, N'Đen', 9),
('30K-99999', N'BMW 320i', 2021, N'Trắng', 10),
('51L-00000', N'Audi A4', 2020, N'Xám', 3);


-- =============================================
-- 4. THÊM YÊU CẦU DỊCH VỤ (Nhiều trạng thái khác nhau)
-- =============================================
INSERT INTO YEUCAU_DICHVU (TenKhachHang, SoDienThoai, DiaChi, MaDV, GhiChu, NgayYeuCau, TrangThai, NgayHen, GioHen) VALUES
(N'Nguyễn Văn A', '0901234567', N'Hà Nội', 1, N'Xe chạy được 10,000km, cần thay dầu', DATEADD(DAY, -5, GETDATE()), N'Hoàn thành', DATEADD(DAY, -4, GETDATE()), '09:00'),
(N'Trần Thị B', '0912345678', N'TP.HCM', 2, N'Rửa xe và hút bụi', DATEADD(DAY, -3, GETDATE()), N'Hoàn thành', DATEADD(DAY, -2, GETDATE()), '14:00'),
(N'Lê Văn C', '0923456789', N'Hà Nội', 3, N'Bảo dưỡng định kỳ 20,000km', DATEADD(DAY, -2, GETDATE()), N'Đang xử lý', DATEADD(DAY, -1, GETDATE()), '10:00'),
(N'Phạm Thị D', '0934567890', N'TP.HCM', 4, N'Lốp bị xì hơi', DATEADD(DAY, -1, GETDATE()), N'Đang xử lý', GETDATE(), '08:30'),
(N'Hoàng Văn E', '0945678901', N'Đà Nẵng', 5, N'Vệ sinh nội thất toàn bộ', GETDATE(), N'Mới', DATEADD(DAY, 1, GETDATE()), '15:00'),
(N'Ngô Thị F', '0956789012', N'Cần Thơ', 6, N'Xe bị xước nhẹ cần đánh bóng', GETDATE(), N'Mới', DATEADD(DAY, 2, GETDATE()), '09:30'),
(N'Đặng Văn G', '0967890123', N'Hải Phòng', 7, N'Ắc quy yếu, khó khởi động', GETDATE(), N'Mới', DATEADD(DAY, 1, GETDATE()), '11:00'),
(N'Vũ Thị H', '0978901234', N'TP.HCM', 8, N'Phanh kêu khi đạp', DATEADD(DAY, -7, GETDATE()), N'Đã hủy', NULL, NULL),
(N'Bùi Văn I', '0989012345', N'Hà Nội', 9, N'Khoang máy bẩn', DATEADD(DAY, -4, GETDATE()), N'Hoàn thành', DATEADD(DAY, -3, GETDATE()), '16:00'),
(N'Trịnh Thị K', '0990123456', N'TP.HCM', 1, N'Thay dầu định kỳ', DATEADD(DAY, -10, GETDATE()), N'Hoàn thành', DATEADD(DAY, -9, GETDATE()), '10:30'),
(N'Khách vãng lai 1', '0911111111', N'Quận 7, TP.HCM', 2, N'Rửa xe nhanh', GETDATE(), N'Mới', DATEADD(DAY, 1, GETDATE()), '13:00'),
(N'Khách vãng lai 2', '0922222222', N'Cầu Giấy, Hà Nội', 3, N'Kiểm tra tổng quát xe', GETDATE(), N'Mới', DATEADD(DAY, 3, GETDATE()), '08:00');

-- =============================================
-- 5. THÊM LIÊN HỆ (Nhiều trạng thái xử lý)
-- =============================================
INSERT INTO LIENHE (HoTen, Email, SoDienThoai, ChuDe, NoiDung, NgayGui, DaXuLy) VALUES
(N'Nguyễn Minh Tuấn', 'tuan.nm@gmail.com', '0901111111', N'Hỏi về giá dịch vụ', N'Cho tôi hỏi giá bảo dưỡng định kỳ cho xe Toyota Camry 2020 là bao nhiêu?', DATEADD(DAY, -10, GETDATE()), 1),
(N'Trần Thị Lan', 'lan.tt@gmail.com', '0902222222', N'Đặt lịch hẹn', N'Tôi muốn đặt lịch thay dầu cho xe vào thứ 7 tuần này được không?', DATEADD(DAY, -7, GETDATE()), 1),
(N'Lê Hoàng Nam', 'nam.lh@gmail.com', '0903333333', N'Khiếu nại dịch vụ', N'Xe tôi sau khi bảo dưỡng vẫn còn tiếng kêu lạ, mong được kiểm tra lại.', DATEADD(DAY, -5, GETDATE()), 1),
(N'Phạm Văn Đức', 'duc.pv@gmail.com', '0904444444', N'Hỏi về phụ tùng', N'Gara có bán lốp Michelin cho xe Honda CR-V không?', DATEADD(DAY, -3, GETDATE()), 0),
(N'Hoàng Thị Mai', 'mai.ht@gmail.com', '0905555555', N'Báo giá sửa chữa', N'Xe tôi bị va chạm nhẹ, cần báo giá sửa chữa và sơn lại.', DATEADD(DAY, -2, GETDATE()), 0),
(N'Ngô Quang Hải', 'hai.nq@gmail.com', '0906666666', N'Hợp tác kinh doanh', N'Tôi muốn hợp tác cung cấp phụ tùng cho gara, xin liên hệ lại.', DATEADD(DAY, -1, GETDATE()), 0),
(N'Đặng Thị Hương', 'huong.dt@gmail.com', '0907777777', N'Góp ý dịch vụ', N'Nhân viên phục vụ rất nhiệt tình, tôi rất hài lòng!', GETDATE(), 0),
(N'Vũ Văn Thành', 'thanh.vv@gmail.com', '0908888888', N'Hỏi giờ làm việc', N'Gara có làm việc vào Chủ nhật không?', GETDATE(), 0);

-- =============================================
-- 6. THÊM HÓA ĐƠN (Nhiều trạng thái)
-- Lưu ý: GaRa.sql đã có 2 khách hàng (MaKH 1,2) và 2 xe (MaXe 1,2)
-- Test data thêm 8 khách hàng (MaKH 3-10) và 10 xe (MaXe 3-12)
-- =============================================
-- Hóa đơn hoàn thành (dùng khách hàng và xe có sẵn)
INSERT INTO HOADON (MaKH, MaXe, UserId, NgayLap, HinhThucTT, TrangThai) VALUES
(1, 1, 1, DATEADD(DAY, -30, GETDATE()), N'Tiền mặt', N'Hoàn thành'),
(2, 2, 1, DATEADD(DAY, -25, GETDATE()), N'Chuyển khoản', N'Hoàn thành'),
(3, 3, 1, DATEADD(DAY, -20, GETDATE()), N'Thẻ', N'Hoàn thành'),
(4, 5, 1, DATEADD(DAY, -15, GETDATE()), N'Tiền mặt', N'Hoàn thành'),
(5, 6, 1, DATEADD(DAY, -10, GETDATE()), N'Chuyển khoản', N'Hoàn thành'),
(3, 4, 1, DATEADD(DAY, -8, GETDATE()), N'Tiền mặt', N'Hoàn thành'),
(1, 1, 1, DATEADD(DAY, -5, GETDATE()), N'Chuyển khoản', N'Hoàn thành');

-- Hóa đơn đang xử lý
INSERT INTO HOADON (MaKH, MaXe, UserId, NgayLap, HinhThucTT, TrangThai) VALUES
(6, 7, 1, DATEADD(DAY, -3, GETDATE()), N'Tiền mặt', N'Đang xử lý'),
(7, 8, 1, DATEADD(DAY, -2, GETDATE()), N'Chuyển khoản', N'Đang xử lý'),
(3, 4, 1, DATEADD(DAY, -1, GETDATE()), NULL, N'Đang xử lý');

-- Hóa đơn chờ xác nhận
INSERT INTO HOADON (MaKH, MaXe, UserId, NgayLap, HinhThucTT, TrangThai) VALUES
(8, 9, 1, GETDATE(), NULL, N'Chờ xác nhận'),
(9, 10, 1, GETDATE(), NULL, N'Chờ xác nhận'),
(10, 12, 1, GETDATE(), NULL, N'Chờ xác nhận');

-- Hóa đơn đã hủy
INSERT INTO HOADON (MaKH, MaXe, UserId, NgayLap, HinhThucTT, TrangThai) VALUES
(1, 1, 1, DATEADD(DAY, -40, GETDATE()), NULL, N'Đã hủy');


-- =============================================
-- 7. THÊM CHI TIẾT HÓA ĐƠN - DỊCH VỤ
-- =============================================
-- Tắt trigger tạm thời để insert dữ liệu test
DISABLE TRIGGER TRG_CapNhatTonKho ON CHITIET_HDSP;
GO

-- Hóa đơn 1: Thay dầu + Rửa xe
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(1, 1, 1, 1500000),
(1, 2, 1, 150000);

-- Hóa đơn 2: Bảo dưỡng định kỳ
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(2, 3, 1, 1500000);

-- Hóa đơn 3: Vá lốp + Rửa xe
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(3, 4, 2, 300000),
(3, 2, 1, 150000);

-- Hóa đơn 4: Vệ sinh nội thất + Vệ sinh khoang máy
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(4, 5, 1, 1200000),
(4, 9, 1, 500000);

-- Hóa đơn 5: Đánh bóng sơn
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(5, 6, 1, 2500000);

-- Hóa đơn 6: Vệ sinh nội thất (Hoàn thành)
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(6, 5, 1, 1200000);

-- Hóa đơn 7: Thay dầu + Rửa xe (Hoàn thành)
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(7, 1, 1, 1500000),
(7, 2, 1, 150000);

-- Hóa đơn 8: Thay ắc quy + Rửa xe (Đang xử lý)
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(8, 7, 1, 800000),
(8, 2, 1, 150000);

-- Hóa đơn 9: Sửa phanh (Đang xử lý)
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(9, 8, 1, 750000);

-- Hóa đơn 10: Bảo dưỡng + Thay dầu (Đang xử lý)
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(10, 3, 1, 1500000),
(10, 1, 1, 1500000);

-- Hóa đơn 11: Rửa xe + Vệ sinh nội thất (Chờ xác nhận)
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(11, 2, 1, 150000),
(11, 5, 1, 1200000);

-- Hóa đơn 12: Thay dầu (Chờ xác nhận)
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(12, 1, 1, 1500000);

-- Hóa đơn 13: Vá lốp (Chờ xác nhận)
INSERT INTO CHITIET_HDDV (MaHD, MaDV, SoLuong, DonGia) VALUES
(13, 4, 4, 300000);

-- =============================================
-- 8. THÊM CHI TIẾT HÓA ĐƠN - SẢN PHẨM
-- =============================================
-- Hóa đơn 1: Dầu nhớt + Lọc dầu
INSERT INTO CHITIET_HDSP (MaHD, MaSP, SoLuong, DonGia) VALUES
(1, 1, 4, 250000),
(1, 7, 1, 100000);

-- Hóa đơn 2: Lọc gió + Bugi + Nước làm mát
INSERT INTO CHITIET_HDSP (MaHD, MaSP, SoLuong, DonGia) VALUES
(2, 2, 1, 80000),
(2, 6, 4, 120000),
(2, 4, 1, 180000);

-- Hóa đơn 3: Dung dịch rửa kính
INSERT INTO CHITIET_HDSP (MaHD, MaSP, SoLuong, DonGia) VALUES
(3, 8, 2, 50000);

-- Hóa đơn 4: Không có sản phẩm (chỉ dịch vụ)

-- Hóa đơn 5: Không có sản phẩm (chỉ dịch vụ)

-- Hóa đơn 6: Không có sản phẩm (thay ắc quy - ắc quy khách tự mua)

-- Hóa đơn 6: Không có sản phẩm (chỉ vệ sinh)

-- Hóa đơn 7: Dầu nhớt + Lọc dầu (Hoàn thành)
INSERT INTO CHITIET_HDSP (MaHD, MaSP, SoLuong, DonGia) VALUES
(7, 1, 4, 250000),
(7, 7, 1, 100000);

-- Hóa đơn 8: Không có sản phẩm (thay ắc quy - khách tự mua)

-- Hóa đơn 9: Má phanh (Đang xử lý)
INSERT INTO CHITIET_HDSP (MaHD, MaSP, SoLuong, DonGia) VALUES
(9, 5, 1, 650000);

-- Hóa đơn 10: Dầu nhớt + Lọc dầu + Lọc gió (Đang xử lý)
INSERT INTO CHITIET_HDSP (MaHD, MaSP, SoLuong, DonGia) VALUES
(10, 1, 5, 250000),
(10, 7, 1, 100000),
(10, 2, 1, 80000);

-- Hóa đơn 11-14: Không có sản phẩm (chờ xác nhận/đã hủy)

-- Bật lại trigger
ENABLE TRIGGER TRG_CapNhatTonKho ON CHITIET_HDSP;
GO

-- =============================================
-- 9. CẬP NHẬT TỔNG TIỀN CHO CÁC HÓA ĐƠN
-- =============================================
UPDATE HOADON
SET TongTien = (
    SELECT ISNULL(SUM(ThanhTien), 0)
    FROM CHITIET_HDSP
    WHERE CHITIET_HDSP.MaHD = HOADON.MaHD
) + (
    SELECT ISNULL(SUM(ThanhTien), 0)
    FROM CHITIET_HDDV
    WHERE CHITIET_HDDV.MaHD = HOADON.MaHD
);

-- =============================================
-- 10. THÊM THÊM DỊCH VỤ VÀ SẢN PHẨM
-- =============================================
INSERT INTO DICHVU (TenDV, DonGia, MoTa, HinhAnh, TrangThai) VALUES
(N'Kiểm tra điện - Điện tử', 350000, N'Kiểm tra hệ thống điện, cảm biến, ECU', 'https://images.pexels.com/photos/3807517/pexels-photo-3807517.jpeg', 1),
(N'Thay dầu hộp số', 800000, N'Thay dầu hộp số tự động/số sàn', 'https://images.pexels.com/photos/4489702/pexels-photo-4489702.jpeg', 1),
(N'Nạp ga điều hòa', 450000, N'Kiểm tra và nạp gas điều hòa ô tô', 'https://images.pexels.com/photos/3807386/pexels-photo-3807386.jpeg', 1),
(N'Cứu hộ xe', 500000, N'Dịch vụ cứu hộ xe trong bán kính 20km', 'https://images.pexels.com/photos/97075/pexels-photo-97075.jpeg', 1),
(N'Dịch vụ ngừng cung cấp', 999000, N'Dịch vụ test đã ngừng', NULL, 0);

INSERT INTO SANPHAM (TenSP, DonGia, SoLuongTon, DonVi, MoTa, HinhAnh) VALUES
(N'Dầu hộp số ATF', 350000, 30, N'Lít', N'Dầu hộp số tự động cao cấp', 'https://images.pexels.com/photos/4489765/pexels-photo-4489765.jpeg'),
(N'Gas điều hòa R134a', 200000, 50, N'Bình', N'Gas lạnh cho điều hòa ô tô', 'https://images.pexels.com/photos/4489702/pexels-photo-4489702.jpeg'),
(N'Gạt mưa Bosch', 280000, 40, N'Bộ', N'Gạt mưa cao cấp Bosch', 'https://images.pexels.com/photos/3807277/pexels-photo-3807277.jpeg'),
(N'Bình ắc quy 12V 60Ah', 1500000, 10, N'Bình', N'Ắc quy khởi động cho xe hơi', 'https://images.pexels.com/photos/5572265/pexels-photo-5572265.jpeg'),
(N'Sản phẩm hết hàng', 100000, 0, N'Cái', N'Sản phẩm test hết hàng', NULL);

-- =============================================
-- HOÀN TẤT - KIỂM TRA DỮ LIỆU
-- =============================================
PRINT N'=== THỐNG KÊ DỮ LIỆU TEST ==='
PRINT N''

SELECT 'USERS' AS [Bảng], COUNT(*) AS [Số lượng] FROM USERS
UNION ALL
SELECT 'KHACHHANG', COUNT(*) FROM KHACHHANG
UNION ALL
SELECT 'XE', COUNT(*) FROM XE
UNION ALL
SELECT 'DICHVU', COUNT(*) FROM DICHVU
UNION ALL
SELECT 'SANPHAM', COUNT(*) FROM SANPHAM
UNION ALL
SELECT 'YEUCAU_DICHVU', COUNT(*) FROM YEUCAU_DICHVU
UNION ALL
SELECT 'LIENHE', COUNT(*) FROM LIENHE
UNION ALL
SELECT 'HOADON', COUNT(*) FROM HOADON
UNION ALL
SELECT 'CHITIET_HDDV', COUNT(*) FROM CHITIET_HDDV
UNION ALL
SELECT 'CHITIET_HDSP', COUNT(*) FROM CHITIET_HDSP;

PRINT N''
PRINT N'=== DỮ LIỆU TEST ĐÃ ĐƯỢC TẠO THÀNH CÔNG ==='
GO
