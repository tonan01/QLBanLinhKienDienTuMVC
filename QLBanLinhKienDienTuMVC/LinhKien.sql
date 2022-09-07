create database QLLinhKienDienTu
create table KhachHang
(
 UserName varchar(50) primary key not null,
 HoTen nvarchar(50),
 DienThoai varchar(12),
 Email varchar(50),
 DiaChi nvarchar(50),
 MatKhau varchar(50),
)

create table Loai
(
 MaLoai int identity(1,1) primary key not null,
 TenLoai nvarchar(50),
 NoTe varchar(50),
)
create table SanPham
(
 MaSP int identity(1,1) primary key not null,
 TenSP nvarchar(200),
 GiaBan int,
 MoTa nvarchar(200),
 Anh varchar(50),
 SLTon int,
 MaLoai int not null,
 constraint FK_SanPham_MaLoai foreign key(MaLoai) references Loai(MaLoai)
)

create table DonHang
(
 MaDonHang int identity(1,1) primary key not null,
 NgayGiao date,
 NgayDat date,
 TinhTrangGiaoHang nvarchar(50),
 UserName varchar(50) not null,
 constraint FK_DonHang_UserName foreign key(UserName) references KhachHang(UserName)
)
create table ChiTietDonHang
(
 MaDonHang int not null,
 MaSP int not null,
 SoLuong int,
 DonGia int,
 constraint PK_ChiTietDonHang_MaDonHang_MaSP primary key(MaDonHang,MaSP),
 constraint FK_ChiTietDonHang_MaDonHang foreign key(MaDonHang) references DonHang(MaDonHang),
 constraint FK_ChiTietDonHang_MaSP foreign key(MaSP) references SanPham(MaSP),
)
insert into KhachHang values('Admin',N'Phạm Gia Bảo','08888888','paopaokk321@gmail.com',N'Tây Ninh','123456'),
('giabao',N'Nguyễn Lê Gia Bảo','08777777','giabao@gmail.com',N'TPHCM','123456'),
('dat',N'Phạm Gia Đat','0777777','giadat321@gmail.com',N'TPHCM','123456'),
('hihi',N'Phạm Gia hihi','0644444','hihi@gmail.com',N'Tây Ninh','123456')
insert into Loai values(N'Vi điều khiển-Nhúng','LK'),
(N'Module Ứng dụng','LK'),
(N'Cảm Biến','LK'),
(N'Linh kiện Điện Tử','LK'),
(N'IC Chức Năng','LK'),
(N'Thiết Bị Chế Tạo','LK'),
(N'Dụng Cụ,Phụ Kiện','LK')

insert into SanPham values(N'Mạch Nạp ISP 89S/AVR Mạch Nạp 8051','65000',N'Mô tả đang cập nhật','nhung1.jpg',100,'1'),
(N'Mạch Nạp Pickit 2 Full','280000',N'Mô tả đang cập nhật','nhung2.jpg',100,'1'),
(N'Mạch Nạp Pickit 3 Full','650000',N'Mô tả đang cập nhật','nhung3.jpg',100,'1'),
(N'Module Điều Chỉnh Điện Áp Cao Dimmer 4000W 2P Không Vỏ','65000',N'Mô tả đang cập nhật','m1.jpg',100,'2'),
(N'Mạch Đổi Điện Từ 220V Sang 110V 16A','73150',N'Mô tả đang cập nhật','m2.jpg',100,'2'),
(N'Mạch Tạo Thời Gian Trễ Đóng Ngắt Relay NE555 - 5V','34900',N'Mô tả đang cập nhật','m3.jpg',100,'2'),
(N'Mạch Âm Ly SON 7227 Vỏ Nhôm 12V - Loa 2 Ohm Đến 16 Ohm','259000',N'Mô tả đang cập nhật','m4.jpg',100,'2'),
(N'Cảm Biến Quang Omron E3JK-DS30M2 12-24VDC','379000',N'Mô tả đang cập nhật','c1.jpg',100,'3'),
(N'Module Cảm Biến Mực Nước','8000',N'Mô tả đang cập nhật','c2.jpg',100,'3'),
(N'Mosfet IRF1010 TO-220 60V 84A (Kênh N) - Hàng Tháo Máy','9500',N'Mô tả đang cập nhật','l1.jpg',100,'4'),
(N'Đèn Pin Mini Siêu Nhẹ Và Siêu Sáng Police 3W Không Phụ Kiện','19900',N'Mô tả đang cập nhật','l2.jpg',100,'4'),
(N'Tụ 10uF 370V Dùng Cho Động Cơ Máy Bơm','21000',N'Mô tả đang cập nhật','l3.jpg',100,'4'),
(N'IC Nguồn LNK304 DIP7','16000',N'Mô tả đang cập nhật','ic1.jpg',100,'5'),
(N'Phíp Đồng KM 1 Mặt Loại To Khổ A4 20x30cm','28000',N'Mô tả đang cập nhật','ct1.jpg',100,'6'),
(N'Thiếc Hàn 63% 0.6mm Cuộn 100g','80000',N'Mô tả đang cập nhật','ct2.jpg',100,'6'),
(N'Đầu Mũi Hàn Nhọn 900M-T-B - Mũi Hàn 936','14999',N'Mô tả đang cập nhật','ct3.jpg',100,'6'),
(N'Quạt Thông Gió Hút Mùi Tico TC-14AV6 20x20x8cm','179000',N'Mô tả đang cập nhật','pk1.jpg',100,'7'),
(N'Nguồn Tổ Ong Trong Nhà 12V 50A - Công Suất Thực 600W','620000',N'Mô tả đang cập nhật','pk2.jpg',100,'7')
select * from KhachHang
select * from Loai
select * from SanPham
select * from DonHang
select * from ChiTietDonHang



--tìm kếm
go
create proc TimKiem @tk nvarchar(100)
as
 begin
 select * from SanPham where TenSP like @tk
 end
 exec TimKiem N'%Mạch Nạp Pickit 2 Full%'

 select * from SanPham
 select * from DonHang 
 select * from ChiTietDonHang
 select MAX(MaDonHang) from DonHang 
 delete from ChiTietDonHang
 delete from DonHang
 
 
 insert into DonHang(NgayDat,TinhTrangGiaoHang,UserName) values(GETDATE(),N'Đang Xử Lý','bo')
 exec ThanhToanDH '26','bo',3,111
--thanh toán
go
create proc ThanhToanDH @mDH int,@UserN varchar(50),
@msp int,@sl int
as
 begin
 --Tạo đơn hàng
 if(select COUNT(*) from DonHang where MaDonHang=@mDH)=0 --(chưa có đơn hàng)
  begin
   insert into DonHang(NgayDat,TinhTrangGiaoHang,UserName) values(GETDATE(),N'Đang Xử Lý',@UserN)
   --lưu lại mã hd vừa tạo
   select @mDH=MAX(MaDonHang) from DonHang 
   --lấy ra giá bán của sản phâm
   declare @gb int
   select @gb=GiaBan from SanPham where MaSP=@msp
   --thêm vào chi tiết
   insert into ChiTietDonHang values(@mDH,@msp,@sl,@gb)

   --kiểm tra số lượng
   if(select @sl-SLTon from SanPham where MaSP=@msp)<1  
    begin
	 --giam so luong trong kho
     update SanPham set SLTon=(SLTon - @sl) where MaSP=@msp
	end
  end
 else--(đã có đơn hàng)
  begin --Thêm Vào chi Tiết hóa don
   --lấy ra giá bán của sản phâm
   declare @gb1 int
   select @gb1=GiaBan from SanPham where MaSP=@msp

   insert into ChiTietDonHang values(@mDH,@msp,@sl,@gb1)

   --kiểm tra số lượng
   if(select @sl-SLTon from SanPham where MaSP=@msp)<1  
    begin
	 --giam so luong trong kho
     update SanPham set SLTon=(SLTon - @sl) where MaSP=@msp
	end
  end
 end

 --Xóa hóa đơn đã hoàn thành
go
create proc HuyDonHang @mDH int
as
 begin
 --đơn hàng đã hoàn thành
 if(select count(*) from DonHang where MaDonHang=@mDH and TinhTrangGiaoHang='Đang Xử Lý')<1
  begin
   delete from ChiTietDonHang where MaDonHang=@mDH
   delete from DonHang where MaDonHang=@mDH
  end
 end