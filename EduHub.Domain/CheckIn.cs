using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Domain
{
    public enum CheckInMethod
    {
        Manual,
        QRCode,
        Online
    }

    public class CheckIn
    {
        public Guid Id { get; private set; }
        public Guid RegistrationId { get; private set; }
        public DateTime CheckedInAt { get; private set; }
        public CheckInMethod Method { get; private set; }

        private CheckIn(
            Guid id, Guid registrationId,
            DateTime checkedInAt, CheckInMethod method)
        {
            Id = id;
            RegistrationId = registrationId;
            CheckedInAt = checkedInAt;
            Method = method;
        }

        public static CheckIn Create(
            Registration registration, CheckInMethod method)
        {
            if (registration == null)
                throw new ArgumentNullException(nameof(registration));

            if (registration.Id == Guid.Empty)
                throw new ArgumentException("RegistrationId không hợp lệ.");

            if (registration.Status != RegistrationStatus.Confirmed)
                throw new InvalidOperationException(
                    "Chỉ phiếu đã xác nhận mới được điểm danh.");

            if (!Enum.IsDefined(typeof(CheckInMethod), method))
                throw new ArgumentException(
                    "Hình thức điểm danh không hợp lệ.");

            return new CheckIn(
                Guid.NewGuid(),
                registration.Id,
                DateTime.Now,
                method
            );
        }
        public string Summary()
        {
            return $"Check-in lúc {CheckedInAt:dd/MM/yyyy HH:mm}" +
                   $" | RegistrationId: {RegistrationId}" +
                   $" | Hình thức: {Method}";
        }
    }
}
