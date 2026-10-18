using System;
using System.Collections.Generic;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services
{
    public class ClubService
    {
        private readonly IClubRepository _clubRepo;
        private readonly IAppLogger _logger;

        public ClubService(IClubRepository clubRepo, IAppLogger logger)
        {
            _clubRepo = clubRepo;
            _logger = logger;
        }

        public void CreateClub(string name, string description, string presidentEmail, string channel)
        {
            
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Club name is required");
            if (name.Length > 100)
                throw new ArgumentException("Club name too long");

            
            var club = new Club { Id = Guid.NewGuid(), Name = name, Description = description };
            _clubRepo.AddClub(club);
            _logger.Log($"Club created: {name}");

           
            var notifier = NotificationServiceFactory.Create(channel);
            notifier.Send(presidentEmail, $"Welcome to your new club: {name}");
        }

        public void AddMemberToClub(Guid clubId, string memberName, string email, string channel)
        {
            var club = _clubRepo.GetClubById(clubId)
                ?? throw new InvalidOperationException("Club not found");

            var members = _clubRepo.GetMembersByClubId(clubId);
            if (members.Count >= 50)
                throw new InvalidOperationException("Club is full (max 50 members)");

            var member = new Member { Id = Guid.NewGuid(), Name = memberName, Email = email, ClubId = clubId };
            _clubRepo.AddMember(member);
            _logger.Log($"Member {memberName} joined {club.Name}");

         
            var notifier = NotificationServiceFactory.Create(channel);
            notifier.Send(email, $"Welcome {memberName} to {club.Name}!");
        }

        public List<Club> GetAllClubs()
        {
            return _clubRepo.GetAllClubs();
        }
    }
}