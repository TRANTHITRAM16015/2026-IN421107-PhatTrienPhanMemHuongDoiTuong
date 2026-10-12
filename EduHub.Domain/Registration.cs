using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Domain
{
    public enum RegistrationStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
  
    public class Registration
    {
        public Guid Id { get; private set; }
        public Guid MemberId { get; private set; }

        public Guid EventId { get; private set; }
        public DateTime RegisteredAt { get; private set; }

        public RegistrationStatus Status { get; private set; }

        private Registration(
            Guid id, Guid memberId, Guid eventId,
            DateTime registeredAt, RegistrationStatus status)
        {
            Id = id;
            MemberId = memberId;
            EventId = eventId;
            RegisteredAt = registeredAt;
            Status = status;
        }

        public static Registration Create(
            Guid memberId, Guid eventId)
        {
            if (memberId == Guid.Empty)
                throw new ArgumentException("MemberId không hợp lệ.");

            if (eventId == Guid.Empty)
                throw new ArgumentException("EventId không hợp lệ.");

            return new Registration(
                Guid.NewGuid(),
                memberId,
                eventId,
                DateTime.Now,
                RegistrationStatus.Pending
            );
        }
        public void Confirm()
        {
            if (Status != RegistrationStatus.Pending)
                throw new InvalidOperationException(
                    "Chỉ phiếu Pending mới được xác nhận.");

            Status = RegistrationStatus.Confirmed;
        }

        public void Cancel()
        {
            if (Status == RegistrationStatus.Cancelled)
                throw new InvalidOperationException(
                    "Phiếu đăng ký đã bị hủy.");

            Status = RegistrationStatus.Cancelled;
        }
    }
}
