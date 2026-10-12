using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Domain
{
    public class Event
    {
        public Guid Id { get; private set; }
        public Guid ClubId { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public int Capacity { get; private set; }
        public bool IsOnline { get; private set; }

        private Event(
            Guid id, Guid clubId, string title,
            DateTime date, int capacity, bool isOnline)
        {
            Id = id;
            ClubId = clubId;
            Title = title;
            Date = date;
            Capacity = capacity;
            IsOnline = isOnline;
        }

        /// <summary>Tạo sự kiện mới.</summary>
        public static Event Create(
            Guid clubId, string title, DateTime date,
            int capacity, bool isOnline)
        {
            if (clubId == Guid.Empty)
                throw new ArgumentException("ClubId không hợp lệ.");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Tiêu đề không được để trống.");

            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(capacity), "Sức chứa phải lớn hơn 0.");

            return new Event(
                Guid.NewGuid(),
                clubId,
                title.Trim(),
                date,
                capacity,
                isOnline
            );
        }
        public bool HasCapacity(int registeredCount)
        {
            if (registeredCount < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(registeredCount));

            return registeredCount < Capacity;
        }
    }
}
