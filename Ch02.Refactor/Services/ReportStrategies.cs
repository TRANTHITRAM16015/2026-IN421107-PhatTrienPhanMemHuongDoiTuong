using System;
using System.Collections.Generic;
using System.Linq;
using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services
{
    public interface IReportStrategy
    {
        void Generate(List<Club> clubs, List<Member> members);
    }

    public class ConsoleReportStrategy : IReportStrategy
    {
        public void Generate(List<Club> clubs, List<Member> members)
        {
            foreach (var club in clubs)
            {
                var memberCount = members.Count(m => m.ClubId == club.Id);
                Console.WriteLine($"Club: {club.Name} | Members: {memberCount}");
            }
        }
    }

   
    public class CsvReportStrategy : IReportStrategy
    {
        public void Generate(List<Club> clubs, List<Member> members)
        {
            Console.WriteLine("ClubName,MemberCount");
            foreach (var club in clubs)
            {
                var memberCount = members.Count(m => m.ClubId == club.Id);
                Console.WriteLine($"{club.Name},{memberCount}");
            }
        }
    }
    public class ReportService
    {
        private readonly IReportStrategy _strategy;

        public ReportService(IReportStrategy strategy)
        {
            _strategy = strategy;
        }

        public void GenerateReport(List<Club> clubs, List<Member> members)
        {
            _strategy.Generate(clubs, members);
        }
    }
}