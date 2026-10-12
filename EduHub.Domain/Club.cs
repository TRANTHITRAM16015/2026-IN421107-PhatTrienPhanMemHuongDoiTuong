using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Domain
{
    public class Club
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<Member> Members { get; private set; }

        private Club(
            Guid id, string name, string description,
            DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            Members = new List<Member>();
        }

        public static Club Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên CLB không được để trống.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Mô tả không được để trống.");

            return new Club(
                Guid.NewGuid(),
                name.Trim(),
                description.Trim(),
                DateTime.Now
            );
        }
        public void AddMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (Members.Exists(m => m.Id == member.Id))
                throw new InvalidOperationException(
                    "Thành viên đã có trong câu lạc bộ.");

            Members.Add(member);
        }
        public int MemberCount()
        {
            return Members.Count;
        }
    }
}
