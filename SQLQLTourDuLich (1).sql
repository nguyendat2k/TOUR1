use master
if exists(select * from sysdatabases where name='QLDatTour')
	drop database QLDatTour
create database QLDatTour

use QLDatTour

create table HangKhachSan(
	MaHang varchar(4),
	TenHang nvarchar(30),
	Primary key(MaHang)
)

create table PhuongTien(
	MaPT varchar(4),
	TenPT nvarchar(30)
	Primary key(MaPT),
)

create table KhachSan(
	MaKS varchar(5),
	TenKS nvarchar(50),
	DiaChiKS nvarchar(100),
	AnhKS varchar(100),
	MaHang varchar(4) references HangKhachSan(MaHang),
	Primary Key(MaKS)
)

create table DiaDiemThamQuan(
	MaThamQuan varchar(5),
	TenDiaDiem nvarchar(50),
	NoiDung nvarchar(1000),
	AnhDiaDiem varchar(100),
	YNghia nvarchar(1000),
	Primary Key(MaThamQuan)
)

create table Tour(
	MaTour varchar(5),
	TenTour nvarchar(50),
	MoTa nvarchar(max),
	SoNgay tinyint,
	Gia float,
	NgayKhoiHanh datetime,
	Anhbia varchar(100),
	Primary Key(MaTour)
)

create table DungChan(
	MaThamQuan varchar(5) references  DiaDiemThamQuan(MaThamQuan),
	MaTour varchar(5) references Tour(MaTour),
	MaPT varchar(4) references PhuongTien(MaPT),
	primary key(MaThamQuan, MaTour)
)

create table SuDung(
	MaTour varchar(5) references Tour(MaTour),
	MaPT varchar(4) references PhuongTien(MaPT),
	primary key(MaTour,MaPT)
)

create table O(
	MaTour varchar(5) references Tour(MaTour),
	MaKS varchar(5) references KhachSan(MaKS),
	primary key(MaTour,MaKS)
)

create table NhanVien(
	MaNV varchar(5),
	TenNV nvarchar(30),
	ChucVu nvarchar(50),
	Primary Key(MaNV)
)

create table PhieuDoan(
	MaDoan varchar(4),
	TenCQ nvarchar(50),
	DiaChiCQ nvarchar(100),
	SDTCQ varchar(12),
	TenNĐD nvarchar(30),
	NgayDi DateTime,
	SoNguoi Tinyint,
	MaTour varchar(5) references Tour(MaTour),
	MaNV varchar(5) references NhanVien(MaNV),
	Primary Key(MaDoan)
)

create table ThanhVien(
	STT varchar(5),
	CMNDTV varchar(12),
	DiaChiTV nvarchar(100),
	SDTTV1 varchar(12),
	SDTTV2 varchar(12),
	HoTenTV nvarchar(30),
	MaDoan varchar(4) references PhieuDoan(MaDoan),
	Primary Key (STT)
)

create table HuongDanVien(
	MaHDV varchar(5),
	TenHDV nvarchar(30),
	SDTHDV1 varchar(12),
	SDTHDV2 varchar(12),
	Luong float,
	Primary Key(MaHDV)
)

create table PhuTrach(
	MaDoan varchar(4) references PhieuDoan(MaDoan),
	MaHDV varchar(5) references HuongDanVien(MaHDV),
	primary key(MaDoan,MaHDV)
)

create table Chuyen(
	MaChuyen varchar(10),
	NgayDi DateTime,
	MaHDV varchar(5) references HuongDanVien(MaHDV),
	MaTour varchar(5) references Tour(MaTour),
	Primary Key(MaChuyen)
)

create table KhachLe(
	MaKhachLe varchar(5),
	CMNDKL varchar(12),
	SDTKL1 varchar(12),
	SDTKL2 varchar(12),
	DiaChiKL nvarchar(100),
	HoTenKL nvarchar(30),
	Primary Key(MaKhachLe)
)

create table HoaDon(
	MaHoaDon varchar(5),
	TongTien float,
	ConLai float,
	TienDatCoc float,
	NgayDangKi datetime,
	MaKhachLe varchar(5) references KhachLe(MaKhachLe),
	Primary Key(MaHoaDon)
)

create table DangKi(
	MaHoaDon varchar(5) references HoaDon(MaHoaDon),
	MaChuyen varchar(10) references Chuyen(MaChuyen),
	primary key(MaHoaDon,MaChuyen)
)
GO
---mã tự động hạng khách sạn
create trigger hang_khachsan
on HangKhachSan
instead of insert
as
begin
	declare @tenhang char(1)
	declare @stt int
	declare @mahang char(4)
	declare @STT_MOI char(2)
	set @tenhang=(select left(TenHang,1) from inserted)
	set @stt=(select COUNT(*) from HangKhachSan)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='0'+CAST(@stt as CHAR(2))
	else
		set @STT_MOI=CAST(@stt as CHAR(2))
	set @mahang='H'+@tenhang+@STT_MOI
	print @mahang
	
	insert into HangKhachSan(MaHang,TenHang)
	values (@mahang, (select TenHang from inserted))
end
Go
insert into HangKhachSan(TenHang)
values('1 Sao')
insert into HangKhachSan(TenHang)
values('2 Sao')
insert into HangKhachSan(TenHang)
values('3 Sao')  
insert into HangKhachSan(TenHang)
values('4 Sao')
insert into HangKhachSan(TenHang)
values('5 Sao')  	 
select * from HangKhachSan

---mã tự động phương tiện
select * from PhuongTien
go

create trigger phuong_tien
on PhuongTien
instead of insert
as
begin
	declare @stt int
	declare @mapt char(4)
	declare @STT_MOI char(2)
	set @stt=(select COUNT(*) from PhuongTien)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='0'+CAST(@stt as CHAR(2))
	else
		set @STT_MOI=CAST(@stt as CHAR(2))
	set @mapt='PT'+@STT_MOI
	print @mapt
	
	insert into PhuongTien(MaPT,TenPT)
	values (@mapt, (select TenPT from inserted))
end
Go
insert into PhuongTien(TenPT)
values(N'MÁY BAY')
insert into PhuongTien(TenPT)
values(N'XE KHÁCH')
  	 
select * from PhuongTien


---mã tự động tên khách sạn
select * from KhachSan
go

create trigger ten_khach_san
on KhachSan
instead of insert
as
begin
	declare @stt int
	declare @maks char(4)
	declare @STT_MOI char(2)
	set @stt=(select COUNT(*) from KhachSan)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='0'+CAST(@stt as CHAR(2))
	else
		set @STT_MOI=CAST(@stt as CHAR(2))
	set @maks='KS'+@STT_MOI
	print @maks
	
	insert into KhachSan(MaKS,TenKS,DiaChiKS,AnhKS,MaHang)
	values (@maks, (select TenKS from inserted),(select DiaChiKS from inserted),(select AnhKS from inserted),(select MaHang from inserted))
end
Go
insert into KhachSan(TenKS,DiaChiKS,AnhKS,MaHang)
values('AURORA HOTEL',N' Quận 1, thành phố Hồ Chí Minh','hotel1.png','H303')
insert into KhachSan(TenKS,DiaChiKS,AnhKS,MaHang)
values('VENUS HOTEL',N' Quận 3, thành phố Hồ Chí Minh','hotel2.png','H404')
insert into KhachSan(TenKS,DiaChiKS,AnhKS,MaHang)
values('SWISS BELRESORT',N'KDL Hồ Tuyền Lâm, P3, TP Đà Lạt, Lâm Đồng','hotel3.png','H404')
  	 
select * from KhachSan

---mã tự động Tour
select * from Tour
go

create trigger ma_tour
on Tour
instead of insert
as
begin
	declare @stt int
	declare @matour char(10)
	declare @STT_MOI char(3)
	set @stt=(select COUNT(*) from Tour)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='00'+CAST(@stt as CHAR(3))
	else
		set @STT_MOI=CAST(@stt as CHAR(3))
	set @matour='T'+@STT_MOI
	print @matour
	
	insert into Tour(MaTour,TenTour,SoNgay,Gia,NgayKhoiHanh,Anhbia)
	values (@matour, (select TenTour from inserted),(select SoNgay from inserted),(select Gia from inserted),(select NgayKhoiHanh from inserted),(select Anhbia from inserted))
end
Go
insert into Tour(TenTour,SoNgay,Gia,NgayKhoiHanh,Anhbia)
values(N'ĐÀ LẠT',3,'19790000','20200630','DALAT.png')
insert into Tour(TenTour,SoNgay,Gia,NgayKhoiHanh,Anhbia)
values(N'PHÚ QUỐC',3,'19790000','20200630','PHUQUOC.png')
insert into Tour(TenTour,SoNgay,Gia,NgayKhoiHanh,Anhbia)
values(N'VỊNH HẠ LONG',3,'19790000','20200630','VINHHALONG.png')
  	 
select * from Tour

---mã tự động địa điểm tham quan
select * from DiaDiemThamQuan
go

create trigger dia_diem_tham_quan
on DiaDiemThamQuan
instead of insert
as
begin
	declare @stt int
	declare @maddtq char(5)
	declare @STT_MOI char(3)
	set @stt=(select COUNT(*) from DiaDiemThamQuan)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='00'+CAST(@stt as CHAR(3))
	else
		set @STT_MOI=CAST(@stt as CHAR(3))
	set @maddtq='TQ'+@STT_MOI
	print @maddtq
	
	insert into DiaDiemThamQuan(MaThamQuan,TenDiaDiem,NoiDung,AnhDiaDiem,YNghia)
	values (@maddtq, (select TenDiaDiem from inserted),(select NoiDung from inserted),(select AnhDiaDiem from inserted),(select YNghia from inserted))
end
Go
insert into DiaDiemThamQuan(TenDiaDiem,NoiDung,AnhDiaDiem,YNghia)
values(N'FAIRYTALE LAND LANGBIANG',N'Nghỉ đêm tại Đà Lạt','diadanh.jpg',N'du khách sẽ được lạc vào khu vườn cổ tích của những chú lùn Hobbiton') 
 ---thêm dữ liệu vào dừng chân
insert into DungChan(MaTour,MaThamQuan,MaPT)
values('T001','TQ001','PT01')

select * from DungChan	

---mã tự động phiếu đoàn
select * from PhieuDoan
go

create trigger phieu_doan
on PhieuDoan
instead of insert
as
begin
	declare @stt int
	declare @mapd char(4)
	declare @STT_MOI char(3)
	set @stt=(select COUNT(*) from PhieuDoan)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='00'+CAST(@stt as CHAR(3))
	else
		set @STT_MOI=CAST(@stt as CHAR(3))
	set @mapd='D'+@STT_MOI
	print @mapd
	
	insert into PhieuDoan(MaDoan,TenCQ,DiaChiCQ,SDTCQ,TenNĐD,NgayDi,SoNguoi,MaTour,MaNV)
	values (@mapd, (select TenCQ from inserted),(select DiaChiCQ from inserted),(select SDTCQ from inserted),(select TenNĐD from inserted),(select NgayDi from inserted),(select SoNguoi from inserted),(select MaTour from inserted),(select MaNV from inserted))
end
Go
---mã tự động thành viên
select * from ThanhVien
go

create trigger thanh_vien
on ThanhVien
instead of insert
as
begin
	declare @stt int
	declare @matv char(5)
	declare @STT_MOI char(3)
	set @stt=(select COUNT(*) from ThanhVien)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='00'+CAST(@stt as CHAR(3))
	else
		set @STT_MOI=CAST(@stt as CHAR(3))
	set @matv='TV'+@STT_MOI
	print @matv
	
	insert into ThanhVien(STT,CMNDTV,DiaChiTV,SDTTV1,SDTTV2,HoTenTV,MaDoan)
	values (@matv, (select CMNDTV from inserted),(select DiaChiTV from inserted),(select SDTTV1 from inserted),(select SDTTV2 from inserted),(select HoTenTV from inserted),(select MaDoan from inserted))
end
Go
---mã tự động hướng dẫn viên
select * from HuongDanVien
go

create trigger huong_dan_vien
on HuongDanVien
instead of insert
as
begin
	declare @stt int
	declare @mahdv char(5)
	declare @STT_MOI char(3)
	set @stt=(select COUNT(*) from HuongDanVien)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='00'+CAST(@stt as CHAR(3))
	else
		set @STT_MOI=CAST(@stt as CHAR(3))
	set @mahdv='TV'+@STT_MOI
	print @mahdv
	
	insert into HuongDanVien(MaHDV,TenHDV,SDTHDV1,SDTHDV2,Luong)
	values (@mahdv, (select TenHDV from inserted),(select SDTHDV1 from inserted),(select SDTHDV2 from inserted),(select Luong from inserted))
end
Go
---mã tự động khách lẻ
select * from KhachLe
go

create trigger khach_le
on KhachLe
instead of insert
as
begin
	declare @stt int
	declare @makl char(5)
	declare @STT_MOI char(3)
	set @stt=(select COUNT(*) from KhachLe)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='00'+CAST(@stt as CHAR(3))
	else
		set @STT_MOI=CAST(@stt as CHAR(3))
	set @makl='KL'+@STT_MOI
	print @makl
	
	insert into KhachLe(MaKhachLe,CMNDKL,SDTKL1,SDTKL2,DiaChiKL,HoTenKL)
	values (@makl, (select CMNDKL from inserted),(select SDTKL1 from inserted),(select SDTKL2 from inserted),(select DiaChiKL from inserted),(select HoTenKL from inserted))
end
Go
---mã tự động khóa đơn
select * from HoaDon
go

create trigger hoa_don
on HoaDon
instead of insert
as
begin
	declare @stt int
	declare @mahd char(5)
	declare @STT_MOI char(3)
	set @stt=(select COUNT(*) from HoaDon)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='00'+CAST(@stt as CHAR(3))
	else
		set @STT_MOI=CAST(@stt as CHAR(3))
	set @mahd='HD'+@STT_MOI
	print @mahd
	
	insert into HoaDon(MaHoaDon,TongTien,ConLai,TienDatCoc,NgayDangKi,MaKhachLe)
	values (@mahd, (select TongTien from inserted),(select ConLai from inserted),(select TienDatCoc from inserted),(select NgayDangKi from inserted),(select MaKhachLe from inserted))
end
Go
---mã tự động nhân viên
select * from NhanVien
go

create trigger nhan_vien
on NhanVien
instead of insert
as
begin
	declare @stt int
	declare @manv char(5)
	declare @STT_MOI char(3)
	set @stt=(select COUNT(*) from NhanVien)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='00'+CAST(@stt as CHAR(3))
	else
		set @STT_MOI=CAST(@stt as CHAR(3))
	set @manv='HD'+@STT_MOI
	print @manv
	
	insert into NhanVien(MaNV,TenNV,ChucVu)
	values (@manv, (select TenNV from inserted),(select ChucVu from inserted))
end
Go
---mã tự động chuyến
select * from Chuyen
go

create trigger ma_chuyen
on Chuyen
instead of insert
as
begin
	declare @stt int
	declare @machuyen char(10)
	declare @STT_MOI char(3)
	declare @matour char(5)
	set @matour=(select MaTour from Chuyen)
	set @stt=(select COUNT(*) from Chuyen)
	set @stt=@stt+1
	if(@stt<10)
		set @STT_MOI='00'+CAST(@stt as CHAR(3))
	else
		set @STT_MOI=CAST(@stt as CHAR(3))
	set @machuyen='C'+@STT_MOI+@matour
	print @machuyen
	
	insert into Chuyen(MaChuyen,NgayDi,MaHDV,MaTour)
	values (@machuyen, (select NgayDi from inserted),(select MaHDV from inserted),(select MaTour from inserted))
end
Go



	
	
