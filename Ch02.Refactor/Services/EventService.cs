using System;
using System.Collections.Generic;
using System.Linq;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services
{
    public interface IEventObserver
    {
        void OnEventCreated(Event ev, Club club, List<Member> members);
    }

    
    public class EmailEventObserver : IEventObserver
    {
        public void OnEventCreated(Event ev, Club club, List<Member> members)
        {
            foreach (var m in members)
            {
                Console.WriteLine($"Email to {m.Email}: new event '{ev.Title}' at {club.Name}");
            }
        }
    }

    // EventService quản lý sự kiện và danh sách Observer
    public class EventService
    {
        private readonly List<IEventObserver> _observers = new();
        private readonly IClubRepository _clubRepo;
        private readonly IAppLogger _logger;

        public EventService(IClubRepository clubRepo, IAppLogger logger)
        {
            _clubRepo = clubRepo;
            _logger = logger;
        }

        public void RegisterObserver(IEventObserver observer)
        {
            _observers.Add(observer);
        }

        public Event CreateEvent(Guid clubId, string title, DateTime date)
        {
            var club = _clubRepo.GetClubById(clubId) ?? throw new InvalidOperationException("Club not found");

            var ev = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
            _logger.Log($"Event created: {title}");

            var members = _clubRepo.GetMembersByClubId(clubId);

            
            foreach (var observer in _observers)
            {
                observer.OnEventCreated(ev, club, members);
            }

            return ev;
        }
    }
}