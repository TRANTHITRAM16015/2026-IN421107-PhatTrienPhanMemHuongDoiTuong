using System;
using System.Collections.Generic;
using System.Linq;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services
{
    public class ClubRepository : IClubRepository
    {
        private readonly List<Club> _clubs = new();
        private readonly List<Member> _members = new();

        public List<Club> GetAllClubs() => _clubs;

        public void AddClub(Club club) => _clubs.Add(club);

        public Club GetClubById(Guid id) => _clubs.FirstOrDefault(c => c.Id == id);

        public List<Member> GetMembersByClubId(Guid clubId) => _members.Where(m => m.ClubId == clubId).ToList();

        public void AddMember(Member member) => _members.Add(member);
    }
}