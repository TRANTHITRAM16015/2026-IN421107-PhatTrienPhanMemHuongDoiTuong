using System;
using System.Collections.Generic;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services
{
    
    public class LoggingClubRepository : IClubRepository
    {
        private readonly IClubRepository _innerRepo;
        private readonly IAppLogger _logger;

        public LoggingClubRepository(IClubRepository innerRepo, IAppLogger logger)
        {
            _innerRepo = innerRepo;
            _logger = logger;
        }

        public List<Club> GetAllClubs()
        {
            _logger.Log("Truy cập danh sách tất cả các câu lạc bộ.");
            return _innerRepo.GetAllClubs();
        }

        public void AddClub(Club club)
        {
            _innerRepo.AddClub(club);
            _logger.Log($"Đã thêm CLB mới: {club.Name}");
        }

        public Club GetClubById(Guid id)
        {
            return _innerRepo.GetClubById(id);
        }

        public List<Member> GetMembersByClubId(Guid clubId)
        {
            return _innerRepo.GetMembersByClubId(clubId);
        }

        public void AddMember(Member member)
        {
            _innerRepo.AddMember(member);
            _logger.Log($"Thành viên {member.Name} đã tham gia CLB.");
        }
    }
}
