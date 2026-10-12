using EduHub.Domain;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
        internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== BUOC 1: TAO CLB =====");

            Club club = Club.Create(
                "Tech Club",
                "Cau lac bo cong nghe va lap trinh"
            );

            Console.WriteLine($"Ten CLB: {club.Name}");
            Console.WriteLine($"Mo ta: {club.Description}");

            Console.WriteLine("\n===== BUOC 2: THEM THANH VIEN =====");

            Member member1 = Member.Create(
                "Nguyen Van An", "an@gmail.com", "SV001"
            );

            Member member2 = Member.Create(
                "Tran Thi Binh", "binh@gmail.com", "SV002"
            );

            Member member3 = Member.Create(
                "Le Van Cuong", "cuong@gmail.com", "SV003"
            );

            club.AddMember(member1);
            club.AddMember(member2);
            club.AddMember(member3);

            foreach (Member member in club.Members)
            {
                Console.WriteLine(
                    $"- {member.Name} | {member.StudentCode}"
                );
            }

            Console.WriteLine($"Tong so thanh vien: {club.MemberCount()}");

           
            Console.WriteLine("\n===== BUOC 3: TAO SU KIEN =====");

            Event workshop = Event.Create(
                club.Id,
                "Workshop AI",
                DateTime.Now.AddDays(7),
                50,
                false
            );

            Console.WriteLine($"Su kien: {workshop.Title}");
            Console.WriteLine($"Suc chua: {workshop.Capacity}");
            Console.WriteLine($"Ngay to chuc: {workshop.Date:dd/MM/yyyy}");
            Console.WriteLine(
                $"Hinh thuc: {(workshop.IsOnline ? "Truc tuyen" : "Truc tiep")}"
            );

            Console.WriteLine("\n===== BUOC 4: DANG KY SU KIEN =====");

            List<Registration> registrations = new List<Registration>();

            Registration registration1 = Registration.Create(
                member1.Id, workshop.Id
            );

            Registration registration2 = Registration.Create(
                member2.Id, workshop.Id
            );

            registrations.Add(registration1);
            registrations.Add(registration2);

            Console.WriteLine(
                $"{member1.Name} dang ky - Trang thai: {registration1.Status}"
            );

            Console.WriteLine(
                $"{member2.Name} dang ky - Trang thai: {registration2.Status}"
            );

            registration1.Confirm();
            registration2.Confirm();

            Console.WriteLine("\nSau khi xac nhan:");

            Console.WriteLine(
                $"{member1.Name} - Trang thai: {registration1.Status}"
            );

            Console.WriteLine(
                $"{member2.Name} - Trang thai: {registration2.Status}"
            );

            // BUOC 5: Mot thanh vien check-in
            Console.WriteLine("\n===== BUOC 5: CHECK-IN =====");

            List<CheckIn> checkIns = new List<CheckIn>();

            CheckIn checkIn1 = CheckIn.Create(
                registration1,
                CheckInMethod.Manual
            );

            checkIns.Add(checkIn1);

            Console.WriteLine($"{member1.Name} da check-in thanh cong.");
            Console.WriteLine(checkIn1.Summary());

            Console.WriteLine("\n===== BUOC 6: BAO CAO THAM DU =====");

            Console.WriteLine($"CLB: {club.Name}");
            Console.WriteLine($"Su kien: {workshop.Title}");
            Console.WriteLine($"So luong thanh vien CLB: {club.MemberCount()}");
            Console.WriteLine($"So dang ky: {registrations.Count}");
            Console.WriteLine($"So check-in: {checkIns.Count}");

            Console.WriteLine("\nDanh sach nguoi dang ky:");

            foreach (Registration registration in registrations)
            {
                Member? member = club.Members.Find(
                    m => m.Id == registration.MemberId
                );

                bool hasCheckedIn = checkIns.Exists(
                    c => c.RegistrationId == registration.Id
                );

                if (member != null)
                {
                    Console.WriteLine(
                        $"- {member.Name} | " +
                        $"Trang thai: {registration.Status} | " +
                        $"Check-in: {(hasCheckedIn ? "Da check-in" : "Chua check-in")}"
                    );
                }
            }

          
            Console.WriteLine("\n===== BUOC 7: KIEM TRA NGOAI LE =====");

            Registration testRegistration = Registration.Create(
                member3.Id, workshop.Id
            );

            testRegistration.Cancel();

            try
            {
                testRegistration.Confirm();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(
                    $"Khong the xac nhan phieu da huy: {ex.Message}"
                );
            }

            Console.WriteLine("\n===== KET THUC DEMO =====");
            Console.WriteLine("Nhan phim bat ky de thoat...");
            Console.ReadKey();
        }
    }
}
