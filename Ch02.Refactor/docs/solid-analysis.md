Lỗi 1: Vi phạm nguyên tắc trách nhiệm duy nhất (SRP)
- Các method như: CreateClub (dòng 35), AddMemberToClub (dòng 67), CreateEvent (dòng 88), PrintClubReport (dòng 103).
- Hậu quả: ClubManager phải gánh vác quá nhiều nhiệm vụ cùng lúc như quản lí dữ liệu, validate dữ liệu, gửi thông báo, ghi file log, xuất báo cáo.
Lỗi 2: Vi phạm nguyên lí đóng/mở (OCP)
- Phần xử lí thông báo trong CreateClub (dùng cấu trúc if-else để kiểm tra @gmail.com và @yahoo.com) (dòng 52 & 56) và CreateEvent.
- Hậu quả: Khi hệ thống muốn mở rộng thêm kênh thông báo mới, lập trình viên bắt buộc phải sửa trực tiếp code bên trong các method thay vì mở rộng thông qua kế thừa hoặc interface (áp dụng Factory Pattern).
Lỗi 3: Vi phạm nguyên lý đảo ngược phụ thuộc (Dependency Inversion Principle - DIP)
- Các thao tác ghi file nhật ký diễn ra trực tiếp bằng cách gọi hàm File.AppendAllText(_logFilePath, ...) nằm rải rác trong các method CreateClub, AddMemberToClub, CreateEvent, và PrintClubReport (dòng 47, 79, 94, 111). 
- Hậu quả: ClubManager (module cấp cao) phụ thuộc trực tiếp vào tầng hạ tầng chi tiết cụ thể của hệ thống tệp (File class), thay vì phụ thuộc vào một abstraction/interface trừu tượng hóa việc ghi log (như IAppLogger). 
Lỗi 4: Vi phạm sự tách biệt về hành vi / Trách nhiệm thông báo (Separation of Concerns / ISP)
- Phương thức CreateEvent tự động duyệt qua danh sách thành viên và gọi lệnh Console.WriteLine để in trực tiếp thông báo ra màn hình (dòng 96-97).
- Hậu quả: Trộn lẫn logic nghiệp vụ cốt lõi (tạo sự kiện) với logic phân phối thông điệp (gửi thông báo), khiến mã nguồn bị gắn kết chặt chẽ (tight coupling), khó kiểm thử đơn vị (unit test) và khó tái sử dụng.   