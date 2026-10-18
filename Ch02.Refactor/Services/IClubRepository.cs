using System;
using System.Collections.Generic;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services
{
    public interface IClubRepository
    {
        List<Club> GetAllClubs();
        void AddClub(Club club);
        Club GetClubById(Guid id);
        List<Member> GetMembersByClubId(Guid clubId);
        void AddMember(Member member);
    }
}
